using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.Calificar
{
    public partial class calificarVendedor : Form
    {
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable tablaDatos = new DataTable();
        DbQueryHandlerCalificar dbQueryHandler = new DbQueryHandlerCalificar();

       
        public calificarVendedor()
        {
            InitializeComponent();
            try { dbQueryHandler.crearVistaVendedoresSinCalificar(); }
            catch
            {//vista ya creada
            };

                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                dataAdapter.SelectCommand = dbQueryHandler.consultaVendedoresSinCalificar(CurrentUser.user.getUserId());
                try { dataAdapter.Fill(tablaDatos); }
                catch { };
                dataGridView1.DataSource = tablaDatos;
                dataGridView1.CellClick += dataGridView1_CellClick;

                DataGridViewButtonColumn calificar = new DataGridViewButtonColumn();
                calificar.Name = "Calificar";
                calificar.Text = "Calificar";
                calificar.Name = "Calificar";
                calificar.UseColumnTextForButtonValue = true;

                int columnIndex = dataGridView1.ColumnCount;
                if (dataGridView1.Columns["Calificar"] == null) { dataGridView1.Columns.Insert(columnIndex, calificar); }
                else { dataGridView1.Columns["Calificar"].DisplayIndex = dataGridView1.ColumnCount - 1; };
        }

        public void recargar() 
        {
            dataAdapter = new SqlDataAdapter();
            tablaDatos = new DataTable();
            dataAdapter.SelectCommand = dbQueryHandler.consultaVendedoresSinCalificar(CurrentUser.user.getUserId());
            try { dataAdapter.Fill(tablaDatos); }
            catch { };
            dataGridView1.DataSource = tablaDatos;
            dataGridView1.CellClick += dataGridView1_CellClick;

            DataGridViewButtonColumn calificar = new DataGridViewButtonColumn();
            calificar.Name = "Calificar";
            calificar.Text = "Calificar";
            calificar.Name = "Calificar";
            calificar.UseColumnTextForButtonValue = true;

            int columnIndex = dataGridView1.ColumnCount;
            if (dataGridView1.Columns["Calificar"] == null) { dataGridView1.Columns.Insert(columnIndex, calificar); }
            else { dataGridView1.Columns["Calificar"].DisplayIndex = dataGridView1.ColumnCount - 1; };
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Calificar"].Index)
                {
                    int vendedorId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    int compraID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    darCalificacion pantallaDarCalificacion = new darCalificacion(vendedorId, compraID,this);
                    pantallaDarCalificacion.Show();
                }
            }
            catch { };
                
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tablaDatos = null;
            dataAdapter = null;

            try { dbQueryHandler.dropVistaVendedoresSinCalificar(); }
            catch { };
            this.Dispose();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            historial pantallaHistorialReciente = new historial();
            pantallaHistorialReciente.Show();
        }

    }
    class DbQueryHandlerCalificar 
    { 
        public void crearVistaVendedoresSinCalificar()
        {
            SqlCommand comando = new SqlCommand(
            @"CREATE VIEW vendedoresCalificados AS
            select pub.Id_Usuario
            from
            GROUP_APROVED.Compras comp join GROUP_APROVED.Calificaciones calif on (comp.ID_Compra=calif.ID_Compra)
            join GROUP_APROVED.Publicaciones pub on (pub.Publicacion_Cod = comp.Publicacion_Cod)", DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
        }

        public void dropVistaVendedoresSinCalificar()
        {
            SqlCommand comando = new SqlCommand("DROP VIEW vendedoresCalificados", DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
        }
        public SqlCommand consultaVendedoresSinCalificar(int user)
        {
            SqlCommand comando = new SqlCommand(
            @"select pub.Id_Usuario,comp.ID_Compra
            from GROUP_APROVED.Publicaciones pub join GROUP_APROVED.Compras comp on (pub.Publicacion_Cod=comp.Publicacion_Cod)
            where (pub.Id_Usuario not in (select Id_Usuario from vendedoresCalificados)) and (comp.Id_Usuario=" +user+")",DbConnection.connection.getdbconnection());
            return comando;
        }
    }
}

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

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class BajaUsuarioEmpresa : Form
    {
        SqlDataAdapter dataAdapter;
        DataTable tablaDatos;
        DbQueryHandlerPantallaBajaUsuarioEmpresa dbQueryHandler = new DbQueryHandlerPantallaBajaUsuarioEmpresa();

        
        public BajaUsuarioEmpresa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxRazSoc.Text="";
            textBoxCuit.Text = "";
            textBoxEmail.Text = "";
            dataAdapter = null;
            //dataGridView1.Dispose();
            //dataGridView1.ClearSelection();
            
            dataGridView1.DataSource = null;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            int tipoFiltro = chequearCampos();
            if (tipoFiltro > 0)
            {
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                if (tipoFiltro == 1) { dataAdapter.SelectCommand = dbQueryHandler.consultaFiltrosConCuit(textBoxCuit.Text, textBoxEmail.Text, textBoxRazSoc.Text); };
                if (tipoFiltro == 2) { dataAdapter.SelectCommand = dbQueryHandler.consultaFiltrosSinCuit(textBoxEmail.Text, textBoxRazSoc.Text); };
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
                dataGridView1.CellClick += dataGridView1_CellClick;
                DataGridViewButtonColumn Baja = new DataGridViewButtonColumn();
                Baja.Name = "Baja";
                Baja.Text = "Baja";
                Baja.Name = "Baja";
                Baja.UseColumnTextForButtonValue = true;

                int columnIndex = dataGridView1.ColumnCount;
                if (dataGridView1.Columns["Baja"] == null) { dataGridView1.Columns.Insert(columnIndex, Baja); }
                else { dataGridView1.Columns["Baja"].DisplayIndex = dataGridView1.ColumnCount-1; };
                dataGridView1.Columns["Baja"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private int chequearCampos()
        {
            if ((textBoxCuit.Text != "") && !(tieneLetras(textBoxCuit.Text))) { return 1; };
            if ((textBoxCuit.Text != "") && (tieneLetras(textBoxCuit.Text))) { MessageBox.Show("Ingrese Cuit válido"); return 0; };
            if ((textBoxRazSoc.Text == "")&& (textBoxCuit.Text == "") && (textBoxEmail.Text == "")) { MessageBox.Show("Ingrese algún filtro"); return 0; };
            if (((textBoxRazSoc.Text != "") || (textBoxEmail.Text != "")) && (textBoxCuit.Text == "")) { return 2; };
            MessageBox.Show("Error raro");
            return 0;
        }
        private bool tieneLetras(string texto)
        {
            string letras = "abcdefghijklmmñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            bool rta = false;
            foreach (char letra in texto)
            {
                if (letras.Contains(letra)) { rta = true; return rta; };
            }
            return rta;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == dataGridView1.Columns["Baja"].Index) && (e.RowIndex >= 0))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string username = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if ((id != "") && (username != ""))
                    {
                        int rta = dbQueryHandler.bajaLogica(id, username);
                        if (rta == 1) { MessageBox.Show("Usuario cliente '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' (id: " + id + ") dado de baja lógica"); }
                        else MessageBox.Show("Error SQL");
                    }
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
    class DbQueryHandlerPantallaBajaUsuarioEmpresa
    {
        public SqlCommand consultaFiltrosConCuit(string cuit, string mail, string razSoc)
        {
            SqlCommand comand = new SqlCommand(@"select Id_Usuario, Username, Empresa_Razon_Social, Empresa_Cuit, Empresa_Mail  from GROUP_APROVED.Empresas c join GROUP_APROVED.Usuarios u
 on c.Id_Usuario = u.Id_Usr where Empresa_Razon_Social = '%"+razSoc+"%' and Empresa_Mail like '%"+mail+"%' and Empresa_Cuit = "+cuit, DbConnection.connection.getdbconnection());
            return comand;
        }
        public SqlCommand consultaFiltrosSinCuit(string mail, string razSoc)
        {
            SqlCommand comand = new SqlCommand(@"select Id_Usuario, Username, Empresa_Razon_Social, Empresa_Cuit, Empresa_Mail  from GROUP_APROVED.Empresas c join GROUP_APROVED.Usuarios u
 on c.Id_Usuario = u.Id_Usr where Empresa_Razon_Social = '%" + razSoc + "%' and Empresa_Mail like '%" + mail + "%'", DbConnection.connection.getdbconnection());
            return comand;
        }
        public int bajaLogica(string id, string username)
        {
            int mensajeRespuesta;
            SqlCommand command = new SqlCommand("bajaLogicaUsuario", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idusuario", id);
            command.Parameters.AddWithValue("@Username", username);

            SqlParameter retVal = new SqlParameter("@respuesta", SqlDbType.Int);
            command.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            mensajeRespuesta = Convert.ToInt32(command.Parameters["@respuesta"].Value);

            return mensajeRespuesta;
        }
    }
}

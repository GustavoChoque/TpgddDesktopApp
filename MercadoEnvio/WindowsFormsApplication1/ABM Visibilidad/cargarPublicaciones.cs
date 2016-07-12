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

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class cargarPublicaciones : Form
    {
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable tablaDatos = new DataTable();
        DbQueryHandlerVisibilidad dbQueryHandler = new DbQueryHandlerVisibilidad();

       
        public cargarPublicaciones()
        {
            InitializeComponent();
            dataAdapter = new SqlDataAdapter();
            tablaDatos = new DataTable();
            dataAdapter.SelectCommand = dbQueryHandler.consultaPublicaciones(CurrentUser.user.getUserId());
            dataAdapter.Fill(tablaDatos);
            dataGridView1.DataSource = tablaDatos;
            dataGridView1.CellClick += dataGridView1_CellClick;
            
                DataGridViewButtonColumn ModVis = new DataGridViewButtonColumn();
                ModVis.Name = "Modificación Visibilidad";
                ModVis.Text = "Modificación Visibilidad";
                ModVis.Name = "Modificación Visibilidad";
                ModVis.UseColumnTextForButtonValue = true;

                int columnIndex = dataGridView1.ColumnCount;
                if (dataGridView1.Columns["Modificación Visibilidad"] == null) { dataGridView1.Columns.Insert(columnIndex, ModVis); }
                else { dataGridView1.Columns["Modificación Visibilidad"].DisplayIndex = dataGridView1.ColumnCount - 1; };
            
            
        }
        
        public void cargar()
        {
            dataAdapter = null;
            tablaDatos = null;

            dataAdapter = new SqlDataAdapter();
            tablaDatos = new DataTable();
            dataAdapter.SelectCommand = dbQueryHandler.consultaPublicaciones(CurrentUser.user.getUserId());
            dataAdapter.Fill(tablaDatos);
            dataGridView1.DataSource = tablaDatos;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Modificación Visibilidad"].Index)
                 {
                     int codPub = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                     int codVis = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                     int cantPubl = dbQueryHandler.cantPubl(CurrentUser.user.getUserId());
                     modificarVisibilidad pantallaModificarVisibilidad = new modificarVisibilidad(codPub, codVis, cantPubl, this);
                     pantallaModificarVisibilidad.Show();
                 }
                
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                
        
    }
    class DbQueryHandlerVisibilidad
    {

        public SqlCommand consultaPublicaciones(int userid)
        {
            SqlCommand comand = new SqlCommand(@"select Publicacion_Cod, Publicacion_Desc, Publicacion_Fecha_Venc,Visibilidad_Cod,Publicacion_Acepta_Envio from GROUP_APROVED.Publicaciones where Id_Usuario = " + userid.ToString(), DbConnection.connection.getdbconnection());
            return comand;
        }

        public int cantPubl(int userid)
        {
            SqlDataReader lector;
            SqlCommand comand = new SqlCommand("select count(*) from GROUP_APROVED.Publicaciones where Id_Usuario = " + userid, DbConnection.connection.getdbconnection());
            lector = comand.ExecuteReader();
            lector.Read();
            int retorno = lector.GetInt32(0);
            lector.Close();
            return (retorno);
        }

    }
    }

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

namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class ListadoSeleccion : Form
    {
        DbQueryHandlerListadoSeleccion dbQueryHandler = new DbQueryHandlerListadoSeleccion();

        public ListadoSeleccion()
        {
            InitializeComponent();
            dataGridView1.Columns[0].Visible = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {       if (e.ColumnIndex.Equals(2))  {
                FormularioDeModificacion FdM= new FormularioDeModificacion();
                //guardo el numero de la fila
                int fila=e.RowIndex;
                string rolElegido =dataGridView1.Rows[fila].Cells[1].Value.ToString();
               //para enviar texto al texto de otro form, cambie la propiedad modifiers del textBox1 de FormularioDeModificacion
                FdM.textBox1.Text = rolElegido;
                string idRolElegido = dataGridView1.Rows[fila].Cells[0].Value.ToString();
                FdM.getIdRol(idRolElegido);
                FdM.Show();
               
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            string texto = textoABuscar.Text;
            SqlDataReader registros = dbQueryHandler.getRoles(texto);
            while (registros.Read())
            {  dataGridView1.Rows.Add(registros["Id_Rol"].ToString(), registros["Desc_Rol"].ToString());
               Column2.Text = "Modificar";  
            }
            registros.Close();     
        }

        private void ListadoSeleccion_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class DbQueryHandlerListadoSeleccion
    {
        public SqlDataReader getRoles(string texto) {
            string cadena = "select Id_Rol,Desc_Rol from GROUP_APROVED.Roles where Desc_Rol Like '%" + texto + "%'";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            SqlDataReader registros = comando.ExecuteReader();
            return registros;
        }
    }
}

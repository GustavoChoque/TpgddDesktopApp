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
    public partial class EliminarRol : Form
    {
        DbQueryHandlerBaja dbQueryHandler = new DbQueryHandlerBaja();
        

        public EliminarRol()
        {
            InitializeComponent();
            dataGridView1.Columns[0].Visible = false;
        }
        
        private void botonBuscar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
           
            string texto = textoABuscar.Text;          
            SqlDataReader registros = dbQueryHandler.getRoles(texto);
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Id_Rol"].ToString(), registros["Desc_Rol"].ToString());
                Column2.Text = "Eliminar";
            }
            registros.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(2))
            { //guardo el numero de la fila
                int fila = e.RowIndex;
                string idRolElegido = dataGridView1.Rows[fila].Cells[0].Value.ToString();
                int idRol = Convert.ToInt32(idRolElegido);
                string rolElegido = dataGridView1.Rows[fila].Cells[1].Value.ToString();

                dbQueryHandler.inhabilitarRol(idRol);
                MessageBox.Show("Se Elimino el Rol");
                dataGridView1.Rows.Clear();
            }
        }
    }
    public class DbQueryHandlerBaja {
        public SqlDataReader getRoles(string texto)
        {
            string cadena = "select Id_Rol,Desc_Rol from GROUP_APROVED.Roles where Desc_Rol Like '%" + texto + "%' AND estado='H'";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            SqlDataReader registros = comando.ExecuteReader();
            return registros;
        }
        public void inhabilitarRol(int idRol) {
           
            string cadena = "update GROUP_APROVED.Roles Set estado ='I' where Id_Rol='" + idRol + "'";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
            /*conexion.Open();//ver si hacerlo con un trigger o un delete
                string cadena4 = "Delete From GROUP_APROVED.FuncionesxRol where Id_Rol='" + idRol + "'";
                SqlCommand comando4 = new SqlCommand(cadena4, conexion);
                comando4.ExecuteNonQuery();
                conexion.Close();*/
        }
    }
}

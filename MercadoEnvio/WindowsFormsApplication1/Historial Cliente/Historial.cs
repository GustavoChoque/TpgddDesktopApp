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
namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class Historial : Form
    {
        DbQueryHandlerHistorial dbQueryHandler = new DbQueryHandlerHistorial();
        public Historial()
        {
            InitializeComponent();
        }

        private void salir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Historial_Load(object sender, EventArgs e)
        {
            SqlDataReader registros = dbQueryHandler.getInfoCompras();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Compra_Fecha"].ToString(), registros["Publicacion_Desc"].ToString(), registros["Compra_Cantidad"].ToString(), registros["Calificado"].ToString(), registros["Calif_Cant_Est"].ToString());
               
            }
        }
    }
    public class DbQueryHandlerHistorial
    {
        public SqlDataReader getInfoCompras()
        {
            string subQuery = "realizar funcion que devuelva un bool";//Funcion para ver si ya se califico la compra 
            string cadena = "select Compra_Fecha,Publicacion_Desc,Compra_Cantidad,"+subQuery+" Calificado,Calif_Cant_Est from GROUP_APROVED.Compras c Join GROUP_APROVED.Publicaciones p On(c.Publicacion_Cod=p.Publicacion_Cod) Join GROUP_APROVED.Calificaciones ca On (p.Publicacion_Cod=ca.Publicacion_Cod)";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            SqlDataReader registros = comando.ExecuteReader();
            return registros;
        }
    }
}

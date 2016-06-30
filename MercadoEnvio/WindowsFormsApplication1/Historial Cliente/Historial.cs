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
        SqlDataAdapter pagingAdapter;
        DataSet pagingDS;
        int scrollVal;
        public Historial()
        {
            InitializeComponent();
            scrollVal = 0;
        }

        private void salir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Historial_Load(object sender, EventArgs e)
        {
            /*SqlDataReader registros = dbQueryHandler.getInfoCompras();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Compra_Fecha"].ToString(), registros["Publicacion_Desc"].ToString(), registros["Compra_Cantidad"].ToString(), registros["Calificado"].ToString(), registros["Calificacion"].ToString());
               
            }
            registros.Close();*/

            string cadena = "select Compra_Fecha,Publicacion_Desc,Compra_Cantidad,( GROUP_APROVED.usuarioYaCalifico(c.ID_Compra)) Calificado ,GROUP_APROVED.getCalificacion(c.ID_Compra)Calificacion from GROUP_APROVED.Compras c Join GROUP_APROVED.Publicaciones p On (c.Publicacion_Cod=p.Publicacion_Cod)" /*where c.Id_Usuario=" + CurrentUser.user.getUserId()*/;//sacar luego los /**/ de la consulta
            pagingAdapter = new SqlDataAdapter(cadena, DbConnection.connection.getdbconnection());
            pagingDS = new DataSet();
            
            pagingAdapter.Fill(pagingDS, scrollVal, 5, "Historial_Compras");
            //DbConnection.connection.closeConnection();
            dataGridView1.DataSource = pagingDS;
            dataGridView1.DataMember = "Historial_Compras";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scrollVal = scrollVal - 5;
            if (scrollVal <= 0)
            {
                scrollVal = 0;
            }
            pagingDS.Clear();
            pagingAdapter.Fill(pagingDS, scrollVal, 5, "Historial_Compras");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scrollVal = scrollVal + 5;
            if (scrollVal > 23)
            {
                scrollVal = 18;
            }
            pagingDS.Clear();
            pagingAdapter.Fill(pagingDS, scrollVal, 5, "Historial_Compras");
        }
       
    }
    public class DbQueryHandlerHistorial
    {
        //public SqlDataReader getInfoCompras()
        //{
          //  string cadena = "select Compra_Fecha,Publicacion_Desc,Compra_Cantidad,( GROUP_APROVED.usuarioYaCalifico(c.ID_Compra)) Calificado ,GROUP_APROVED.getCalificacion(c.ID_Compra)Calificacion from GROUP_APROVED.Compras c Join GROUP_APROVED.Publicaciones p On (c.Publicacion_Cod=p.Publicacion_Cod)" /*where c.Id_Usuario=" + CurrentUser.user.getUserId()*/;//sacar luego los /**/ de la consulta
            //SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            //SqlDataReader registros = comando.ExecuteReader();
            //return registros;
       // }
        
    }
}

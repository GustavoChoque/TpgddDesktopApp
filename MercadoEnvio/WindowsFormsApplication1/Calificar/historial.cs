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
    public partial class historial : Form
    {
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable tablaDatos = new DataTable();
        DbQueryHandlerHistorial dbQueryHandler = new DbQueryHandlerHistorial();

        public historial()
        {
            InitializeComponent();
            dataAdapter = new SqlDataAdapter();
            tablaDatos = new DataTable();
            dataAdapter.SelectCommand = dbQueryHandler.ultimasNOperaciones(CurrentUser.user.getUserId(),5);
            dataAdapter.Fill(tablaDatos);
            dataGridView1.DataSource = tablaDatos;

            textBoxComprasTotales.Text= dbQueryHandler.totalCompras(CurrentUser.user.getUserId());
            textBoxSubTotales.Text= dbQueryHandler.totalSubastas(CurrentUser.user.getUserId());

            textBoxCompras5est.Text= dbQueryHandler.totalComprasNest(CurrentUser.user.getUserId(), 5);
            textBoxCompras4est.Text = dbQueryHandler.totalComprasNest(CurrentUser.user.getUserId(), 4);
            textBoxCompras3est.Text = dbQueryHandler.totalComprasNest(CurrentUser.user.getUserId(), 3);
            textBoxCompras2est.Text = dbQueryHandler.totalComprasNest(CurrentUser.user.getUserId(), 2);
            textBoxCompras1est.Text = dbQueryHandler.totalComprasNest(CurrentUser.user.getUserId(), 1);

            textBoxSub5est.Text = dbQueryHandler.totalSubNest(CurrentUser.user.getUserId(), 5);
            textBoxSub4est.Text = dbQueryHandler.totalSubNest(CurrentUser.user.getUserId(), 4);
            textBoxSub3est.Text = dbQueryHandler.totalSubNest(CurrentUser.user.getUserId(), 3);
            textBoxSub2est.Text = dbQueryHandler.totalSubNest(CurrentUser.user.getUserId(), 2);
            textBoxSub1est.Text = dbQueryHandler.totalSubNest(CurrentUser.user.getUserId(), 1);
            
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class DbQueryHandlerHistorial 
    {
        public SqlCommand ultimasNOperaciones(int userId, int cantOperaciones) 
        {
            SqlCommand comando = new SqlCommand(
            "select top "+cantOperaciones+ @" com.ID_Compra,com.Compra_Fecha,com.Publicacion_Cod,pub.Publicacion_Tipo
            from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com
            on (cal.ID_Compra=com.ID_Compra)
            join GROUP_APROVED.Publicaciones pub on (pub.Publicacion_Cod=com.Publicacion_Cod)
            where com.Id_Usuario = "+userId+
            @"order by com.Compra_Fecha desc", DbConnection.connection.getdbconnection());
            return comando;
        }

        public string totalCompras(int userid) 
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand(
            @"select count(com.ID_Compra)
            from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com
            on (cal.ID_Compra=com.ID_Compra)
            join GROUP_APROVED.Publicaciones p on (com.Publicacion_Cod=p.Publicacion_Cod)
            where com.ID_Compra="+userid+" and p.Publicacion_Tipo='Compra Inmediata'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            string rta = lector.GetInt32(0).ToString();
            lector.Close();
            return rta;

        }

        public string totalSubastas(int userid) 
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand(
            @"select count(com.ID_Compra)
            from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com
            on (cal.ID_Compra=com.ID_Compra)
            join GROUP_APROVED.Publicaciones p on (com.Publicacion_Cod=p.Publicacion_Cod)
            where com.ID_Compra=" + userid + " and p.Publicacion_Tipo='Subasta'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            string rta = lector.GetInt32(0).ToString();
            lector.Close();
            return rta;
        }

        public string totalComprasNest(int userid, int cantidad) 
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand(
            @"select count(cal.Calif_Cant_Est)
            from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com
            on (com.ID_Compra=cal.ID_Compra)
            where cal.Calif_Cant_Est = "+cantidad+" and com.Id_Usuario="+userid+
            " and p.Publicacion_Tipo='Compra Inmediata'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            string rta = lector.GetInt32(0).ToString();
            lector.Close();
            return rta;
        }

        public string totalSubNest(int userid, int cantidad)
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand(
            @"select count(cal.Calif_Cant_Est)
            from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com
            on (com.ID_Compra=cal.ID_Compra)
            where cal.Calif_Cant_Est = " + cantidad + " and com.Id_Usuario=" + userid +
            " and p.Publicacion_Tipo='Subasta'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            string rta = lector.GetInt32(0).ToString();
            lector.Close();
            return rta;
        }
    }

}

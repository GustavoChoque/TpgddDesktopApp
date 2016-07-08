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
    public partial class darCalificacion : Form
    {
        int idVendedor;
        int idCompra;
        DbQueryHandlerCalif dbQueryHandler = new DbQueryHandlerCalif();

        public darCalificacion(int vendedorID, int compraID)
        {
            InitializeComponent();
            idVendedor = vendedorID;
            idCompra = compraID;
            textBox1.Text = vendedorID.ToString();
            textBox3.Text = dbQueryHandler.reputacionVend(idVendedor);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string desc = textBox2.Text;
                string estrellas = comboBox1.SelectedItem.ToString();
                string idcompra = idCompra.ToString();
                dbQueryHandler.insertarCalificacion(estrellas, desc, idcompra);
                MessageBox.Show("Exito");
                this.Close();

            }
            catch { MessageBox.Show("Error"); };

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    class DbQueryHandlerCalif 
    {
        public string reputacionVend(int idvend) 
        {
            SqlDataReader lector;
            SqlCommand comandoTotalCalif = new SqlCommand(
                @"select sum(cal.Calif_Cant_Est) 
                from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com on (cal.ID_Compra=com.ID_Compra)
                join GROUP_APROVED.Publicaciones pub on (pub.Publicacion_Cod=com.Publicacion_Cod)
                where pub.Id_Usuario = "+idvend, DbConnection.connection.getdbconnection());
            lector = comandoTotalCalif.ExecuteReader();
            lector.Read();
            int totalEst = lector.GetInt32(0);
            lector.Close();
            
            SqlCommand comandoCantidadVentas = new SqlCommand(
                @"select count(pub.Publicacion_Cod)
                from GROUP_APROVED.Publicaciones pub
                where pub.Id_Usuario ="+idvend, DbConnection.connection.getdbconnection());
            lector = comandoCantidadVentas.ExecuteReader();
            lector.Read();
            int totVtas = lector.GetInt32(0);
            lector.Close();
            
            int rta = totalEst / totVtas;
            return rta.ToString();
        }
        public void insertarCalificacion(string cantEst, string desc, string idcompra)
        {
            SqlCommand comando = new SqlCommand(
            @"insert into GROUP_APROVED.Calificaciones 
            (Calif_Cant_Est,Calif_Descr,ID_Compra)
            values ("+cantEst+", "+desc+", "+idcompra+")", DbConnection.connection.getdbconnection());
        }
    }
}

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
        Calificar.calificarVendedor pantallaAnterior;
        DbQueryHandlerCalif dbQueryHandler = new DbQueryHandlerCalif();

        public darCalificacion(int vendedorID, int compraID, Calificar.calificarVendedor pantalla)
        {
            InitializeComponent();
            idVendedor = vendedorID;
            idCompra = compraID;
            pantallaAnterior = pantalla;
            textBox1.Text = vendedorID.ToString();
            textBox3.Text = dbQueryHandler.reputacionVend(idVendedor);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                string desc = textBox2.Text;
                string estrellas;
                if (comboBox1.SelectedIndex != -1)
                { estrellas = comboBox1.SelectedItem.ToString(); }
                else { estrellas = "nada"; }
                string idcompra = idCompra.ToString();
            if (estrellas != "nada"){
                if (dbQueryHandler.insertarCalificacion(estrellas, desc, idcompra) == 1)
                { MessageBox.Show("Exito"); }
                else { MessageBox.Show("Error"); }
                pantallaAnterior.recargar();
                this.Close();}
             else {MessageBox.Show("Seleccionar cantidad de estrellas");}
            //}
            //catch { MessageBox.Show("Error"); };

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pantallaAnterior.recargar();
            this.Dispose();
            this.Close();
        }
    }
    class DbQueryHandlerCalif 
    {       
        /*public void insertarCalificacion(string cantEst, string desc, string idcompra)
        {
            SqlCommand comando = new SqlCommand(
            @"insert into GROUP_APROVED.Calificaciones 
            (Calif_Cant_Est,Calif_Descr,ID_Compra)
            values ("+cantEst+", "+desc+", "+idcompra+")", DbConnection.connection.getdbconnection());
        }*/

        public string reputacionVend(int idvend) 
        {
            try
            {
                int totalEst;
                int rta;
                int totVtas;
            SqlDataReader lector;
            SqlCommand comandoTotalCalif = new SqlCommand(
                @"select sum(cal.Calif_Cant_Est) 
                from GROUP_APROVED.Calificaciones cal join GROUP_APROVED.Compras com on (cal.ID_Compra=com.ID_Compra)
                join GROUP_APROVED.Publicaciones pub on (pub.Publicacion_Cod=com.Publicacion_Cod)
                where pub.Id_Usuario = "+idvend, DbConnection.connection.getdbconnection());
            lector = comandoTotalCalif.ExecuteReader();
            lector.Read();

            SqlDataReader lector2;
            SqlCommand comandoCantidadVentas = new SqlCommand(
                @"select count(pub.Publicacion_Cod)
                from GROUP_APROVED.Publicaciones pub
                where pub.Id_Usuario ="+idvend, DbConnection.connection.getdbconnection());
            lector2 = comandoCantidadVentas.ExecuteReader();
            lector2.Read();
            totalEst = lector.GetInt32(0);
            totVtas = lector2.GetInt32(0);

            lector2.Close();
            lector.Close();
            
            rta = 0;
                return rta.ToString();
            }
            catch { int rta = 0; return rta.ToString(); };
        }

        public int insertarCalificacion(string cantEst, string desc, string idcompra)
        {
            SqlCommand comando = new SqlCommand("GROUP_APROVED.insertarCalificacion", DbConnection.connection.getdbconnection());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cantEstrellas", cantEst);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@idcompra", idcompra);

            SqlParameter retVal = new SqlParameter("@rta", SqlDbType.Int);
            comando.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            comando.ExecuteNonQuery();
            int mensajeRespuesta = Convert.ToInt32(comando.Parameters["@rta"].Value);
            return mensajeRespuesta;


        }
    }
}

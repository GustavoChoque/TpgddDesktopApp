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

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class Form2 : Form
    {
        Int32 pubId = 0;
        DbQueryHandlerModify dbQueryHandler = new DbQueryHandlerModify();
        Dictionary<String, String> rubros;
        Dictionary<String, String> visibilidades;

        public Form2(Int32 pub_Id)
        {
            InitializeComponent();
            pubId = pub_Id;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            estado.Enabled = false;
            rubros = dbQueryHandler.cargarRubros();
            visibilidades = dbQueryHandler.cargarVisibilidades();

            foreach (var map in rubros)
            {
                rubro.Items.Add(map.Key);
            }
            foreach (var map in visibilidades)
            {
                tipoVisib.Items.Add(map.Key);
            }

            tipo.Items.Add("Subasta");
            tipo.Items.Add("Compra Inmediata");

            SqlDataReader dataReader = dbQueryHandler.cargarPublicacion(pubId.ToString());
            dataReader.Read();
            String visib = dataReader.GetDecimal(6).ToString();
            //String est = dataReader.GetInt32(7).ToString();
            String rub = dataReader.GetDecimal(8).ToString();
            String envios = dataReader.GetString(9);

            richTextBox1.Text = dataReader.GetString(0);
            stock.Text = dataReader.GetDecimal(1).ToString();
            precio.Text = dataReader.GetDecimal(4).ToString();
            fecCrea.Text = dataReader.GetDateTime(2).ToString("yyyy-MM-dd");
            fecVenc.Text = dataReader.GetDateTime(3).ToString("yyyy-MM-dd");
            tipo.Text = dataReader.GetString(5);

            if (tipo.Text == "Subasta") {
                button3.Text = "Ofertar";
                label11.Text = "Oferta";
            }else{
                button3.Text = "Comprar";
                label11.Visible = false;
                textBox1.Visible = false;

            }

            if (envios == "V")
                radioButton1.Checked = true;
            if (envios == "F")
                radioButton2.Checked = true;

            dataReader.Close();

            tipoVisib.Text = dbQueryHandler.cargarVisibilidad(visib);
            //estado.Text = dbQueryHandler.cargarEstado(est);
            rubro.Text = dbQueryHandler.cargarRubro(rub);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 result1 = 0;
            Int32 result2 = 0;
            Decimal precio2 = Decimal.Parse(precio.Text);
            


            if (tipo.Text == "Subasta")
            {
                Int32 precio1 = Int32.Parse(textBox1.Text);
               
                if (precio1 > precio2)
                {
                    result1 = dbQueryHandler.ofertarPub(textBox1.Text, pubId.ToString());
                    result2 = dbQueryHandler.updateOffer(textBox1.Text, pubId.ToString());

                    if (result1 > 0 && result2 > 0)
                    {
                        MessageBox.Show("Oferta correctamente realizada, ofertaste " + textBox1.Text + " pesos.");
                        this.Close();
                    }
                }
                else {
                    MessageBox.Show("El precio ofertado debe ser mayor al actual.");
                }
            }
            else
            {

                result1 = dbQueryHandler.crearFactura(precio.Text, pubId.ToString());

                Form3 f3 = new Form3(result1);

                f3.Show();

                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    class DbQueryHandlerModify
    {
        public SqlDataReader cargarPublicacion(String pubId)
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro,Publicacion_Acepta_Envio from GROUP_APROVED.publicaciones where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            return dataReader;
        }
        public Dictionary<string, string> cargarRubros()
        {
            SqlCommand cmd = new SqlCommand("select Id_Rubro,Rubro_Desc_Corta,Rubro_Desc_Completa from GROUP_APROVED.Rubros", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            var rubros = new Dictionary<string, string>();

            while (dataReader.Read())
            {
                rubros[dataReader.GetString(1)] = dataReader.GetDecimal(0).ToString();
            }

            dataReader.Close();

            return rubros;
        }

        public Dictionary<string, string> cargarVisibilidades()
        {
            SqlCommand cmd = new SqlCommand("select Visibilidad_Cod,Visibilidad_Desc from GROUP_APROVED.Visibilidades", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            var visibilidades = new Dictionary<string, string>();

            while (dataReader.Read())
            {
                visibilidades[dataReader.GetString(1)] = dataReader.GetDecimal(0).ToString();
            }

            dataReader.Close();

            return visibilidades;
        }

        public String cargarEstado(String Id)
        {
            SqlCommand cmd = new SqlCommand("select Descripcion from GROUP_APROVED.Estado_Publ where ID_Est = " + Id, DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            String estado = dataReader.GetString(0);
            dataReader.Close();
            return estado;

        }
        public String cargarVisibilidad(String Id)
        {
            SqlCommand cmd = new SqlCommand("select Visibilidad_Desc from GROUP_APROVED.Visibilidades where Visibilidad_Cod = " + Id, DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            String estado = dataReader.GetString(0);
            dataReader.Close();
            return estado;

        }
        public String cargarRubro(String Id)
        {
            SqlCommand cmd = new SqlCommand("select Rubro_Desc_Corta from GROUP_APROVED.Rubros where Id_Rubro = " + Id, DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            String estado = dataReader.GetString(0);
            dataReader.Close();
            return estado;

        }
        public Int32 updatePub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro, String estado, String pubId, String envios)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Desc = '" + desc + "',Publicacion_Stock= " + stock + ",Publicacion_Precio = " + precio + ",Publicacion_Tipo= '" + tipo + "',Visibilidad_Cod= " + visib + ",Publicacion_Estado= " + estado + ",Id_Rubro= " + Id_Rubro + ",Publicacion_Acepta_Envio = '" + envios + "' where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

            Int32 result = cmd.ExecuteNonQuery();

            return result;
        }
        public Int32 updatePub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro, String pubId, String envios)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Desc = '" + desc + "',Publicacion_Stock= " + stock + ",Publicacion_Precio = " + precio + ",Publicacion_Tipo= '" + tipo + "',Visibilidad_Cod= " + visib + ",Id_Rubro= " + Id_Rubro + ",Publicacion_Acepta_Envio = '" + envios + "' where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

            Int32 result = cmd.ExecuteNonQuery();

            return result;
        }
        public String getEstado(String est)
        {
            SqlCommand cmd = new SqlCommand("select Id_Est from GROUP_APROVED.Estado_Publ where Descripcion = '" + est + "'", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            String estado;

            var estados = new Dictionary<string, string>();

            dataReader.Read();
            estado = dataReader.GetInt32(0).ToString();
            dataReader.Close();

            return estado;

        }

         public Int32 ofertarPub(String precio, String pubId)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Precio = " + precio + " where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

            Int32 result = cmd.ExecuteNonQuery();

            return result;
        }

         public Int32 updateOffer(String precio, String pubId)
         {
             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Ofertas values(getdate()," + precio + ","+CurrentUser.user.getUserId().ToString()+"," + pubId + ")", DbConnection.connection.getdbconnection());

             Int32 result = cmd.ExecuteNonQuery();

             return result;
         }

         public Int32 crearFactura(String precio, String pubId)
         {
             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Facturas values(180048,getdate()," + precio.Replace(',','.') + ","+"'Efectivo', " + pubId + ");SELECT Nro_Fact FROM GROUP_APROVED.Facturas WHERE Nro_Fact = @@Identity", DbConnection.connection.getdbconnection());

             //Int32 result = (Int32)cmd.ExecuteScalar();
           
             
            Int32 result = cmd.ExecuteNonQuery();

             return 180048;

            
         }


    }
}

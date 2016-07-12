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
            String est = dataReader.GetInt32(7).ToString();
            String rub = dataReader.GetDecimal(8).ToString();
            String envios = dataReader.GetString(9);
            String preguntas = dataReader.GetString(10);

            richTextBox1.Text = dataReader.GetString(0);
            stock.Text = dataReader.GetDecimal(1).ToString();
            precio.Text = dataReader.GetDecimal(4).ToString();
            fecCrea.Text = dataReader.GetDateTime(2).ToString("yyyy-MM-dd");
            fecVenc.Text = dataReader.GetDateTime(3).ToString("yyyy-MM-dd");
            tipo.Text = dataReader.GetString(5);

            if (preguntas == "v") 
            {
                radioButton4.Checked = true;
            }
            else { radioButton3.Checked = true; }

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
            estado.Text = dbQueryHandler.cargarEstado(est);
            rubro.Text = dbQueryHandler.cargarRubro(rub);

            if (tipo.Text == "Subasta")
            {
                textBox2.Visible = false;
                label12.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 result1 = 0;
            Int32 result2 = 0;
            Decimal precio2 = Decimal.Parse(precio.Text);
            bool statusOK = true;

            int cant;

            if (!int.TryParse(textBox2.Text, out cant) && statusOK == true && tipo.Text == "Compra Inmediata")
            {
                statusOK = false;
                MessageBox.Show("La cantidad debe ser un numero entero.");
            }

            int cant2;

            if (!int.TryParse(textBox1.Text, out cant2) && statusOK == true && tipo.Text == "Subasta")
            {
                statusOK = false;
                MessageBox.Show("La cantidad ofertada debe ser un numero entero.");
            }


            if (dbQueryHandler.checkUser(pubId.ToString()) == true) {
                MessageBox.Show("No se puede comprar u ofertar a una publicación perteneciente al usuario.");
                statusOK = false;
            }

            if (textBox2.Text == "" && tipo.Text == "Compra Inmediata")
            {
                MessageBox.Show("Debe ingresar la cantidad de unidades a comprar.");
                statusOK = false;
            }
            else
            {

                
            }
            if (estado.Text == "Finalizada")
            {
                MessageBox.Show("No se puede comprar u ofertar una publicación que ha finalizado");
                statusOK = false;
            }

            if (estado.Text == "Pausada")
            {
                MessageBox.Show("No se puede comprar u ofertar una publicación pausada");
                statusOK = false;
            }

            if (statusOK == false)
            {
            }
            else
            {
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
                    else
                    {
                        MessageBox.Show("El precio ofertado debe ser mayor al actual.");
                    }
                }
                else
                {
                    String envio = "0";

                    if(radioButton1.Checked == true)
                    { 
                        envio = dbQueryHandler.getPrecioEnvio(tipoVisib.Text); 
                    }

                    Double total = Double.Parse(precio.Text);

                    Int32 unidades = Int32.Parse(textBox2.Text);

                    Double porcRecarga = dbQueryHandler.getPorcentajeRecarga(tipoVisib.Text) * 0.01;

                    Decimal recargo = Decimal.Add(Decimal.Parse((total * unidades * porcRecarga).ToString()), Decimal.Parse(envio.Replace('.', ',')));
            

                    String idCompra = dbQueryHandler.crearCompra(textBox2.Text, pubId.ToString());

                    String factId = dbQueryHandler.crearFactura(recargo.ToString(), pubId.ToString(), idCompra);

                    Decimal nroItem = dbQueryHandler.getNumeroItems(factId);

                    dbQueryHandler.crearItem(factId, (nroItem + 1).ToString(), (recargo - Decimal.Parse(envio.Replace('.', ','))).ToString(), unidades.ToString(), "Venta");

                    dbQueryHandler.actualizarPub(pubId.ToString(),textBox2.Text);

                    if (radioButton1.Checked == true)
                    {

                        nroItem = dbQueryHandler.getNumeroItems(factId);
                        dbQueryHandler.crearItem(factId, (nroItem + 1).ToString(), envio, "1", "Envio");
                    }

                    Form3 f3 = new Form3(factId);

                    f3.Show();
                    
                    this.Close();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
    class DbQueryHandlerModify
    {
        public SqlDataReader cargarPublicacion(String pubId)
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro,Publicacion_Acepta_Envio,Publicacion_Acepta_Preguntas from GROUP_APROVED.publicaciones where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());
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
                rubros[dataReader.GetString(2)] = dataReader.GetDecimal(0).ToString();
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
            SqlCommand cmd = new SqlCommand("select Rubro_Desc_Completa from GROUP_APROVED.Rubros where Id_Rubro = " + Id, DbConnection.connection.getdbconnection());
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

         public Int32 actualizarPub(String pubId, String cantidad)
         {
             SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Stock = Publicacion_Stock-" + cantidad + " where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

             Int32 result = cmd.ExecuteNonQuery();

             return result;
         }

         public Int32 updateOffer(String precio, String pubId)
         {
             DateTime myDateTime = DateTime.ParseExact(CustomDate.date.getDate(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

             String sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM");

             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Ofertas values('" + sqlFormattedDate + "'," + precio + "," + CurrentUser.user.getUserId().ToString() + "," + pubId + ")", DbConnection.connection.getdbconnection());
             
             Int32 result = cmd.ExecuteNonQuery();

             return result;
         }

         public String getPrecioEnvio(String visib)
         {
             SqlCommand cmd = new SqlCommand("select Visibilidad_Costo_Envio from GROUP_APROVED.Visibilidades where Visibilidad_Desc = '" + visib+"'", DbConnection.connection.getdbconnection());

             SqlDataReader dataReader = cmd.ExecuteReader();
             dataReader.Read();

             String result = dataReader.GetDecimal(0).ToString().Replace(',', '.');
             dataReader.Close();
             return result;
         }

         public Int32 getPorcentajeRecarga(String visib)
         {
             SqlCommand cmd = new SqlCommand("select Visibilidad_Costo_Venta from GROUP_APROVED.Visibilidades where Visibilidad_Desc = '" + visib + "'", DbConnection.connection.getdbconnection());

             SqlDataReader dataReader = cmd.ExecuteReader();
             dataReader.Read();

             Int32 result = dataReader.GetInt32(0);
             dataReader.Close();
             return result;
         }

         public String crearFactura(String precio, String pubId, String idCompra)
         {
             DateTime myDateTime = DateTime.ParseExact(CustomDate.date.getDate(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

             String sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM");

             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Facturas values('" + sqlFormattedDate + "'," + precio.Replace(',', '.') + "," + "'Efectivo', " + pubId + "," + idCompra + ");SELECT Nro_Fact FROM GROUP_APROVED.Facturas WHERE Nro_Fact = @@Identity", DbConnection.connection.getdbconnection());

             Decimal result = (Decimal)cmd.ExecuteScalar();



             return result.ToString();

            
         }

         public String crearCompra(String cantidad,String pubId)
         {
             DateTime myDateTime = DateTime.ParseExact(CustomDate.date.getDate(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

             String sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM");

             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Compras values('" + sqlFormattedDate + "'," + cantidad + "," + CurrentUser.user.getUserId() + ", " + pubId + ");SELECT ID_Compra FROM GROUP_APROVED.Compras WHERE ID_Compra = @@Identity", DbConnection.connection.getdbconnection());

             Decimal result = (Decimal)cmd.ExecuteScalar();



             return result.ToString();


         }

         public bool checkUser(String pubId)
         {
             SqlCommand cmd = new SqlCommand("select Id_Usuario from GROUP_APROVED.Publicaciones where Publicacion_Cod = "+pubId, DbConnection.connection.getdbconnection());

             SqlDataReader dataReader = cmd.ExecuteReader();
             Int32 id;
             bool ret = false;

             dataReader.Read();
             id = dataReader.GetInt32(0);
             dataReader.Close();

             if (id == CurrentUser.user.getUserId())
             {
                 ret = true;
             }
             else { ret = false; }

             return ret;
         }

         public String crearItem(String idFactura, String nroItem, String costo, String cantItems, String tipo)
         {
             SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.Items values(" + idFactura + "," + nroItem + "," + costo.Replace(',', '.') + "," + cantItems + ",'" + tipo + "')", DbConnection.connection.getdbconnection());

             Int32 result = cmd.ExecuteNonQuery();

             return result.ToString();

         }
         public Decimal getNumeroItems(String factId)
         {
             SqlCommand cmd = new SqlCommand("select max(Nro_Item) items from GROUP_APROVED.Items where Nro_Fact = " + factId, DbConnection.connection.getdbconnection());

             SqlDataReader dataReader = cmd.ExecuteReader();

             Decimal max = 0;
             if (dataReader.Read())
             {
                 if (!dataReader.IsDBNull(0))
                 {
                     max = dataReader.GetDecimal(0);
                 }
                    
             }
             else { max = 0; }
             dataReader.Close();
             return max;
         }


    }
}

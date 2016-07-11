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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class Form2 : Form
    {
        Int32 pubId;
        String type;
        DbQueryHandlerModify dbQueryHandler = new DbQueryHandlerModify();
        Dictionary<String,String> rubros;
        Dictionary<String, String> visibilidades;

        public Form2(Int32 pub_Id, String tipo)
        {
            InitializeComponent();
            pubId = pub_Id;
            type = tipo;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (type == "creacion")
            {
                button3.Visible = false;
            }
            if (type == "modificacion")
            {
                button1.Visible = false;
            }

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

            if (envios == "V")
                radioButton1.Checked = true;
            if (envios == "F")
                radioButton2.Checked = true;

            if (preguntas == "V")
                radioButton4.Checked = true;
            if (preguntas == "F")
                radioButton3.Checked = true;

            dataReader.Close();

            tipoVisib.Text = dbQueryHandler.cargarVisibilidad(visib);
            estado.Text = dbQueryHandler.cargarEstado(est);
            rubro.Text = dbQueryHandler.cargarRubro(rub);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strdesc = richTextBox1.Text;
            String strstock = stock.Text;
            StringBuilder sb = new StringBuilder(precio.Text);
            sb.Replace(",",".");
            String strprecio = sb.ToString();
            String strtipo = tipo.Text;
            String strvisib = visibilidades[tipoVisib.Text].ToString();
            String strrubro = rubros[rubro.Text].ToString();
            String strestado = dbQueryHandler.getEstado("Activa");
            String strenvios = "V";
            String strpreguntas = "V";

            if (radioButton2.Checked == true)
                strenvios = "F";
            if (radioButton3.Checked == true)
                strpreguntas = "F";
            


            Int32  result = dbQueryHandler.updatePub(strdesc,strstock,strprecio,strtipo,strvisib,strrubro,strestado,pubId.ToString(),strenvios,strpreguntas);

            

           if (result > 0)
            {
                MessageBox.Show("Publicacion correctamente activada");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fecVenc_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String strdesc = richTextBox1.Text;
            String strstock = stock.Text;
            StringBuilder sb = new StringBuilder(precio.Text);
            sb.Replace(",", ".");
            String strprecio = sb.ToString();
            String strtipo = tipo.Text;
            String strvisib = visibilidades[tipoVisib.Text].ToString();
            String strrubro = rubros[rubro.Text].ToString();
            String strenvios = "V";
            String strpreguntas = "V";

            if (radioButton2.Checked == true)
                strenvios = "F";

            if (radioButton3.Checked == true)
                strpreguntas = "F";

            Int32 result = dbQueryHandler.updatePub(strdesc, strstock, strprecio, strtipo, strvisib, strrubro, pubId.ToString(), strenvios, strpreguntas);



            if (result > 0)
            {
                MessageBox.Show("Publicacion correctamente modificada");
                this.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

    class DbQueryHandlerModify {
        public SqlDataReader cargarPublicacion(String pubId)
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro,Publicacion_Acepta_Envio,Publicacion_Acepta_Preguntas from GROUP_APROVED.publicaciones where Publicacion_cod = " + pubId,DbConnection.connection.getdbconnection());
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
            SqlCommand cmd = new SqlCommand("select Descripcion from GROUP_APROVED.Estado_Publ where ID_Est = " + Id , DbConnection.connection.getdbconnection());
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
        public Int32 updatePub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro, String estado, String pubId, String envios, String preguntas)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Desc = '" + desc + "',Publicacion_Stock= " + stock + ",Publicacion_Precio = " + precio + ",Publicacion_Tipo= '" + tipo + "',Visibilidad_Cod= " + visib + ",Publicacion_Estado= " + estado + ",Id_Rubro= " + Id_Rubro + ",Publicacion_Acepta_Envio = '" + envios + "',Publicacion_Acepta_Preguntas = '" + preguntas + "' where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

            Int32 result = cmd.ExecuteNonQuery();

            return result;
        }
        public Int32 updatePub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro, String pubId, String envios,String preguntas)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Desc = '" + desc + "',Publicacion_Stock= " + stock + ",Publicacion_Precio = " + precio + ",Publicacion_Tipo= '" + tipo + "',Visibilidad_Cod= " + visib + ",Id_Rubro= " + Id_Rubro + ",Publicacion_Acepta_Envio = '" + envios + "'"+ ",Publicacion_Acepta_Preguntas = '" + preguntas + "' where Publicacion_cod = " + pubId, DbConnection.connection.getdbconnection());

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
    }
}

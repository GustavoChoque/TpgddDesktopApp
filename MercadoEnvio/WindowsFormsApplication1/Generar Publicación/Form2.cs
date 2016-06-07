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
        DbQueryHandlerModify dbQueryHandler = new DbQueryHandlerModify();
        Dictionary<String,String> rubros;
        Dictionary<String, String> visibilidades;

        public Form2(Int32 pub_Id)
        {
            InitializeComponent();
            pubId = pub_Id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
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
            tipo.Items.Add("Compra directa");

            SqlDataReader dataReader = dbQueryHandler.cargarPublicacion(pubId.ToString());
            dataReader.Read();
            String visib = dataReader.GetDecimal(6).ToString();
            String est = dataReader.GetInt32(7).ToString();
            String rub = dataReader.GetDecimal(8).ToString();

            richTextBox1.Text = dataReader.GetString(0);
            stock.Text = dataReader.GetDecimal(1).ToString();
            precio.Text = dataReader.GetDecimal(4).ToString();
            fecCrea.Text = dataReader.GetDateTime(2).ToString("yyyy-MM-dd");
            fecVenc.Text = dataReader.GetDateTime(3).ToString("yyyy-MM-dd");
            tipo.Text = dataReader.GetString(5);
            dataReader.Close();

            tipoVisib.Text = dbQueryHandler.cargarVisibilidad(visib);
            estado.Text = dbQueryHandler.cargarEstado(est);
            rubro.Text = dbQueryHandler.cargarRubro(rub);
            
        }
    }

    class DbQueryHandlerModify {
        public SqlDataReader cargarPublicacion(String pubId)
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicaicon_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro from GROUP_APROVED.publicaciones where Publicacion_cod = " + pubId,DbConnection.connection.getdbconnection());
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
    }
}

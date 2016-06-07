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
    public partial class CrearPublicacion : Form
    {
        DbQueryHandlerCreate dbQueryHandler = new DbQueryHandlerCreate();
        Dictionary<String,String> rubros;
        Dictionary<String, String> visibilidades;

        public CrearPublicacion()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            String desc = richTextBox1.Text;
            String stock = textBox3.Text;
            String precio = textBox1.Text;
            String tipo = comboBox2.Text;
            String visib = visibilidades[comboBox3.Text].ToString();
            String rubro = rubros[comboBox4.Text].ToString();
            String estado = dbQueryHandler.cargarEstado();
            
            
            Int32 pubId = dbQueryHandler.createPub(desc, stock,precio,tipo,visib,rubro,estado);

            if (pubId > 0)
            {
                MessageBox.Show("Publicacion creada correctamente");
                Form2 f2 = new Form2(pubId);
                f2.Show();
                this.Close();
            }
        }

        private void CrearPublicacion_Load(object sender, System.EventArgs e)
        {

            rubros = dbQueryHandler.cargarRubros();
            visibilidades = dbQueryHandler.cargarVisibilidades();
            foreach (var map in rubros)
            {
                comboBox4.Items.Add(map.Key);
            }
            foreach (var map in visibilidades)
            {
                comboBox3.Items.Add(map.Key);
            }

            comboBox2.Items.Add("Subasta");
            comboBox2.Items.Add("Compra directa");
        }
    }
   
    class DbQueryHandlerCreate {

        public Int32 createPub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro,String estado)
        {

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");
            DateTime myDateTime2 = DateTime.Now.AddDays(30);

            string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.publicaciones values ('" + desc + "'," + stock + ",'" + sqlFormattedDate + "','" + sqlFormattedDate2 + "'," + precio + ",'" + tipo + "'," + visib + "," + estado + "," + Id_Rubro + "," + CurrentUser.user.getUserId() + ");SELECT Publicacion_Cod FROM GROUP_APROVED.Publicaciones WHERE Publicacion_Cod = @@Identity", DbConnection.connection.getdbconnection());
            Int32 Publicacion_Cod = (Int32)cmd.ExecuteScalar();

            return Publicacion_Cod;
        }

        public Dictionary<string, string> cargarRubros()
        {
             SqlCommand cmd = new SqlCommand("select Id_Rubro,Rubro_Desc_Corta,Rubro_Desc_Completa from GROUP_APROVED.Rubros",DbConnection.connection.getdbconnection());
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

        public String cargarEstado()
        {
            SqlCommand cmd = new SqlCommand("select Id_Est from GROUP_APROVED.Estado_Publ where Descripcion = 'Borrador'", DbConnection.connection.getdbconnection());
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

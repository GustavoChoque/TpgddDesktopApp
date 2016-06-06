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
            String visib = comboBox3.Text;
            String rubro = comboBox4.Text;
            String estado = "borrador";

            
            textBox2.Text = dbQueryHandler.createPub(desc, stock,precio,tipo,visib,rubro,estado);
        }

        private void CrearPublicacion_Load(object sender, System.EventArgs e)
        {
            for (int i = 0; i == 99; i++)
                comboBox1.Items.Add(i);

            var rubros = dbQueryHandler.cargarRubros();
            foreach (var map in rubros)
            {
                comboBox4.Items.Add(map.Key);
            }

            comboBox3.Items.Add("20");
            comboBox2.Items.Add("Subasta");
        }
    }
   
    class DbQueryHandlerCreate {

        public String createPub(String desc, String stock, String precio, String tipo, String visib, String Id_Rubro,String estado)
        {

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime myDateTime2 = DateTime.Now.AddDays(30);

            string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss");
            SqlCommand cmd = new SqlCommand("insert into GROUP_APROVED.publicaciones values (" + desc + "," + stock + "," + sqlFormattedDate + "," + sqlFormattedDate2 + "," + precio + "," + tipo + "," + visib + "," + estado + "," + Id_Rubro + "," + CurrentUser.user.getUserId(),DbConnection.connection.getdbconnection());
            return "insert into GROUP_APROVED.publicaciones values (" + desc + "," + stock + "," + sqlFormattedDate + "," + sqlFormattedDate2 + "," + precio + "," + tipo + "," + visib + "," + estado + "," + Id_Rubro + "," + CurrentUser.user.getUserId();
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
    }
}

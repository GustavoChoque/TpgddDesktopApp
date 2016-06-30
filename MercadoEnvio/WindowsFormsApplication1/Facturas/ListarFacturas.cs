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
namespace WindowsFormsApplication1.Facturas
{
    public partial class ListarFacturas : Form
    {
        DbQueryHandlerListarFacturas dbQueryHandler = new DbQueryHandlerListarFacturas();
        public ListarFacturas()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string texto = textBox1.Text;
            SqlDataReader registros = dbQueryHandler.getFacturas(texto);
            while (registros.Read())
            {
                //dataGridView1.Rows.Add(registros["Nro_Fact"].ToString(), registros["Fact_Fecha"].ToString(), registros["Fact_Total"].ToString(), registros["Fact_Forma_Pago"].ToString());
                dataGridView1.Rows.Add(registros["Publicacion_Cod"].ToString());
            }
            registros.Close(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ListarFacturas_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Maximo");
            comboBox1.Items.Add("15000-50000");
            comboBox1.Items.Add("5000-15000");
            comboBox1.Items.Add("0-5000");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("Historico");
            comboBox2.Items.Add("ultimos 30 dias");
            comboBox2.Items.Add("ultimos 15 dias");
            comboBox2.Items.Add("ultimos 2 dias");
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class DbQueryHandlerListarFacturas
    {
        public SqlDataReader getFacturas(string texto) {
            //string cadena = "select Nro_Fact,Fact_Fecha,Fact_Total,Fact_Forma_Pago from GROUP_APROVED.Facturas where Fact_Forma_Pago LIKE '%" + texto + "%'";
            string cadena = "select Publicacion_Cod from GROUP_APROVED.Publicaciones";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            SqlDataReader registros = comando.ExecuteReader();
            return registros;
        }
    }
}

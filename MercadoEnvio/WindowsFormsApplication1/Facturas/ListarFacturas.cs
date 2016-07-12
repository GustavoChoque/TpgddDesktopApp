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
        public int currentPageNumber = 1;
        public const int PAGE_SIZE = 5;
        double totalRows;
        SqlDataAdapter ad;
        DateTime fechaActual = Convert.ToDateTime(CustomDate.date.getDate());
        public ListarFacturas()
        {
            InitializeComponent();
            panel1.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ad = null;
            dataGridView1.DataSource = null;
            currentPageNumber = 1;
            
            BindData();
            panel1.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            panel1.Hide();

           
        }

        private void ListarFacturas_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Todo");
            comboBox1.Items.Add("15000-50000");
            comboBox1.Items.Add("5000-15000");
            comboBox1.Items.Add("0-5000");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("Historico");
            comboBox2.Items.Add("ultimos 6 meses");
            comboBox2.Items.Add("ultimos 3 meses");
            comboBox2.Items.Add("ultimos 30 dias");
            comboBox2.Items.Add("ultimos 15 dias");
            comboBox2.Items.Add("ultimos 2 dias");
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPageNumber > 1)
            {
                currentPageNumber = currentPageNumber - 1;
                BindData();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentPageNumber<=totalRows)
            {
            currentPageNumber =currentPageNumber+ 1;
            BindData();
            }
        }
            private void BindData()
        {
                string textoABuscar=textBox1.Text;
                int importeInicio = 0;
                int importeFin=0;
                int dias=0;
                switch(comboBox1.Text){
                    case "0-5000": importeInicio = 0;
                        importeFin = 5000;
                        break;
                    case "5000-15000": importeInicio = 5000;
                        importeFin = 15000;
                        break;
                    case "15000-50000": importeInicio = 15000;
                        importeFin = 50000;
                        break;
                    default: importeInicio = 0;
                        importeFin = 99999999;
                        break;
                }
                switch (comboBox2.Text)
                {
                    case "ultimos 2 dias": dias = 2;
                        break;
                    case "ultimos 15 dias": dias = 15;
                        break;
                    case "ultimos 30 dias": dias = 30;
                        break;
                    case "ultimos 3 meses": dias = 90;
                        break;
                    case "ultimos 6 meses": dias = 180;
                        break;
                    default: dias = 36500;
                        break;
                }

                SqlCommand myCommand = new SqlCommand("GROUP_APROVED.consultarFacturas", DbConnection.connection.getdbconnection());
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.AddWithValue("@startRowIndex",currentPageNumber);
            myCommand.Parameters.AddWithValue("@maximumRows", PAGE_SIZE);
            myCommand.Parameters.AddWithValue("@idUsuario", CurrentUser.user.getUserId());
            myCommand.Parameters.AddWithValue("@textoABuscar", textoABuscar);
            myCommand.Parameters.AddWithValue("@importeInicio", importeInicio);
            myCommand.Parameters.AddWithValue("@importeFin", importeFin);
            myCommand.Parameters.AddWithValue("@dias", dias);
            myCommand.Parameters.AddWithValue("@fechaActual", fechaActual);
            myCommand.Parameters.Add("@totalRows", SqlDbType.Int, 4);
            myCommand.Parameters["@totalRows"].Direction = ParameterDirection.Output;

             ad = new SqlDataAdapter(myCommand);

          
            DataTable ds = new DataTable();
            
            ad.Fill(ds);
            
            dataGridView1.DataSource = ds;
            //totalRows = (int)myCommand.Parameters["@totalRows"].Value;
            label5.Text = currentPageNumber.ToString(); 
            label7.Text = CalculateTotalPages(totalRows+1).ToString();

        }
        private int CalculateTotalPages(double totalRows)
        {
            int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);

            return totalPages;
        }
        public void Limpiar() {
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            ad = null;
            dataGridView1.DataSource = null;
            currentPageNumber = 1;
            
        }
    }
    public class DbQueryHandlerListarFacturas
    {
        
    }
}

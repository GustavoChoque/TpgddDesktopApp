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
namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class Historial : Form
    {
        DbQueryHandlerHistorial dbQueryHandler = new DbQueryHandlerHistorial();
        
        public int currentPageNumber = 1;
        public const int PAGE_SIZE = 10;
        double totalRows;
        public Historial()
        {
            InitializeComponent();
            
        }

        private void salir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Historial_Load(object sender, EventArgs e)
        {
           
            BindData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPageNumber > 1)
            {
                currentPageNumber = currentPageNumber - 1;
                BindData();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentPageNumber<=totalRows)
            {
            currentPageNumber =currentPageNumber+ 1;
            BindData();
        }
            
        }
        private void BindData()
        {


            SqlCommand myCommand = new SqlCommand("GROUP_APROVED.paginacionHistorial", DbConnection.connection.getdbconnection());
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.AddWithValue("@startRowIndex",currentPageNumber);
            myCommand.Parameters.AddWithValue("@maximumRows", PAGE_SIZE);
            myCommand.Parameters.AddWithValue("@idUsuario", CurrentUser.user.getUserId());
            myCommand.Parameters.Add("@totalRows", SqlDbType.Int, 4);
            myCommand.Parameters["@totalRows"].Direction = ParameterDirection.Output;

            SqlDataAdapter ad = new SqlDataAdapter(myCommand);

          
            DataTable ds = new DataTable();
            
            ad.Fill(ds);
            
            dataGridView1.DataSource = ds;
            totalRows = (int)myCommand.Parameters["@totalRows"].Value;
            label1.Text = currentPageNumber.ToString(); 
            label2.Text = CalculateTotalPages(totalRows+1).ToString();

        }
        private int CalculateTotalPages(double totalRows)
        {
            int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);

            return totalPages;
        }
    }
    public class DbQueryHandlerHistorial
    {
        
    }
}

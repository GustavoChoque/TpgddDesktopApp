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
    public partial class Form3 : Form
    {

        String factId;
        dbQueryHandlerFact dbQueryHandler = new dbQueryHandlerFact();

         public Form3(String fact_Id)
        {
            InitializeComponent();
            factId = fact_Id;

            SqlDataReader dataReader = dbQueryHandler.cargarFactura(factId);

            dataReader.Read();

            label7.Text = dataReader.GetDecimal(0).ToString();
            label8.Text = dataReader.GetDateTime(1).ToString();
            label9.Text = dataReader.GetDecimal(2).ToString();
            label10.Text = dataReader.GetString(3);
            label11.Text = dataReader.GetInt32(4).ToString();

            dataReader.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }

    class dbQueryHandlerFact {

        public SqlDataReader cargarFactura(String idfact)
        {
            SqlCommand cmd = new SqlCommand("select Nro_Fact, Fact_Fecha, Fact_Total, Fact_Forma_Pago, Publicacion_Cod FROM GROUP_APROVED.Facturas where Nro_Fact = " +idfact, DbConnection.connection.getdbconnection());

            SqlDataReader dataReader = cmd.ExecuteReader();

            return dataReader;
        }
    
    }
}

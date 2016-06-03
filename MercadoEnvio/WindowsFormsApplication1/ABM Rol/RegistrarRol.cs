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
using System.Text.RegularExpressions;
namespace WindowsFormsApplication1.ABM_Rol

{
    public partial class RegistrarRol : Form
    {

        DbQueryHandler dbQueryHandler = new DbQueryHandler();
        List<Int32> listaFuncionesId = new List<Int32>();

        public RegistrarRol()
        {
            InitializeComponent();
            label4.Text = "";
            label4.ForeColor = System.Drawing.Color.Red;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SqlDataReader registros = dbQueryHandler.getFunctions();
 
            while (registros.Read())
            {
                listBox1.Items.Add(registros["desc_Func"].ToString());
                listaFuncionesId.Add(Convert.ToInt32(registros["Id_Func"]));
                
            }

            registros.Close();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool statusOK = true;
            int selectedItems = 0;
            label4.Text = "";

            List<Int32> funcionesSeleccionadas = new List<Int32>();
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matches = regex.Matches(nombreRol.Text);

            List<String> listaFunciones = new List<String>();

            foreach (object o in listBox1.SelectedItems)
            {
                funcionesSeleccionadas.Add(listaFuncionesId[listBox1.Items.IndexOf(o)]);
                selectedItems++;
            }

            if (matches.Count == 0 )
            {
                
                label4.Text = "El nombre del rol debe ser una letra.";
                statusOK = false;
            }
            if (selectedItems == 0)
            {
                label4.Text = "Debe seleccionar al menos una función.";
                statusOK = false;
            }

            if (statusOK)
            {
                string desc = nombreRol.Text;

                dbQueryHandler.registrarRol(desc, funcionesSeleccionadas);

                MessageBox.Show("Los datos se guardaron correctamente");
                nombreRol.Text = "";
                //comboBox1.SelectedIndex = 0;
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void faltaFuncion_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    public class DbQueryHandler
    {
        
        public SqlDataReader getFunctions()
        {
            SqlCommand comando = new SqlCommand("Select Id_func, Desc_Func FROM GROUP_APROVED.Funciones", DbConnection.connection.getdbconnection());

            SqlDataReader registros = comando.ExecuteReader();

            return registros;

        }

        public void registrarRol(String desc, List<Int32> funciones)
        {

            SqlCommand comando = new SqlCommand("insert into GROUP_APROVED.Roles (desc_Rol) values('" + desc + "'); SELECT Id_Rol FROM GROUP_APROVED.Roles WHERE Id_Rol = @@Identity", DbConnection.connection.getdbconnection());
            Int32 id_Rol = (Int32)comando.ExecuteScalar();

            foreach (Int32 funcionId in funciones)
            {
                comando = new SqlCommand("insert into GROUP_APROVED.FuncionesxRol (id_Rol,id_Func) values(" + id_Rol + "," + funcionId + ")", DbConnection.connection.getdbconnection());
                comando.ExecuteNonQuery();
            }
                
       

            }
        }
    }


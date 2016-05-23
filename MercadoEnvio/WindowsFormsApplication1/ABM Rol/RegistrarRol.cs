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
namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class RegistrarRol : Form
    {
        public RegistrarRol()
        {
            InitializeComponent();
            label3.Text = "";
            label4.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           SqlConnection conexion=new SqlConnection("data source = .\\SQLSERVER2012; database = test;user = gd; password = gd2016");
           SqlCommand comando;
           conexion.Open();
            
            comboBox1.Items.Add("Seleccione un valor");
            string cadena="Select desc_Func FROM funciones";
            comando=new SqlCommand(cadena,conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read()) {
                comboBox1.Items.Add(registros["desc_Func"].ToString());
            }
               
            comboBox1.SelectedIndex = 0;
            conexion.Close();
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
            int cont=0;
            int idRol=0;
            int idFuncion=0;

            if (nombreRol.Text == "") {
                label3.ForeColor = System.Drawing.Color.Red;
                label3.Text = "Falta nombre de rol";
                cont++;
            }
            if (comboBox1.Text == "Seleccione un valor") {
                label4.ForeColor = System.Drawing.Color.Red;
                label4.Text = "Elija una funcion";
                cont++;
            }
            if (cont == 0) {
                SqlConnection conexion = new SqlConnection("data source = .\\SQLSERVER2012; database = test;user = gd; password = gd2016");
                SqlCommand comando,comando2,comando3,comando4;
                conexion.Open();
                string desc = nombreRol.Text;
                string funcion = comboBox1.Text;
                string cadena = "insert into roles (desc_Rol) values('"+desc+"')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();

                conexion.Open();
                string cadena2 = "Select id_Func FROM funciones where desc_Func='"+funcion+"'";
                comando2 = new SqlCommand(cadena2, conexion);
                SqlDataReader registros = comando2.ExecuteReader();
                 while(registros.Read()){
                     idFuncion = Convert.ToInt32(registros["id_Func"]);
                 }
                 registros.Close();
                 conexion.Close();

                 conexion.Open();
                 string cadena3 = "Select id_Rol FROM roles where desc_Rol='" + desc + "'";
                 comando3 = new SqlCommand(cadena3, conexion);
                 registros = comando3.ExecuteReader();
                 while (registros.Read())
                 {
                     idRol = Convert.ToInt32(registros["id_Rol"]);
                 }
                 registros.Close();
                conexion.Close();

                conexion.Open();
                 string cadena4 = "insert into funcionesxRol (id_Rol,id_Func) values("+idRol+ ","+idFuncion+")";
                 comando4 = new SqlCommand(cadena4, conexion);
                 comando4.ExecuteNonQuery();

                MessageBox.Show("Los datos se guardaron correctamente");
                nombreRol.Text = "";
                comboBox1.SelectedIndex = 0;
                conexion.Close();
                }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void faltaFuncion_Click(object sender, EventArgs e)
        {

        }
    }
}

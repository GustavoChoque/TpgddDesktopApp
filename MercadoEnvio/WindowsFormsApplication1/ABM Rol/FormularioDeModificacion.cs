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
    public partial class FormularioDeModificacion : Form
    {
        DbQueryHandlerFormModificacion dbQueryHandler = new DbQueryHandlerFormModificacion();
        string datoIdRol;
        public FormularioDeModificacion()
        {
            InitializeComponent();
            
           }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void FormularioDeModificacion_Load(object sender, EventArgs e)
        {

        }
        //me trae info del formulario listadeseleccion.cs
        public void getIdRol(string id) {         
            datoIdRol = id;           
        }

        private void button1_Click(object sender, EventArgs e)
        {   //"checked" indica si esta seleccionado el radioButton       
            int idFuncion = 0;
            string funcion;
            string desc = textBox1.Text;
            //dbQueryHandler.modificarNombreRol(desc,datoIdRol);
            if ((!radioButton1.Checked && !radioButton2.Checked) && (textBox1.ReadOnly==false)) {
                dbQueryHandler.modificarNombreRol(desc, datoIdRol);
                MessageBox.Show("Los cambios se efectuaron correctamente");
                this.Close();
            }   
            if (radioButton1.Checked && comboBox1.Text!="Seleccione un valor") {
                //agrega una funcion 
                dbQueryHandler.modificarNombreRol(desc, datoIdRol);
                funcion = comboBox1.Text;
                idFuncion=dbQueryHandler.getIdFuncion(funcion);
                dbQueryHandler.agregarFuncionARol(datoIdRol, idFuncion);
                MessageBox.Show("Los cambios se efectuaron correctamente");
                this.Close();
            }
            if (radioButton2.Checked && comboBox1.Text != "Seleccione un valor")
            {//borrar una funcion
                dbQueryHandler.modificarNombreRol(desc, datoIdRol);
                funcion = comboBox1.Text;
                idFuncion = dbQueryHandler.getIdFuncion(funcion);
                dbQueryHandler.eliminarFuncionDeRol(datoIdRol, idFuncion);
                MessageBox.Show("Los cambios se efectuaron correctamente");
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Seleccione un valor");
            SqlDataReader registros = dbQueryHandler.getFunciones(datoIdRol, false);
            while (registros.Read())
            {
                comboBox1.Items.Add(registros["Desc_Func"].ToString());
            }
            registros.Close();
            comboBox1.SelectedIndex = 0; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Seleccione un valor");
            SqlDataReader registros = dbQueryHandler.getFunciones(datoIdRol, true);
            while (registros.Read())
            {
                comboBox1.Items.Add(registros["Desc_Func"].ToString());
            }
            registros.Close();
            comboBox1.SelectedIndex = 0;   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
        }
    }
    public class DbQueryHandlerFormModificacion
    {
        public void modificarNombreRol(string desc,string datoIdRol) {
            SqlCommand comando;
            string cadena = "update GROUP_APROVED.Roles Set Desc_Rol ='" + desc + "' where Id_Rol='" + datoIdRol + "'";
            comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
        }
        public int getIdFuncion(string funcion) {
            int id=0;
            string cadena2 = "Select Id_Func FROM GROUP_APROVED.Funciones where Desc_Func='" + funcion + "'";
            SqlCommand comando2 = new SqlCommand(cadena2, DbConnection.connection.getdbconnection());
            SqlDataReader registros = comando2.ExecuteReader();
            while (registros.Read())
            {
                id = Convert.ToInt32(registros["Id_Func"]);
            }
            registros.Close();
            return id;    
        }
        public void agregarFuncionARol(string datoIdRol,int idFuncion) {
            string cadena = "insert into GROUP_APROVED.FuncionesxRol  (Id_Rol,Id_Func) values(" + datoIdRol + "," + idFuncion + ")";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();            
            }
        public void eliminarFuncionDeRol(string datoIdRol, int idFuncion)
        {
            string cadena = "Delete From GROUP_APROVED.FuncionesxRol where Id_Rol='" + datoIdRol + "' And Id_Func='" + idFuncion + "'";
            SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
        }
        public SqlDataReader getFunciones(string datoIdRol,bool contiene) {
            string cadena;
            if (contiene == false)
            {
                cadena = "Select Desc_Func FROM GROUP_APROVED.Funciones where Id_Func not In (select Id_Func from GROUP_APROVED.FuncionesxRol where Id_Rol =" + datoIdRol + ")";
                SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
                SqlDataReader registros = comando.ExecuteReader();
                return registros;
            }
            else {
                cadena = "Select Desc_Func FROM GROUP_APROVED.Funciones where Id_Func In (select Id_Func from GROUP_APROVED.FuncionesxRol where Id_Rol =" + datoIdRol + ")";
                SqlCommand comando = new SqlCommand(cadena, DbConnection.connection.getdbconnection());
                SqlDataReader registros = comando.ExecuteReader();
                return registros;
            }
        }
    }
}

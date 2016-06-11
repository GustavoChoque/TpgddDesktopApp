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
using System.Globalization;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class FormularioModificacionCliente : Form
    {
        public string ID;
        public string usuario;
        DbQueryHandlerPantallaModifUsuarioCliente dbQueryHandler = new DbQueryHandlerPantallaModifUsuarioCliente();
        List<String> datos;

        public FormularioModificacionCliente(string id, string usuarioRecibido)
        {
            InitializeComponent();
            ID = id;
            usuario = usuarioRecibido;
            datos = dbQueryHandler.buscarDatos(ID);
            llenarTextBoxs(datos);
        }

        private void llenarTextBoxs(List<string> datos)
        {
            textBox1.Text = datos[12];
            textBox2.Text = usuario;
            textBox3.Text = datos[0];
            comboBox1.SelectedItem = datos[1];
            textBox5.Text = datos[2];
            textBox6.Text = datos[3];
            dateTimePicker1.Value = DateTime.ParseExact(datos[4], "yyyy/dd/MM HH:mm:ss", CultureInfo.InvariantCulture);
            textBox8.Text = datos[5];
            textBox9.Text = datos[6];
            textBox10.Text = datos[7];
            textBox11.Text = datos[8];
            textBox12.Text = datos[9];
            textBox13.Text = datos[10];
            textBox14.Text = datos[11];
            
        }

        private void FormularioModificacionCliente_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            llenarTextBoxs(datos);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            datos = null;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarLlenado())
            {
                List<String> datosModificados = new List<string>();
                grabarDatosNuevosEnLista(datosModificados);
                int mjeRta = dbQueryHandler.grabarDatos(datosModificados,ID);
            }
        }

        private bool verificarLlenado()
        {
            string mensajeFalla = "";
            bool rta = true;
            if ((textBox3.Text == "") || (tieneLetras(textBox3.Text))) { mensajeFalla = mensajeFalla + "\nNro de documento inválido"; rta = false; };
            if ((comboBox1.SelectedIndex) == (-1)) { mensajeFalla = mensajeFalla + "\nTipo de documento inválido"; rta = false; };
            if ((textBox5.Text == "") || tieneNumeros(textBox5.Text)) { mensajeFalla = mensajeFalla + "Nombre inválido"; rta = false; };
            if ((textBox6.Text == "") || tieneNumeros(textBox6.Text)) { mensajeFalla = mensajeFalla + "\nApellido inválido"; rta = false; };
            if ((textBox8.Text == "") || (tieneLetras(textBox8.Text))) { mensajeFalla = mensajeFalla + "\nTeléfono inválido"; rta = false; };
            if ((textBox9.Text == "") || !(textBox9.Text.Contains('@'))) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false; };
            if (textBox10.Text == "") { mensajeFalla = mensajeFalla + "\nNombre de calle inválido"; rta = false; };
            if ((textBox11.Text == "") || (tieneLetras(textBox11.Text))) { mensajeFalla = mensajeFalla + "\nNro de calle inválido"; rta = false; };
            if ((textBox12.Text == "") || (tieneLetras(textBox12.Text))) { mensajeFalla = mensajeFalla + "\nNro de piso inválido"; rta = false; };
            if (textBox13.Text == "") { mensajeFalla = mensajeFalla + "\nDepartamento inválido"; rta = false; };
            if ((textBox14.Text == "") || (tieneLetras(textBox14.Text))) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if (!(DateTime.Compare(dateTimePicker1.Value, DateTime.Now) <= 0)) { mensajeFalla = mensajeFalla + "\nFecha de nacimiento inválida"; rta = false; };
            if ((!(textBox3.Text == "") && !(tieneLetras(textBox3.Text)) && !((comboBox1.SelectedIndex) == (-1))))
            {
                if (!verificarUnicos(textBox3.Text, comboBox1.Text)) { rta = false; mensajeFalla = mensajeFalla + "\nDocumento y tipo de documento registrados"; };
            };

            if (rta == false) { MessageBox.Show("Errores detectados:\n\n" + mensajeFalla); }
            return rta;
        }

        private bool tieneNumeros(string texto)
        {
            return (texto.Contains('1') || texto.Contains('2') || texto.Contains('3') || texto.Contains('4')
                || texto.Contains('5') || texto.Contains('6') || texto.Contains('7') || texto.Contains('8')
                || texto.Contains('9') || texto.Contains('0'));
        }

        private bool tieneLetras(string texto)
        {
            string letras = "abcdefghijklmmñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            bool rta = false;
            foreach (char letra in texto)
            {
                if (letras.Contains(letra)) { rta = true; return rta; };
            }
            return rta;
        }

        private bool verificarUnicos(string nrodoc, string tipodoc)
        {
            return dbQueryHandler.verificarDocumento(nrodoc, tipodoc);
        }

        private void grabarDatosNuevosEnLista(List<string> datosModificados)
        {
            datosModificados[0]= textBox3.Text;
            datosModificados[1] = comboBox1.SelectedItem.ToString();
            datosModificados[2]= textBox5.Text;
            datosModificados[3]= textBox6.Text;
            datosModificados[4]= dateTimePicker1.Value.ToString();
            datosModificados[5]= textBox8.Text;
            datosModificados[6]= textBox9.Text;
            datosModificados[7]= textBox10.Text;
            datosModificados[8]= textBox11.Text;
            datosModificados[9]= textBox12.Text;
            datosModificados[10]= textBox13.Text;
            datosModificados[11]= textBox14.Text;
            datosModificados[12]= textBox1.Text;
        }
    }
    class DbQueryHandlerPantallaModifUsuarioCliente 
    {
        SqlCommand comando;
        public List<String> buscarDatos(string id)
        {
            comando = new SqlCommand(@"select Dni_Cli, Tipo_Dni, Cli_Nombre,Cli_Apellido,Cli_Fecha_Nac,
            CLI_Telefono,Cli_Mail,Cli_Dom_Calle,Cli_Nro_Calle,Cli_Piso,Cli_Depto,Cli_Cod_Postal,Id_Usuario
            from GROUP_APROVED.Clientes where Id_Usuario = '"+id+"'", DbConnection.connection.getdbconnection());
            SqlDataReader lector = comando.ExecuteReader();
            lector.Read();
            List<string> datos = new List<string>();
            //problemas con la lista de string... dice que esta fuera del intervalo... ver bien como poronga se usa
            datos[0]=lector.GetValue(0).ToString();
            datos[1]=lector.GetValue(1).ToString();
            datos[2]=lector.GetValue(2).ToString();
            datos[3]=lector.GetValue(3).ToString();
            datos[4]=lector.GetValue(4).ToString();
            datos[5]=lector.GetValue(5).ToString();
            datos[6]=lector.GetValue(6).ToString();
            datos[7]=lector.GetValue(7).ToString();
            datos[8]=lector.GetValue(8).ToString();
            datos[9]=lector.GetValue(9).ToString();
            datos[10]=lector.GetValue(10).ToString();
            datos[11]=lector.GetValue(11).ToString();
            datos[12]=lector.GetValue(12).ToString();
            

            return datos;
        }

        public int grabarDatos(List<string> datosModificados, string id)
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand(@"update GROUP_APROVED.Clientes SET Dni_Cli = "+datosModificados[3]+@", Tipo_Dni = "+datosModificados[4]+@",
            Cli_Nombre="+datosModificados[5]+@",Cli_Apellido="+datosModificados[6]+@",Cli_Fecha_Nac="+datosModificados[7]+@",CLI_Telefono,Cli_Mail="+datosModificados[8]+",Cli_Dom_Calle="+datosModificados[9]+@",
            Cli_Nro_Calle="+datosModificados[10]+",Cli_Piso"+datosModificados[11]+",Cli_Depto="+datosModificados[12]+",Cli_Cod_Postal="+datosModificados[13]+" where Id_Usuario = '" + id + "' ", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Close();
            return 0;
        }

        public bool verificarDocumento(string nrodoc, string tipodoc)
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand("select count(*) from GROUP_APROVED.Clientes where Dni_Cli = '" + nrodoc + "' and Tipo_Dni = '" + tipodoc + "'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            int retorno = lector.GetInt32(0);
            lector.Close();
            return (retorno == 0);
        }
    }
}

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

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class agregarUsuarioCliente : Form
    {
        public CreacionUsuarioCliente datosParaCrear;
        DbQueryHandlerPantallaAgregarUsuarioCliente dbQueryHandler = new DbQueryHandlerPantallaAgregarUsuarioCliente();

        public agregarUsuarioCliente(CreacionUsuarioCliente cliente)
        {
            InitializeComponent();
            datosParaCrear = cliente;
            dateTimePickerFecNac.Value = DateTime.Now;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void agregarUsuarioCliente_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarLlenadoDatos())
            {

                dbQueryHandler.IniciarTransaction();
                cargarDatosEnDataObject();
                int resultado = dbQueryHandler.crearUsuario(datosParaCrear);
                //1 inserto en usuarios 
                //2 no inserto en usuarios
                //2xx no inserto en clientes
                //1xx inserto en lcinetes
                if (resultado == 101) { mensajeExito(); this.Close(); };
                if (resultado == 102) { mensajeErrorUsuarios(); };
                if (resultado == 201) { mensajeErrorClientes(); };
                if (resultado == 202) { mensajeErrorAmbos(); };
                datosParaCrear = null;
            };
        }

        private void mensajeErrorAmbos()
        {
            dbQueryHandler.rollbackear();
            MessageBox.Show("Error en SQL- Se hizo un rollback transaction");
        }

        private void mensajeErrorClientes()
        {
            dbQueryHandler.rollbackear();
            MessageBox.Show("Error en SQL- Tabla Clientes - Se hizo un rollback transaction");
        }

        private void mensajeErrorUsuarios()
        {
            dbQueryHandler.rollbackear();
            MessageBox.Show("Error en SQL- Tabla Usuarios - Se hizo un rollback transaction");
        }
        private void mensajeExito()
        {
            dbQueryHandler.endTransaction();
            MessageBox.Show("Éxito");
            datosParaCrear = null;
        }

        private void cargarDatosEnDataObject()
        {
            datosParaCrear.setApellido(textBoxApellido.Text);
            datosParaCrear.setNombre(textBoxNombre.Text);
            datosParaCrear.setTipoDoc(comboBoxTipoDoc.SelectedItem.ToString());
            datosParaCrear.setDoc(Convert.ToInt32(textBoxNroDoc.Text));
            datosParaCrear.setEmail(textBoxEmail.Text);
            datosParaCrear.setCalle(textBoxCalle.Text);
            datosParaCrear.setPiso(Convert.ToInt32(textBoxPiso.Text));
            datosParaCrear.setNCalle(Convert.ToInt32(textBoxNroCalle.Text));
            datosParaCrear.setLoc(textBoxLocalidad.Text);
            datosParaCrear.setTel(Convert.ToInt32(textBoxTelefono.Text));
            datosParaCrear.setDpto(textBoxDpto.Text);
            datosParaCrear.setCP(Convert.ToInt32(textBoxCP.Text));
            datosParaCrear.setFecNac(dateTimePickerFecNac.Value.ToString());
            datosParaCrear.setFecCre(DateTime.Now.ToString());

        }

        private bool verificarLlenadoDatos()
        {
            string mensajeFalla = "";
            bool rta = true;

            if ((textBoxNombre.Text == "") || tieneNumeros(textBoxNombre.Text)) { mensajeFalla = mensajeFalla + "Nombre inválido"; rta = false; };
            if ((textBoxApellido.Text == "") || tieneNumeros(textBoxApellido.Text)) { mensajeFalla = mensajeFalla + "\nApellido inválido"; rta = false; };
            if ((comboBoxTipoDoc.SelectedIndex) == (-1)) { mensajeFalla = mensajeFalla + "\nTipo de documento inválido"; rta = false; };
            if ((textBoxNroDoc.Text == "") || (tieneLetras(textBoxNroDoc.Text))) { mensajeFalla = mensajeFalla + "\nNro de documento inválido"; rta = false; };
            if ((textBoxEmail.Text == "") || !(textBoxEmail.Text.Contains('@'))) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false; };
            if ((textBoxTelefono.Text == "") || (tieneLetras(textBoxTelefono.Text))) { mensajeFalla = mensajeFalla + "\nTeléfono inválido"; rta = false; };
            if (textBoxCalle.Text == "") { mensajeFalla = mensajeFalla + "\nNombre de calle inválido"; rta = false; };
            if ((textBoxNroCalle.Text == "") || (tieneLetras(textBoxNroCalle.Text))) { mensajeFalla = mensajeFalla + "\nNro de calle inválido"; rta = false; };
            if ((textBoxPiso.Text == "") || (tieneLetras(textBoxPiso.Text))) { mensajeFalla = mensajeFalla + "\nNro de piso inválido"; rta = false; };
            if (textBoxDpto.Text == "") { mensajeFalla = mensajeFalla + "\nDepartamento inválido"; rta = false; };
            if (textBoxLocalidad.Text == "") { mensajeFalla = mensajeFalla + "\nLocalidad inválida"; rta = false; };
            if ((textBoxCP.Text == "") || (tieneLetras(textBoxCP.Text))) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if (!(DateTime.Compare(dateTimePickerFecNac.Value, DateTime.Now) <= 0)) { mensajeFalla = mensajeFalla + "\nFecha de nacimiento inválida"; rta = false; };
            if ((!(textBoxNroDoc.Text == "") && !(tieneLetras(textBoxNroDoc.Text)) && !((comboBoxTipoDoc.SelectedIndex) == (-1))))
            {
                if (!verificarUnicos(textBoxNroDoc.Text, comboBoxTipoDoc.Text)) { rta = false; mensajeFalla = mensajeFalla + "\nDocumento y tipo de documento registrados"; };
            };

            if (rta == false) { MessageBox.Show("Errores detectados:\n\n"+mensajeFalla); }
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
            foreach (char letra in texto){
                if (letras.Contains(letra)) { rta = true; return rta; };
            }
            return rta;
        }

        private bool verificarUnicos(string nrodoc, string tipodoc)
        {
            return dbQueryHandler.verificarDocumento(nrodoc, tipodoc);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            datosParaCrear = null;

            this.Close();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxApellido.Text = "";
            textBoxCalle.Text = "";
            textBoxCP.Text = "";
            textBoxDpto.Text = "";
            textBoxEmail.Text = "";
            textBoxLocalidad.Text = "";
            textBoxNombre.Text = "";
            textBoxNroCalle.Text = "";
            textBoxNroDoc.Text = "";
            textBoxPiso.Text = "";
            textBoxTelefono.Text = "";
            comboBoxTipoDoc.SelectedIndex=-1;
            dateTimePickerFecNac.Value = DateTime.Now;
        }

    }
    class DbQueryHandlerPantallaAgregarUsuarioCliente
    {
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

        public int crearUsuario(CreacionUsuarioCliente datos)
        {
            int mensajeRespuesta;
            SqlCommand command = new SqlCommand("IngresarNuevoUsuarioCliente", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@password", datos.password);
                command.Parameters.AddWithValue("@dni_Cli", datos.documento);
                command.Parameters.AddWithValue("@Tipo_Dni", datos.tipoDoc);
                command.Parameters.AddWithValue("@Cli_Nombre", datos.nombre);
                command.Parameters.AddWithValue("@Cli_Apellido", datos.apellido);
                command.Parameters.AddWithValue("@Cli_Mail", datos.email);
                command.Parameters.AddWithValue("@Cli_Dom_Calle", datos.calle);
                command.Parameters.AddWithValue("@Cli_Nro_Calle", datos.nrocalle);
                command.Parameters.AddWithValue("@Cli_Piso", datos.piso);
                command.Parameters.AddWithValue("@Cli_Depto", datos.dpto);
                command.Parameters.AddWithValue("@Cli_Cod_Postal", datos.codigopostal);
                command.Parameters.AddWithValue("@Cli_Localidad", datos.localidad);
                command.Parameters.AddWithValue("@Id_Usuario", datos.idusuario);
                command.Parameters.AddWithValue("@Fecha_Creacion", datos.fechaCreacion);
                command.Parameters.AddWithValue("@Cli_Fecha_Nac", datos.fechaNac);

                SqlParameter retVal = new SqlParameter("@responseMessage", SqlDbType.Int);
            command.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            mensajeRespuesta = Convert.ToInt32(command.Parameters["@responseMessage"].Value);

            return mensajeRespuesta;
        }

        public void IniciarTransaction()
        {
            SqlCommand command = new SqlCommand("Begin Transaction", DbConnection.connection.getdbconnection());
        }

        public void rollbackear()
        {
            SqlCommand command = new SqlCommand("Rollback Transaction", DbConnection.connection.getdbconnection());
        }

        public void endTransaction()
        {
            SqlCommand command = new SqlCommand("End Transaction", DbConnection.connection.getdbconnection());
        }
    }
}

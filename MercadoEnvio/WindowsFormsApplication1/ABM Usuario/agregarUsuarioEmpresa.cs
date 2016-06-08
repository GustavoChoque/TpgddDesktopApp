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
    public partial class agregarUsuarioEmpresa : Form
    {
        public CreacionUsuarioEmpresa datosParaCrear;

        DbQueryHandlerPantallaAgregarUsuarioEmpresa dbQueryHandler = new DbQueryHandlerPantallaAgregarUsuarioEmpresa();

        public agregarUsuarioEmpresa(CreacionUsuarioEmpresa cliente)
        {
            InitializeComponent();
            datosParaCrear = cliente;
        }

        private void agregarUsuarioEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCrearUsuarioEmpresa_Click(object sender, EventArgs e)
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
            }
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
            datosParaCrear.setCalle(textBoxCalle.Text);
            datosParaCrear.setCiudad(textBoxCiudad.Text);
            datosParaCrear.setCP(Convert.ToInt32(textBoxCP.Text));
            datosParaCrear.setCuit(textBoxCP.Text);
            datosParaCrear.setDpto(textBoxDpto.Text);
            datosParaCrear.setEmail(textBoxEmail.Text);
            datosParaCrear.setLoc(textBoxLoc.Text);
            datosParaCrear.setNCalle(Convert.ToInt32(textBoxNroCalle.Text));
            datosParaCrear.setNombreCont(textBoxNombreContact.Text);
            datosParaCrear.setPiso(Convert.ToInt32(textBoxPiso.Text));
            datosParaCrear.setRazSoc(textBoxRazonSoc.Text);
            datosParaCrear.setRubroTrabajo(textBoxRolTrabajoEmpresa.Text);
            datosParaCrear.setTel(Convert.ToInt32(textBoxTel.Text));
            datosParaCrear.setFecCreacion(DateTime.Now.ToString());
        }

        private bool verificarLlenadoDatos()
        {
            string mensajeFalla="";
            bool rta = true;

            if (textBoxCalle.Text == "") { mensajeFalla=mensajeFalla+"Nombre de calle inválido"; rta = false; };
            if (textBoxCiudad.Text == "") { mensajeFalla = mensajeFalla + "\nNombre de Ciudad inválido"; rta = false; };
            if ((textBoxCP.Text == "") || tieneLetras(textBoxCP.Text)) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if ((textBoxCuit.Text == "") || tieneLetras(textBoxCuit.Text)) { mensajeFalla = mensajeFalla + "\nNro de CUIT inválido"; rta = false; };
            if (textBoxDpto.Text == "") { mensajeFalla = mensajeFalla + "\nNro de departamento inválido"; rta = false; };
            if ((textBoxEmail.Text == "") || !textBoxEmail.Text.Contains('@')) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false;  };
            if (textBoxLoc.Text == "") { mensajeFalla = mensajeFalla + "\nNombre de localidad inválido"; rta = false; };
            if ((textBoxNombreContact.Text == "") || tieneNumeros(textBoxNombreContact.Text)) { mensajeFalla = mensajeFalla + "\nNombre de contacto inválido"; rta = false; };
            if ((textBoxNroCalle.Text == "") || tieneLetras(textBoxNroCalle.Text)) { mensajeFalla = mensajeFalla + "\nNumero de calle inválido"; rta = false; };
            if ((textBoxPiso.Text == "") || (tieneLetras(textBoxPiso.Text))) { mensajeFalla = mensajeFalla + "\nPiso inválido"; rta = false; };
            if ((textBoxRazonSoc.Text == "") || tieneNumeros(textBoxRazonSoc.Text)) { mensajeFalla = mensajeFalla + "\nRazón social inválido"; rta = false; };
            if (textBoxRolTrabajoEmpresa.Text == "") { mensajeFalla = mensajeFalla + "\nRubro en el que se desenvuelve la empresa inválido"; rta = false; };
            if ((textBoxTel.Text == "") || tieneLetras(textBoxTel.Text)) { mensajeFalla = mensajeFalla + "\nNumero de teléfono inválido"; rta = false; };
            if (!(textBoxRazonSoc.Text == "")&&!(textBoxCuit.Text == ""))
            {
                if (!verificarUnicos(textBoxRazonSoc.Text, textBoxCuit.Text)) { rta = false; mensajeFalla = mensajeFalla + "\nRazón Social y Cuit registrados"; };
            }
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

        private bool verificarUnicos(string razonSoc, string nrocuit)
        {
            return dbQueryHandler.verificarRazonSocYCUIT(razonSoc, nrocuit);
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            datosParaCrear = null;

            this.Close();
        }
        
        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxCalle.Text= "";
            textBoxCiudad.Text = "";
            textBoxCP.Text = "";
            textBoxCuit.Text = "";
            textBoxDpto.Text = "";
            textBoxEmail.Text = "";
            textBoxLoc.Text = "";
            textBoxNombreContact.Text = "";
            textBoxNroCalle.Text = "";
            textBoxPiso.Text = "";
            textBoxRazonSoc.Text = "";
            textBoxRolTrabajoEmpresa.Text = "";
            textBoxTel.Text = "";

        }

    }


public class DbQueryHandlerPantallaAgregarUsuarioEmpresa
    {
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

        public int crearUsuario(CreacionUsuarioEmpresa datos)
        {
            int mensajeRespuesta;
            SqlCommand command = new SqlCommand("IngresarNuevoUsuarioEmpresa", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idusuario", datos.idusuario);
            command.Parameters.AddWithValue("@password", datos.password);
            command.Parameters.AddWithValue("@razonSoc", datos.razonSocial);
            command.Parameters.AddWithValue("@cuit", datos.cuit);
            command.Parameters.AddWithValue("@email", datos.email);
            command.Parameters.AddWithValue("@calle", datos.calle);
            command.Parameters.AddWithValue("@nrocalle", datos.nrocalle);
            command.Parameters.AddWithValue("@codpostal", datos.codigopostal);
            command.Parameters.AddWithValue("@ciudad", datos.ciudad);
            command.Parameters.AddWithValue("@dpto", datos.dpto);
            command.Parameters.AddWithValue("@loc", datos.localidad);
            command.Parameters.AddWithValue("@nomContacto", datos.nombreContacto);
            command.Parameters.AddWithValue("@piso", datos.piso);
            command.Parameters.AddWithValue("@rubroTrabajo", datos.rubroTrabajoEmpresa);
            command.Parameters.AddWithValue("@Fecha_Creacion", datos.fechaCreacion);

            SqlParameter retVal = new SqlParameter("@responseMessage", SqlDbType.Int);
            command.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            mensajeRespuesta = Convert.ToInt32(command.Parameters["@responseMessage"].Value);

            return mensajeRespuesta;
        }
    
        public bool verificarRazonSocYCUIT(string razonSoc, string nrocuit)
        {
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand("select count(*) from GROUP_APROVED.Empresas where Empresa_Razon_Social = '" + razonSoc + "' and Empresa_Cuit = '" + nrocuit + "'", DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            int retorno = lector.GetInt32(0);
            lector.Close();
            return (retorno == 0);
        }
    }

}

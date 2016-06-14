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
                string mjeError = "";
                dbQueryHandler.IniciarTransaction();
                cargarDatosEnDataObject();
                string resultado = dbQueryHandler.crearUsuario(datosParaCrear);
                //A inserto en usuarios 
                //B no inserto en usuarios
                //F no inserto en clientes
                //E inserto en lcinetes
                //C inserto rol
                //D no inserto rol
                if (resultado.Contains("ACE")) { mensajeExito(); this.Close(); };
                if (resultado.Contains('B')) { mjeError = mjeError + "Error en tabla usuarios\n"; };
                if (resultado.Contains('F')) { mjeError = mjeError + "Error en tabla clientes\n"; };
                if (resultado.Contains('D')) { mjeError = mjeError + "Error en tabla roles\n"; };
                if (mjeError != "") { dbQueryHandler.rollbackear(); MessageBox.Show(mjeError + ("Se hizo un rollback transaction automático")); };
                datosParaCrear = null;
            }
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
            datosParaCrear.setCP(Convert.ToInt32(textBoxCP.Text));
            datosParaCrear.setCuit(textBoxCP.Text);
            datosParaCrear.setDpto(textBoxDpto.Text);
            datosParaCrear.setEmail(textBoxEmail.Text);
            datosParaCrear.setNCalle(Convert.ToInt32(textBoxNroCalle.Text));
            datosParaCrear.setNombreCont(textBoxNombreContact.Text);
            datosParaCrear.setPiso(Convert.ToInt32(textBoxPiso.Text));
            datosParaCrear.setRazSoc(textBoxRazonSoc.Text);
            datosParaCrear.setRubroTrabajo(textBoxRolTrabajoEmpresa.Text);
            datosParaCrear.setTel(Convert.ToInt32(textBoxTel.Text));
            datosParaCrear.setFecCreacion(DateTime.Now);
        }

        private bool verificarLlenadoDatos()
        {
            string mensajeFalla="";
            bool rta = true;

            if (textBoxCalle.Text == "") { mensajeFalla=mensajeFalla+"Nombre de calle inválido"; rta = false; };
            if ((textBoxCP.Text == "") || tieneLetras(textBoxCP.Text)) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if ((textBoxCuit.Text == "") || tieneLetras(textBoxCuit.Text)) { mensajeFalla = mensajeFalla + "\nNro de CUIT inválido"; rta = false; };
            if (textBoxDpto.Text == "") { mensajeFalla = mensajeFalla + "\nNro de departamento inválido"; rta = false; };
            if ((textBoxEmail.Text == "") || !textBoxEmail.Text.Contains('@')) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false;  };
            if ((textBoxNombreContact.Text == "") || tieneNumeros(textBoxNombreContact.Text)) { mensajeFalla = mensajeFalla + "\nNombre de contacto inválido"; rta = false; };
            if ((textBoxNroCalle.Text == "") || tieneLetras(textBoxNroCalle.Text)) { mensajeFalla = mensajeFalla + "\nNumero de calle inválido"; rta = false; };
            if ((textBoxPiso.Text == "") || (tieneLetras(textBoxPiso.Text))) { mensajeFalla = mensajeFalla + "\nPiso inválido"; rta = false; };
            if (textBoxRazonSoc.Text == "") { mensajeFalla = mensajeFalla + "\nRazón social inválido"; rta = false; };
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
            textBoxCP.Text = "";
            textBoxCuit.Text = "";
            textBoxDpto.Text = "";
            textBoxEmail.Text = "";
            textBoxNombreContact.Text = "";
            textBoxNroCalle.Text = "";
            textBoxPiso.Text = "";
            textBoxRazonSoc.Text = "";
            textBoxRolTrabajoEmpresa.Text = "";
            textBoxTel.Text = "";

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

    }


public class DbQueryHandlerPantallaAgregarUsuarioEmpresa
    {
        public void IniciarTransaction()
        {
            SqlCommand command = new SqlCommand("Begin Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();
        }

        public void rollbackear()
        {
            SqlCommand command = new SqlCommand("Rollback Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();
        }

        public void endTransaction()
        {
            SqlCommand command = new SqlCommand("Commit Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();
        }

        public string crearUsuario(CreacionUsuarioEmpresa datos)
        {
            string mensajeRespuesta;
            SqlCommand command = new SqlCommand("GROUP_APROVED.CrearUsuarioEmpresa", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Username", datos.idusuario);
            command.Parameters.AddWithValue("@Password", datos.password);
            command.Parameters.AddWithValue("@Fecha_Creacion", datos.fechaCreacion);
            command.Parameters.AddWithValue("@Empresa_Razon_Social", datos.razonSocial);
            command.Parameters.AddWithValue("@Empresa_Cuit", datos.cuit);
            command.Parameters.AddWithValue("@Empresa_Mail", datos.email);
            command.Parameters.AddWithValue("@Empresa_Dom_Calle", datos.calle);
            command.Parameters.AddWithValue("@Empresa_Nro_Calle", datos.nrocalle);
            command.Parameters.AddWithValue("@Empresa_Cod_Postal", datos.codigopostal);
            command.Parameters.AddWithValue("@Empresa_Telefono", datos.telefono);
            command.Parameters.AddWithValue("@Empresa_Depto", datos.dpto);
            command.Parameters.AddWithValue("@Empresa_Nombre_Contacto", datos.nombreContacto);
            command.Parameters.AddWithValue("@Empresa_Piso", datos.piso);
            command.Parameters.AddWithValue("@Empresa_RubroP", datos.rubroTrabajoEmpresa);
            

            SqlParameter retVal = new SqlParameter("@respuesta", SqlDbType.NVarChar,255);
            command.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            mensajeRespuesta = command.Parameters["@respuesta"].Value.ToString();

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

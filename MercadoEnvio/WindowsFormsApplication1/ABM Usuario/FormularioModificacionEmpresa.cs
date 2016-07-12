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
    public partial class FormularioModificacionEmpresa : Form
    {
        public string ID;
        public string usuario;
        DbQueryHandlerPantallaModifUsuarioEmpresa dbQueryHandler = new DbQueryHandlerPantallaModifUsuarioEmpresa();
        ModificacionUsuarioEmpresa datosCliente = new ModificacionUsuarioEmpresa();
        
        public FormularioModificacionEmpresa(string id,string usuarioRecibido)
        {
            InitializeComponent();
            ID = id;
            usuario = usuarioRecibido;
            datosCliente = dbQueryHandler.buscarDatos(ID);
            llenarTextBoxs(datosCliente);
        }

        private void llenarTextBoxs(ModificacionUsuarioEmpresa datos)
        {
            if (datos.calle != null) { textBoxCalle.Text = datos.calle; };
            if (datos.codigopostal != -1) { textBoxCP.Text = datos.codigopostal.ToString();};
            if (datos.dpto != null) { textBoxDepto.Text = datos.dpto;};
            if (datos.email != null) { textBoxEmail.Text = datos.email;};
            if (datos.nrocalle != -1) { textBoxNCalle.Text = datos.nrocalle.ToString();};
            if (datos.telefono != -1) { textBoxTel.Text = datos.telefono.ToString();};
            if (datos.piso != -200) { textBoxPiso.Text = datos.piso.ToString();};
            if (datos.estado != null) { comboBox2.SelectedItem = datos.estado;};
            textBox1.Text = ID;
            textBox2.Text = usuario;
            if (datos.cuit != null) { textBoxCuit.Text = datos.cuit;};
            if (datos.nombreContacto != null) { textBoxNomCon.Text = datos.nombreContacto;};
            if (datos.razonSocial != null) { textBoxRS.Text = datos.razonSocial;};
            if (datos.rubroTrabajoEmpresa != null) { textBoxRubro.Text = datos.rubroTrabajoEmpresa; };

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarLlenado())
            {
                //dbQueryHandler.IniciarTransaction();
                ModificacionUsuarioEmpresa datosModificados = new ModificacionUsuarioEmpresa();
                cargarDatosEnDataObject(datosModificados);
                string mjeRta = dbQueryHandler.grabarDatos(datosModificados, Convert.ToInt32(ID));
                //if (mjeRta.Contains("AC")) { MessageBox.Show("Exito"); dbQueryHandler.endTransaction(); this.Close(); } else { MessageBox.Show("Error SQL"); dbQueryHandler.rollbackear(); };
                if (mjeRta.Contains("AC")) { MessageBox.Show("Exito"); this.Close(); } else { MessageBox.Show("Error SQL"); };
                if (mjeRta.Contains('B')) { MessageBox.Show("Error tabla clientes"); };
                if (mjeRta.Contains('D')) { MessageBox.Show("Error tabla usuarios"); };

            }
        }

        private void cargarDatosEnDataObject(ModificacionUsuarioEmpresa datosParaCrear)
        {
            datosParaCrear.setCalle(textBoxCalle.Text);
            datosParaCrear.setCP(Convert.ToInt32(textBoxCP.Text));
            datosParaCrear.setCuit(textBoxCP.Text);
            datosParaCrear.setDpto(textBoxDepto.Text);
            datosParaCrear.setEmail(textBoxEmail.Text);
            datosParaCrear.setNCalle(Convert.ToInt32(textBoxNCalle.Text));
            datosParaCrear.setNombreCont(textBoxNomCon.Text);
            datosParaCrear.setPiso(Convert.ToInt32(textBoxPiso.Text));
            datosParaCrear.setRazSoc(textBoxRS.Text);
            datosParaCrear.setRubroTrabajo(textBoxRubro.Text);
            datosParaCrear.setTel(Convert.ToInt32(textBoxTel.Text));
            datosParaCrear.setEstado(comboBox2.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            llenarTextBoxs(datosCliente);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            datosCliente = null;
            this.Close();
        }

        private bool verificarLlenado()
        {
            string mensajeFalla = "";
            bool rta = true;

            if (textBoxCalle.Text == "") { mensajeFalla = mensajeFalla + "Nombre de calle inválido"; rta = false; };
            if ((textBoxCP.Text == "") || tieneLetras(textBoxCP.Text)) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if ((textBoxCuit.Text == "") || tieneLetras(textBoxCuit.Text)) { mensajeFalla = mensajeFalla + "\nNro de CUIT inválido"; rta = false; };
            if (textBoxDepto.Text == "") { mensajeFalla = mensajeFalla + "\nNro de departamento inválido"; rta = false; };
            if ((textBoxEmail.Text == "") || !textBoxEmail.Text.Contains('@')) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false; };
            if ((textBoxNomCon.Text == "") || tieneNumeros(textBoxNomCon.Text)) { mensajeFalla = mensajeFalla + "\nNombre de contacto inválido"; rta = false; };
            if ((textBoxNCalle.Text == "") || tieneLetras(textBoxNCalle.Text)) { mensajeFalla = mensajeFalla + "\nNumero de calle inválido"; rta = false; };
            if ((textBoxPiso.Text == "") || (tieneLetras(textBoxPiso.Text))) { mensajeFalla = mensajeFalla + "\nPiso inválido"; rta = false; };
            if (textBoxRS.Text == "") { mensajeFalla = mensajeFalla + "\nRazón social inválido"; rta = false; };
            if (textBoxRubro.Text == "") { mensajeFalla = mensajeFalla + "\nRubro en el que se desenvuelve la empresa inválido"; rta = false; };
            if ((textBoxTel.Text == "") || tieneLetras(textBoxTel.Text)) { mensajeFalla = mensajeFalla + "\nNumero de teléfono inválido"; rta = false; };
            if ((!(textBoxRS.Text.Contains(datosCliente.razonSocial))
                &&!(textBoxCuit.Text.Contains(datosCliente.cuit)))
                &&!verificarUnicos(textBoxRS.Text, textBoxCuit.Text)) 
            { rta = false; mensajeFalla = mensajeFalla + "\nRazón Social y Cuit registrados"; };
            if (rta == false) { MessageBox.Show("Errores detectados:\n\n" + mensajeFalla); };
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
    }

    public class ModificacionUsuarioEmpresa
    {
        public string razonSocial;
        public string cuit;
        public string email;
        public int telefono;
        public string calle;
        public int nrocalle;
        public int piso;
        public string dpto;
        public string localidad;
        public string ciudad;
        public int codigopostal;
        public string nombreContacto;
        public string rubroTrabajoEmpresa;
        public string estado;

        //setters
        public void setRazSoc(string n) { razonSocial = n; }
        public void setCuit(string t) { cuit = t; }
        public void setEmail(string e) { email = e; }
        public void setTel(int t) { telefono = t; }
        public void setCalle(string c) { calle = c; }
        public void setNCalle(int n) { nrocalle = n; }
        public void setPiso(int p) { piso = p; }
        public void setDpto(string d) { dpto = d; }
        public void setLoc(string l) { localidad = l; }
        public void setCiudad(string c) { ciudad = c; }
        public void setCP(int cp) { codigopostal = cp; }
        public void setNombreCont(string nc) { nombreContacto = nc; }
        public void setRubroTrabajo(string r) { rubroTrabajoEmpresa = r; }
        public void setEstado(string es) { estado=es;}

    }

    class DbQueryHandlerPantallaModifUsuarioEmpresa
    {
        SqlCommand comando;

        public ModificacionUsuarioEmpresa buscarDatos(string id)
        {
            comando = new SqlCommand(@"select 
            Empresa_Razon_Social,	Empresa_Cuit,	Empresa_Mail,	
            Empresa_Dom_Calle,	Empresa_Piso,	Empresa_Depto,	
            Empresa_Fecha_Creacion,	Empresa_Nro_Calle, Empresa_Cod_Postal,	
            Id_Usuario,	Empresa_Telefono, Empresa_Nombre_Contacto,
            Empresa_RubroP, Estado
            from GROUP_APROVED.Empresas c join GROUP_APROVED.Usuarios u on (u.Id_Usr = c.Id_Usuario) where Id_Usuario =" + id, DbConnection.connection.getdbconnection());
            SqlDataReader lector = comando.ExecuteReader();
            lector.Read();

            ModificacionUsuarioEmpresa datosCliente = new ModificacionUsuarioEmpresa();

            if (!lector.IsDBNull(3)) { datosCliente.setCalle(lector.GetValue(3).ToString()); };
            if (!lector.IsDBNull(8)) { datosCliente.setCP(Convert.ToInt32(lector.GetValue(8)));}else datosCliente.setCP(-1);
            if (!lector.IsDBNull(1)) { datosCliente.setCuit(lector.GetValue(1).ToString());};
            if (!lector.IsDBNull(5)) { datosCliente.setDpto(lector.GetValue(5).ToString());};
            if (!lector.IsDBNull(2)) { datosCliente.setEmail(lector.GetValue(2).ToString());};
            if (!lector.IsDBNull(4)) { datosCliente.setPiso(Convert.ToInt32(lector.GetValue(4))); } else datosCliente.setPiso(-200);
            if (!lector.IsDBNull(7)) { datosCliente.setNCalle(Convert.ToInt32(lector.GetValue(7))); } else datosCliente.setNCalle(-1);
            if (!lector.IsDBNull(10)) { datosCliente.setTel(Convert.ToInt32(lector.GetValue(10))); } else datosCliente.setTel(-1);
            if (!lector.IsDBNull(13)) { datosCliente.setEstado(lector.GetValue(13).ToString());};
            if (!lector.IsDBNull(11)) { datosCliente.setNombreCont(lector.GetValue(11).ToString());};
            if (!lector.IsDBNull(0)) { datosCliente.setRazSoc(lector.GetValue(0).ToString());};
            if (!lector.IsDBNull(12)) { datosCliente.setRubroTrabajo(lector.GetValue(12).ToString()); };
            lector.Close();

            return datosCliente;
        }

        public string grabarDatos(ModificacionUsuarioEmpresa datosModificados, int id)
        {
            string mensajeRespuesta;
            SqlCommand command = new SqlCommand("GROUP_APROVED.updateEmpresa", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Empresa_Razon_Social", datosModificados.razonSocial);
            command.Parameters.AddWithValue("@Empresa_Cuit", datosModificados.cuit);
            command.Parameters.AddWithValue("@Empresa_Mail ", datosModificados.email);
            command.Parameters.AddWithValue("@Empresa_Dom_Calle", datosModificados.calle);
            command.Parameters.AddWithValue("@Empresa_Nro_Calle ", datosModificados.nrocalle);
            command.Parameters.AddWithValue("@Empresa_Piso ", datosModificados.piso);
            command.Parameters.AddWithValue("@Empresa_Depto", datosModificados.dpto);
            command.Parameters.AddWithValue("@Empresa_Cod_Postal", datosModificados.codigopostal);
            command.Parameters.AddWithValue("@Id_Usuario", id);
            command.Parameters.AddWithValue("@Empresa_Telefono ", datosModificados.telefono);
            command.Parameters.AddWithValue("@Empresa_Nombre_Contacto ", datosModificados.nombreContacto);
            command.Parameters.AddWithValue("@Empresa_RubroP ", datosModificados.rubroTrabajoEmpresa);
            command.Parameters.AddWithValue("@Estado ", datosModificados.estado);
            
            SqlParameter retVal = new SqlParameter("@respuesta", SqlDbType.NVarChar, 255);
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
    }
}

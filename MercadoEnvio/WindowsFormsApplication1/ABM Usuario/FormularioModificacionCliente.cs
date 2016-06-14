﻿using System;
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
        ModificacionUsuarioCliente datosCliente = new ModificacionUsuarioCliente();

        public FormularioModificacionCliente(string id, string usuarioRecibido)
        {
            InitializeComponent();
            ID = id;
            usuario = usuarioRecibido;
            datosCliente = dbQueryHandler.buscarDatos(ID);
            llenarTextBoxs(datosCliente);
        }

        private void llenarTextBoxs(ModificacionUsuarioCliente datos)
        {
            textBoxAp.Text = datos.apellido;
            textBoxCalle.Text = datos.calle;
            textBoxCP.Text = datos.codigopostal.ToString();
            textBoxDepto.Text = datos.dpto;
            textBoxDoc.Text = datos.documento.ToString();
            textBoxEmail.Text = datos.email;
            textBoxNCalle.Text = datos.nrocalle.ToString();
            textBoxNom.Text = datos.nombre;
            textBoxPiso.Text = datos.piso.ToString();
            textBoxTel.Text = datos.telefono.ToString();
            comboBox1.SelectedItem = datos.tipoDoc;
            comboBox2.SelectedItem = datos.estado;
            dateTimePicker1.Value = datos.fechaNac;
            textBox1.Text = ID;
            textBox2.Text = usuario;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarLlenado())
            {
                dbQueryHandler.IniciarTransaction();
                ModificacionUsuarioCliente datosModificados = new ModificacionUsuarioCliente();
                cargarDatosEnDataObject(datosModificados);
                string mjeRta = dbQueryHandler.grabarDatos(datosModificados,Convert.ToInt32(ID));
                if (mjeRta.Contains("AC")) { MessageBox.Show("Exito"); dbQueryHandler.endTransaction(); this.Close(); } else { MessageBox.Show("Error SQL"); dbQueryHandler.rollbackear(); };
                if (mjeRta.Contains('B')){MessageBox.Show("Error tabla clientes");};
                if (mjeRta.Contains('D')){MessageBox.Show("Error tabla usuarios");};

            }
        }

        private bool verificarLlenado()
        {
            string mensajeFalla = "";
            bool rta = true;
            if ((textBoxDoc.Text == "") || (tieneLetras(textBoxDoc.Text))) { mensajeFalla = mensajeFalla + "\nNro de documento inválido"; rta = false; };
            if ((comboBox1.SelectedIndex) == (-1)) { mensajeFalla = mensajeFalla + "\nTipo de documento inválido"; rta = false; };
            if ((textBoxNom.Text == "") || tieneNumeros(textBoxNom.Text)) { mensajeFalla = mensajeFalla + "Nombre inválido"; rta = false; };
            if ((textBoxAp.Text == "") || tieneNumeros(textBoxAp.Text)) { mensajeFalla = mensajeFalla + "\nApellido inválido"; rta = false; };
            if ((textBoxTel.Text == "") || (tieneLetras(textBoxTel.Text))) { mensajeFalla = mensajeFalla + "\nTeléfono inválido"; rta = false; };
            if ((textBoxEmail.Text == "") || !(textBoxEmail.Text.Contains('@'))) { mensajeFalla = mensajeFalla + "\nEmail inválido"; rta = false; };
            if (textBoxCalle.Text == "") { mensajeFalla = mensajeFalla + "\nNombre de calle inválido"; rta = false; };
            if ((textBoxNCalle.Text == "") || (tieneLetras(textBoxNCalle.Text))) { mensajeFalla = mensajeFalla + "\nNro de calle inválido"; rta = false; };
            if ((textBoxPiso.Text == "") || (tieneLetras(textBoxPiso.Text))) { mensajeFalla = mensajeFalla + "\nNro de piso inválido"; rta = false; };
            if (textBoxDepto.Text == "") { mensajeFalla = mensajeFalla + "\nDepartamento inválido"; rta = false; };
            if ((textBoxCP.Text == "") || (tieneLetras(textBoxCP.Text))) { mensajeFalla = mensajeFalla + "\nCódigo postal inválido"; rta = false; };
            if ((comboBox2.SelectedIndex) == (-1)) { mensajeFalla = mensajeFalla + "\nEstado inválido"; rta = false; };
            if (!(DateTime.Compare(dateTimePicker1.Value, DateTime.Now) <= 0)) { mensajeFalla = mensajeFalla + "\nFecha de nacimiento inválida"; rta = false; };
            if ((!(textBoxDoc.Text == "") && !(tieneLetras(textBoxDoc.Text)) && !((comboBox1.SelectedIndex) == (-1))))
            {
                if ((!(textBoxDoc.Text.Contains(datosCliente.documento.ToString()))
                    &&(comboBox1.SelectedItem.ToString().Contains(datosCliente.tipoDoc)))
                    &&(!verificarUnicos(textBoxDoc.Text, comboBox1.Text))) { rta = false; mensajeFalla = mensajeFalla + "\nDocumento y tipo de documento registrados"; };
                //se fija si modifico el dni y tipo dni y busca si existen los nuevos datos ingresdaos
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

        private void cargarDatosEnDataObject(ModificacionUsuarioCliente datosCliente)
        {

            datosCliente.setApellido(textBoxAp.Text);
            datosCliente.setNombre(textBoxNom.Text);
            datosCliente.setTipoDoc(comboBox1.SelectedItem.ToString());
            datosCliente.setDoc(Convert.ToInt32(textBoxDoc.Text));
            datosCliente.setEmail(textBoxEmail.Text);
            datosCliente.setCalle(textBoxCalle.Text);
            datosCliente.setPiso(Convert.ToInt32(textBoxPiso.Text));
            datosCliente.setNCalle(Convert.ToInt32(textBoxNCalle.Text));
            datosCliente.setTel(Convert.ToInt32(textBoxTel.Text));
            datosCliente.setDpto(textBoxDepto.Text);
            datosCliente.setCP(Convert.ToInt32(textBoxCP.Text));
            datosCliente.setFecNac(dateTimePicker1.Value);
            datosCliente.setEstado(comboBox2.SelectedItem.ToString());

        }
    }
    class DbQueryHandlerPantallaModifUsuarioCliente 
    {
        SqlCommand comando;
        
        public ModificacionUsuarioCliente buscarDatos(string id)
        {
            comando = new SqlCommand(@"select Dni_Cli, Tipo_Dni, Cli_Nombre,Cli_Apellido,Cli_Fecha_Nac,
            CLI_Telefono,Cli_Mail,Cli_Dom_Calle,Cli_Nro_Calle,Cli_Piso,Cli_Depto,Cli_Cod_Postal,Id_Usuario,Estado
            from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on (u.Id_Usr = c.Id_Usuario) where Id_Usuario =" + id , DbConnection.connection.getdbconnection());
            SqlDataReader lector = comando.ExecuteReader();
            lector.Read();

            ModificacionUsuarioCliente datosCliente= new ModificacionUsuarioCliente();

            datosCliente.setApellido(lector.GetValue(3).ToString());
            datosCliente.setNombre(lector.GetValue(2).ToString());
            datosCliente.setTipoDoc(lector.GetValue(1).ToString());
            datosCliente.setDoc(Convert.ToInt32(lector.GetValue(0)));
            datosCliente.setEmail(lector.GetValue(6).ToString());
            datosCliente.setCalle(lector.GetValue(7).ToString());
            datosCliente.setPiso(Convert.ToInt32(lector.GetValue(9)));
            datosCliente.setNCalle(Convert.ToInt32(lector.GetValue(8)));
            datosCliente.setTel(Convert.ToInt32(lector.GetValue(5)));
            datosCliente.setDpto(lector.GetValue(10).ToString());
            datosCliente.setCP(Convert.ToInt32(lector.GetValue(11)));
            datosCliente.setFecNac(lector.GetDateTime(4));
            datosCliente.setEstado(lector.GetValue(13).ToString());
            lector.Close();

            return datosCliente;
        }

        public string grabarDatos(ModificacionUsuarioCliente datosModificados, int id)
        {
            string mensajeRespuesta;
            SqlCommand command = new SqlCommand("GROUP_APROVED.updateClientes", DbConnection.connection.getdbconnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Dni_Cli", datosModificados.documento);
            command.Parameters.AddWithValue("@Tipo_Dni", datosModificados.tipoDoc);
            command.Parameters.AddWithValue("@Cli_Nombre", datosModificados.nombre);
            command.Parameters.AddWithValue("@Cli_Apellido", datosModificados.apellido);
            command.Parameters.AddWithValue("@Cli_Fecha_Nac", datosModificados.fechaNac);
            command.Parameters.AddWithValue("@CLI_Telefono", datosModificados.telefono);
            command.Parameters.AddWithValue("@Cli_Mail", datosModificados.email);
            command.Parameters.AddWithValue("@Cli_Dom_Calle", datosModificados.calle);
            command.Parameters.AddWithValue("@Cli_Nro_Calle", datosModificados.nrocalle);
            command.Parameters.AddWithValue("@Cli_Piso", datosModificados.piso);
            command.Parameters.AddWithValue("@Cli_Depto", datosModificados.dpto);
            command.Parameters.AddWithValue("@Cli_Cod_Postal", datosModificados.codigopostal);
            command.Parameters.AddWithValue("@Id_Usr", id);
            command.Parameters.AddWithValue("@Estado",datosModificados.estado);

            SqlParameter retVal = new SqlParameter("@respuesta", SqlDbType.NVarChar, 255);
            command.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            mensajeRespuesta = command.Parameters["@respuesta"].Value.ToString();

            return mensajeRespuesta;
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


    public class ModificacionUsuarioCliente
    {
        public string idusuario;
        public string password;
        public string nombre;
        public string apellido;
        public string tipoDoc;
        public int documento;
        public string email;
        public int telefono;
        public string calle;
        public int nrocalle;
        public int piso;
        public string dpto;
        public string localidad;
        public int codigopostal;
        public DateTime fechaCreacion;
        public DateTime fechaNac;
        public string estado;
        //setters
        public void setNombre(string n) { nombre = n; }
        public void setApellido(string a) { apellido = a; }
        public void setTipoDoc(string t) { tipoDoc = t; }
        public void setDoc(int d) { documento = d; }
        public void setEmail(string e) { email = e; }
        public void setTel(int t) { telefono = t; }
        public void setCalle(string c) { calle = c; }
        public void setNCalle(int n) { nrocalle = n; }
        public void setPiso(int p) { piso = p; }
        public void setDpto(string d) { dpto = d; }
        public void setLoc(string l) { localidad = l; }
        public void setCP(int cp) { codigopostal = cp; }
        public void setFecCre(DateTime date) { fechaCreacion = date; }
        public void setFecNac(DateTime date) { fechaNac = date; }
        public void setEstado(string es) { estado = es; }
    }
}
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

namespace WindowsFormsApplication1.Pantalla_Principal
{
    public partial class Principal : Form
    {
        private String usuarioEnUso;
        private DbClass conexion;
                
        public Principal()
        {
            InitializeComponent();
        }

            public void setearUsuarioEnUso(String usuario)
        {
            this.usuarioEnUso = usuario;
        }

        public void setearConexion(DbClass conexion)
        {
            this.conexion = conexion;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Usuario logueado correctamente-- Pantalla principal";
            button1.Text = "Salir";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void buttonABMUsuario_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form1 pantallaABMUsuario = new ABM_Usuario.Form1();
            pantallaABMUsuario.Show();
        }

        private void buttonABMRol_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form1 pantallaABMRol = new ABM_Rol.Form1();
            pantallaABMRol.Show();
        }

        private void buttonABMRubro_Click(object sender, EventArgs e)
        {
            ABM_Rubro.Form1 pantallaABMRubro = new ABM_Rubro.Form1();
            pantallaABMRubro.Show();
        }

        private void buttonGenerarPublicacion_Click(object sender, EventArgs e)
        {
            Generar_Publicación.Form1 pantallaGenerarPublicacion = new Generar_Publicación.Form1();
            pantallaGenerarPublicacion.Show();
        }

        private void buttonVisibilidadPublicacion_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form1 pantallaABMVisibilidad = new ABM_Visibilidad.Form1();
            pantallaABMVisibilidad.Show();
        }

        private void buttonComprarOfertar_Click(object sender, EventArgs e)
        {
            ComprarOfertar.Form1 pantallaComprarOfertar = new ComprarOfertar.Form1();
            pantallaComprarOfertar.Show();
        }

        private void buttonHistorialCliente_Click(object sender, EventArgs e)
        {
            Historial_Cliente.Form1 pantallaHistorialCliente = new Historial_Cliente.Form1();
            pantallaHistorialCliente.Show();
        }

        private void buttonCalificarVendedor_Click(object sender, EventArgs e)
        {
            Calificar.Form1 pantallaCalificar = new Calificar.Form1();
            pantallaCalificar.Show();
        }

        private void buttonConsultaFacturas_Click(object sender, EventArgs e)
        {
            Facturas.Form1 pantallaConsultaFacturas = new Facturas.Form1();
            pantallaConsultaFacturas.Show();
        }

        private void buttonListadoEstadistico_Click(object sender, EventArgs e)
        {
            Listado_Estadistico.Form1 pantallaListadoEstadistico = new Listado_Estadistico.Form1();
            pantallaListadoEstadistico.Show();
        }
        public void verificarAccesos()
        {
            crearVista();

            if (verificarAccesoFuncion('r') == false) { buttonABMRol.Enabled = false; };
            if (verificarAccesoFuncion('u') == false) { buttonABMUsuario.Enabled = false; };
            if (verificarAccesoFuncion('b') == false) { buttonABMRubro.Enabled = false; };
            if (verificarAccesoFuncion('p') == false) { buttonGenerarPublicacion.Enabled = false; };
            if (verificarAccesoFuncion('h') == false) { buttonHistorialCliente.Enabled = false; };
            if (verificarAccesoFuncion('l') == false) { buttonListadoEstadistico.Enabled = false; };
            if (verificarAccesoFuncion('v') == false) { buttonVisibilidadPublicacion.Enabled = false; };
            if (verificarAccesoFuncion('c') == false) { buttonCalificarVendedor.Enabled = false; };
            if (verificarAccesoFuncion('o') == false) { buttonComprarOfertar.Enabled = false; };
            if (verificarAccesoFuncion('f') == false) { buttonConsultaFacturas.Enabled = false; };

            dropearVista();
            /*funcionalidades:  r=abm rol
                                u=abm usuario
                                b=abm rubro
                                p=generar publicacion
                                h=historial cliente
                                l=listado estadistico
                                v=abm visibilidad
                                c=calificar
                                o=comprar/ofertar
                                f=consultar facturas
             */

        }
        private bool verificarAccesoFuncion(char idfuncion)
        {
            bool respuesta = false;
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand("select count(*) from tablausuariofunciones where usuario = '" + this.usuarioEnUso + "' and funciones = '" + idfuncion + "'", this.conexion.getdbconection());
            lector = comando.ExecuteReader();
            lector.Read();
            int retorno = lector.GetInt32(0);
            lector.Close();
            if (retorno >= 1) { respuesta = true; }
            return respuesta;
        }

        private void crearVista()
        {
            SqlCommand comando = new SqlCommand("create VIEW tablausuariofunciones (usuario, funciones) as select ru.id_usuario, f.id_func from rolesxusuario ru join roles r on (id_rol = id_roles) join funcionesxrol fr on (fr.id_rol=r.id_rol) join funciones f on (f.id_func =fr.id_func) ", this.conexion.getdbconection());
            comando.ExecuteNonQuery();
            //Crea una vista con los usuarios y las funciones a las que pueden acceder
        }
        private void dropearVista()
        {
            SqlCommand comando = new SqlCommand("drop VIEW tablausuariofunciones", this.conexion.getdbconection());
            comando.ExecuteNonQuery();
            //Dropea la vista creada en el metodo anterior
        }



    }
}

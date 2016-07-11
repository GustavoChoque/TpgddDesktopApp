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

        DbQueryHandler dbQueryHandler = new DbQueryHandler();
       
        public Principal()
        {
            InitializeComponent();
            this.verificarAccesos();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Usuario logueado correctamente-- Pantalla principal";
            button1.Text = "Salir";
            this.verificarAccesos();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void buttonABMUsuario_Click(object sender, EventArgs e)
        {
            ABM_Usuario.abmUsuarioPrincipal pantallaABMUsuario = new ABM_Usuario.abmUsuarioPrincipal();
            pantallaABMUsuario.Show();
        }

        private void buttonABMRol_Click(object sender, EventArgs e)
        {
            ABM_Rol.ABM_Rol pantallaABMRol = new ABM_Rol.ABM_Rol();
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
            ABM_Visibilidad.cargarPublicaciones pantallaABMVisibilidad = new ABM_Visibilidad.cargarPublicaciones();
            pantallaABMVisibilidad.Show();
        }

        private void buttonComprarOfertar_Click(object sender, EventArgs e)
        {
            ComprarOfertar.Form1 pantallaComprarOfertar = new ComprarOfertar.Form1();
            pantallaComprarOfertar.Show();
        }

        private void buttonHistorialCliente_Click(object sender, EventArgs e)
        {
            Historial_Cliente.Historial pantallaHistorialCliente = new Historial_Cliente.Historial();
            pantallaHistorialCliente.Show();
        }

        private void buttonCalificarVendedor_Click(object sender, EventArgs e)
        {
            Calificar.calificarVendedor pantallaCalificar = new Calificar.calificarVendedor();
            pantallaCalificar.Show();
        }

        private void buttonConsultaFacturas_Click(object sender, EventArgs e)
        {
            Facturas.ListarFacturas pantallaConsultaFacturas = new Facturas.ListarFacturas();
            pantallaConsultaFacturas.Show();
        }

        private void buttonListadoEstadistico_Click(object sender, EventArgs e)
        {
            Listado_Estadistico.listado pantallaListadoEstadistico = new Listado_Estadistico.listado();
            pantallaListadoEstadistico.Show();
        }
        public void verificarAccesos()
        {
            try { dbQueryHandler.crearVista(); } //Crea una vista con los usuarios y las funciones a las que pueden acceder
            catch { };//try por si estaba creada y no se dropeo

            /*if (dbQueryHandler.verificarAccesoFuncion(1) == false) { buttonABMRol.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(2) == false) { buttonABMUsuario.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(3) == false) { buttonABMRubro.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(4) == false) { buttonGenerarPublicacion.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(7) == false) { buttonHistorialCliente.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(8) == false) { buttonListadoEstadistico.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(9) == false) { buttonVisibilidadPublicacion.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(6) == false) { buttonCalificarVendedor.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(10) == false) { buttonComprarOfertar.Hide(); };
            if (dbQueryHandler.verificarAccesoFuncion(5) == false) { buttonConsultaFacturas.Hide(); };
            */
            dbQueryHandler.dropearVista();
            /*funcionalidades:  1 = r=abm rol
                                2 = u=abm usuario
                                3 = b=abm rubro
                                4 = p=generar publicacion
                                7 = h=historial cliente
                                8 = l=listado estadistico
                                9 = v=abm visibilidad
                                6 = c=calificar
                                10 = o=comprar/ofertar
                                5 = f=consultar facturas
             */

        }
        



    }
    public class DbQueryHandler
    {

        public bool verificarAccesoFuncion(int idfuncion)
        {
            bool respuesta = false;
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand("select count(*) from tablausuariofunciones where usuario = '" + CurrentUser.user.getUserId() + "' and funciones = " + idfuncion, DbConnection.connection.getdbconnection());
            lector = comando.ExecuteReader();
            lector.Read();
            int retorno = lector.GetInt32(0);
            lector.Close();
            if (retorno >= 1) { respuesta = true; }
            return respuesta;
        }

        public void crearVista()
        {
            SqlCommand comando = new SqlCommand("create VIEW tablausuariofunciones (usuario, funciones) as select ru.Id_Usr, f.id_func from GROUP_APROVED.RolesxUsuario ru join GROUP_APROVED.Roles r on (Id_Rol = Id_Roles ) join GROUP_APROVED.FuncionesxRol  fr on (fr.Id_Rol=r.Id_Rol) join GROUP_APROVED.Funciones f on (f.Id_Func =fr.Id_Func) ", DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
            //Crea una vista con los usuarios y las funciones a las que pueden acceder
        }
        public void dropearVista()
        {
            SqlCommand comando = new SqlCommand("drop VIEW tablausuariofunciones", DbConnection.connection.getdbconnection());
            comando.ExecuteNonQuery();
            //Dropea la vista creada en el metodo anterior
        }
    }
}

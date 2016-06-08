using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class abmUsuarioPrincipal : Form
    {
        public abmUsuarioPrincipal()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModificacionUsuario pantallaModificacionUsuario = new ModificacionUsuario();
            pantallaModificacionUsuario.Show();
        }

        private void butVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butCrear_Click(object sender, EventArgs e)
        {
            crearUsuario pantallaCrearUsuario = new crearUsuario();
            pantallaCrearUsuario.Show();
        }

        private void butBajaUsuario_Click(object sender, EventArgs e)
        {
            BajaUsuario pantallaBajaUsuario = new BajaUsuario();
            pantallaBajaUsuario.Show();
        }

        private void abmUsuarioPrincipal_Load(object sender, EventArgs e)
        {

        }

        //metodos que usan los forms "agregar.."

    }
}

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
    public partial class ModificacionUsuario : Form
    {
        public ModificacionUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modificacionUsuarioCliente pantallaModificacionUsuarioCliente = new modificacionUsuarioCliente();
            pantallaModificacionUsuarioCliente.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificacionUsuarioEmpresa pantallaModificacionUsuarioEmpresa = new modificacionUsuarioEmpresa();
            pantallaModificacionUsuarioEmpresa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

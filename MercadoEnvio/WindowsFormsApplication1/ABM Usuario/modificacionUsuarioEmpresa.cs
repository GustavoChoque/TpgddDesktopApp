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
    public partial class modificacionUsuarioEmpresa : Form
    {
        SqlDataAdapter dataAdapter;
        DataTable tablaDatos;
        DbQueryHandlerPantallaModificacionUsuarioEmpresa dbQueryHandler = new DbQueryHandlerPantallaModificacionUsuarioEmpresa();

        public modificacionUsuarioEmpresa()
        {
            InitializeComponent();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            int tipoFiltro = chequearCampos();
            if (tipoFiltro > 0)
            {
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                if (tipoFiltro == 1) { dataAdapter.SelectCommand = dbQueryHandler.consultaFiltrosConCuit(textBoxCuit.Text, textBoxEmail.Text, textBoxRazSoc.Text); };
                if (tipoFiltro == 2) { dataAdapter.SelectCommand = dbQueryHandler.consultaFiltrosSinCuit(textBoxEmail.Text, textBoxRazSoc.Text); };
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
                dataGridView1.CellClick += dataGridView1_CellClick;
                DataGridViewButtonColumn Baja = new DataGridViewButtonColumn();
                Baja.Name = "Modificacion";
                Baja.Text = "Modificacion";
                Baja.Name = "Modificacion";
                Baja.UseColumnTextForButtonValue = true;

                int columnIndex = dataGridView1.ColumnCount;
                if (dataGridView1.Columns["Modificacion"] == null) { dataGridView1.Columns.Insert(columnIndex, Baja); }
                else { dataGridView1.Columns["Modificacion"].DisplayIndex = dataGridView1.ColumnCount - 1; };
                dataGridView1.Columns["Modificacion"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == dataGridView1.Columns["Modificacion"].Index) && (e.RowIndex >= 0))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string usuario = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    FormularioModificacionEmpresa pantallaModifCliente = new FormularioModificacionEmpresa(id, usuario);
                    pantallaModifCliente.Show();
                    dataAdapter = null;
                    tablaDatos = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxRazSoc.Text = "";
            textBoxCuit.Text = "";
            textBoxEmail.Text = "";
            dataAdapter = null;
            dataGridView1.DataSource = null;
        }

        private int chequearCampos()
        {
            if ((textBoxCuit.Text != "") && !(tieneLetras(textBoxCuit.Text))) { return 1; };
            if ((textBoxCuit.Text != "") && (tieneLetras(textBoxCuit.Text))) { MessageBox.Show("Ingrese Cuit válido"); return 0; };
            if ((textBoxRazSoc.Text == "") && (textBoxCuit.Text == "") && (textBoxEmail.Text == "")) { MessageBox.Show("Ingrese algún filtro"); return 0; };
            if (((textBoxRazSoc.Text != "") || (textBoxEmail.Text != "")) && (textBoxCuit.Text == "")) { return 2; };
            MessageBox.Show("Error raro");
            return 0;
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


        class DbQueryHandlerPantallaModificacionUsuarioEmpresa
        {
            public SqlCommand consultaFiltrosConCuit(string cuit, string mail, string razSoc)
            {
                SqlCommand comand = new SqlCommand(@"select Id_Usuario, Username, Empresa_Razon_Social, Empresa_Cuit, Empresa_Mail, Estado  from GROUP_APROVED.Empresas c join GROUP_APROVED.Usuarios u
 on c.Id_Usuario = u.Id_Usr where Empresa_Razon_Social like '%" + razSoc + "%' and Empresa_Mail like '%" + mail + "%' and Empresa_Cuit = " + cuit, DbConnection.connection.getdbconnection());
                return comand;
            }
            public SqlCommand consultaFiltrosSinCuit(string mail, string razSoc)
            {
                SqlCommand comand = new SqlCommand(@"select Id_Usuario, Username, Empresa_Razon_Social, Empresa_Cuit, Empresa_Mail, Estado  from GROUP_APROVED.Empresas c join GROUP_APROVED.Usuarios u
 on c.Id_Usuario = u.Id_Usr where Empresa_Razon_Social like '%" + razSoc + "%' and Empresa_Mail like '%" + mail + "%'", DbConnection.connection.getdbconnection());
                return comand;
            }

        }
    }
}
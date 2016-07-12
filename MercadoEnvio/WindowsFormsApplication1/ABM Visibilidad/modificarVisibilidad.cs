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

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class modificarVisibilidad : Form
    {
        ABM_Visibilidad.cargarPublicaciones pant;
        bool puedeUsarGratis = false;
        bool verificado = false;
        bool envio = false;
        int codPub;
        int codVisibilidad;
        int codVisibilidadAcambiar;
        dbQueryHandlerVisibilidades dbQueryHandler = new dbQueryHandlerVisibilidades();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable tablaDatos = new DataTable();

        public modificarVisibilidad(int codPubl, int codVisi, int cantPubl, ABM_Visibilidad.cargarPublicaciones pantallaABMVisibilidad )
        {
            InitializeComponent();
            codPub = codPubl;
            codVisibilidad = codVisi;
            pant = pantallaABMVisibilidad;
            if (cantPubl == 1) { puedeUsarGratis = true; };

            dataAdapter = new SqlDataAdapter();
            tablaDatos = new DataTable();
            dataAdapter.SelectCommand = dbQueryHandler.consultaVisibilidades();
            dataAdapter.Fill(tablaDatos);
            dataGridView1.DataSource = tablaDatos;
            dataGridView1.CellClick += dataGridView1_CellClick;
            DataGridViewButtonColumn seleccionarVisibilidad = new DataGridViewButtonColumn();
            seleccionarVisibilidad.Name = "Seleccionar Visibilidad";
            seleccionarVisibilidad.Text = "Seleccionar Visibilidad";
            seleccionarVisibilidad.Name = "Seleccionar Visibilidad";
            seleccionarVisibilidad.UseColumnTextForButtonValue = true;

            int columnIndex = dataGridView1.ColumnCount;
            if (dataGridView1.Columns["seleccionarVisibilidad"] == null) { dataGridView1.Columns.Insert(columnIndex, seleccionarVisibilidad); }
            else { dataGridView1.Columns["seleccionarVisibilidad"].DisplayIndex = dataGridView1.ColumnCount - 1; };
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //bool envio = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                if ((e.ColumnIndex == dataGridView1.Columns["Seleccionar Visibilidad"].Index) && (e.RowIndex >= 0))
                {
                    this.codVisibilidadAcambiar= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    textBox1.Text = codVisibilidad.ToString();
                    int cod = codVisibilidadAcambiar;
                    if (verificarSeleccionVisibilidad(cod)) { verificado = true; };
                    textBox1.Text = codVisibilidadAcambiar.ToString();
                }
            }
            catch { }
        }

        private bool verificarSeleccionVisibilidad(int codVis)
        {
            if ((codVis == 10006) && (!puedeUsarGratis)) { MessageBox.Show("No disponible visibilidad gratuita"); return false; }
            else
            {
                if ((codVis == 10006) && (puedeUsarGratis)) { radioButton2.Checked = true; radioButton1.Checked = false; radioButton1.Enabled = false; radioButton2.Enabled = false; };
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(radioButton1.Checked) && !(radioButton2.Checked)) { MessageBox.Show("Elegir modalidad envío"); textBox1.Text = codVisibilidad.ToString(); }
            else
            {
                textBox1.Text = codVisibilidad.ToString();
                if (verificado == false) { MessageBox.Show("Elegir visibilidad"); }
                else
                {

                    if (radioButton1.Checked) { envio = true; };
                    //dbQueryHandler.IniciarTransaction();
                    textBox1.Text = codVisibilidad.ToString();
                    bool rta = dbQueryHandler.actualizarVisibilidad(codPub, codVisibilidadAcambiar, envio);
                    //if (rta) { MessageBox.Show("Éxito"); dbQueryHandler.commit(); } else { MessageBox.Show("Error"); dbQueryHandler.rollback(); };
                    if (rta) { MessageBox.Show("Éxito"); } else { MessageBox.Show("Error"); };
                    pant.cargar();
                    this.Close();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
    class dbQueryHandlerVisibilidades 
    {
        public SqlCommand consultaVisibilidades() 
        {
            SqlCommand comand = new SqlCommand("select * from GROUP_APROVED.Visibilidades", DbConnection.connection.getdbconnection());
            return comand;
        }

        public void IniciarTransaction()
        {
            SqlCommand command = new SqlCommand("Begin Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();

        }

        public void rollback()
        {
            SqlCommand command = new SqlCommand("Rollback Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();

        }

        public void commit()
        {
            SqlCommand command = new SqlCommand("Commit Transaction", DbConnection.connection.getdbconnection());
            command.ExecuteNonQuery();

        }
        
        public bool actualizarVisibilidad(int codPublic, int codVisib, bool envio)
        {
            char pubEnvio;
            if (envio == true) { pubEnvio = 'V'; } else pubEnvio = 'F';

            SqlCommand comando = new SqlCommand("GROUP_APROVED.actualizarVisibilidad", DbConnection.connection.getdbconnection());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@pubEnvio", pubEnvio);
            comando.Parameters.AddWithValue("@codPublic", codPublic);
            comando.Parameters.AddWithValue("@codVisib", codVisib);
            
            SqlParameter retVal = new SqlParameter("@rta", SqlDbType.Int);
            comando.Parameters.Add(retVal);
            retVal.Direction = ParameterDirection.Output;
            comando.ExecuteNonQuery();

            int mensajeRespuesta = Convert.ToInt32(comando.Parameters["@rta"].Value);
            if (mensajeRespuesta==1) { return true; } else { return false; };
        }

    }
}

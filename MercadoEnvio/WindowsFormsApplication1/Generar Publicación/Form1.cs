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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class Form1 : Form
    {
        DbQueryHandler dbQueryHandler = new DbQueryHandler();
        Dictionary<String, String> estados;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearPublicacion cp = new CrearPublicacion();
            cp.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            estados = dbQueryHandler.cargarEstados();
            SqlDataReader dataReader = dbQueryHandler.cargarPublicaciones();

            while (dataReader.Read())
            {

                dataGridView1.Rows.Add(dataReader.GetInt32(0).ToString(), dataReader.GetString(1), estados[dataReader.GetInt32(2).ToString()]);
            }
            dataReader.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0)
            {

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string pubId = Convert.ToString(selectedRow.Cells["col1"].Value);
                string strest = Convert.ToString(selectedRow.Cells["col3"].Value);

                if (strest == "Finalizada")
                {
                    MessageBox.Show("No se puede modificar una publicacion finalizada.");
                }
                else
                {
                    Form2 f2 = new Form2(Convert.ToInt32(pubId), "modificacion");
                    f2.Show();
                    
                }
            }

            else
            {
                MessageBox.Show("Debe Seleccionar al menos una publicacion");
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
             if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string pubId = Convert.ToString(selectedRow.Cells["col1"].Value);

                string strest = Convert.ToString(selectedRow.Cells["col3"].Value);

                if (strest == "Finalizada")
                {
                    MessageBox.Show("No se puede activar una publicacion finalizada.");
                }
                else
                {
                    dbQueryHandler.activarPub(pubId, dbQueryHandler.getEstado("Activa"));

                    selectedRow.Cells["col3"].Value = "Activa";
                }
            }
             else
             {
                 MessageBox.Show("Debe Seleccionar al menos una publicacion");
             }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string pubId = Convert.ToString(selectedRow.Cells["col1"].Value);

                string strest = Convert.ToString(selectedRow.Cells["col3"].Value);

                if (strest == "Finalizada")
                {
                    MessageBox.Show("No se puede pausar una publicacion finalizada.");
                }
                else
                {
                    dbQueryHandler.activarPub(pubId, dbQueryHandler.getEstado("Pausada"));

                    selectedRow.Cells["col3"].Value = "Pausada";
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar al menos una publicacion");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string pubId = Convert.ToString(selectedRow.Cells["col1"].Value);

                string strest = Convert.ToString(selectedRow.Cells["col3"].Value);

                if (strest == "Finalizada")
                {
                    MessageBox.Show("No se puede finalizar una publicacion finalizada.");
                }
                else
                {

                    dbQueryHandler.activarPub(pubId, dbQueryHandler.getEstado("Finalizada"));

                    selectedRow.Cells["col3"].Value = "Finalizada";
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar al menos una publicacion");
            }
        }
    }

    class DbQueryHandler
    {
        public SqlDataReader cargarPublicaciones()
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Cod, Publicacion_Desc, Publicacion_Estado from GROUP_APROVED.Publicaciones where Id_Usuario = " + CurrentUser.user.getUserId(), DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            return dataReader;
        }

        public String cargarEstado(String Id)
        {
            SqlCommand cmd = new SqlCommand("select Descripcion from GROUP_APROVED.Estado_Publ where ID_Est = " + Id, DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            String estado = dataReader.GetString(0);
            dataReader.Close();
            return estado;

        }

        public Dictionary<string, string> cargarEstados()
        {
            SqlCommand cmd = new SqlCommand("select Id_Est,Descripcion from GROUP_APROVED.Estado_Publ", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            var estados = new Dictionary<string, string>();

            while (dataReader.Read())
            {
                estados[dataReader.GetInt32(0).ToString()] = dataReader.GetString(1);
            }

            dataReader.Close();

            return estados;
        }

        public void activarPub(String pubId, String estadoId) {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Estado = "+ estadoId +" where Publicacion_Cod = "+ pubId , DbConnection.connection.getdbconnection());
            cmd.ExecuteNonQuery();
        }
        public String getEstado(String est)
        {
            SqlCommand cmd = new SqlCommand("select Id_Est from GROUP_APROVED.Estado_Publ where Descripcion = '" + est + "'", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            String estado;

            var estados = new Dictionary<string, string>();

            dataReader.Read();
            estado = dataReader.GetInt32(0).ToString();
            dataReader.Close();

            return estado;

        }
    }
}

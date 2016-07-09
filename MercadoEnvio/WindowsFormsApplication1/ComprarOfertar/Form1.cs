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

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class Form1 : Form
    {
        DbQueryHandler dbQueryHandler = new DbQueryHandler();
        List<Publicacion> pList = new List<Publicacion>();
        Dictionary<string, string> rubros;
        Dictionary<string, string> rubrosId = new Dictionary<string,string>();
        Dictionary<string, string> visibilidades;
        int pubIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rubros = dbQueryHandler.getRubros();

            foreach (KeyValuePair<string, string> entry in rubros)
            {
                listBox1.Items.Add(entry.Value);
                rubrosId[entry.Value] = entry.Key;
            }

            visibilidades = dbQueryHandler.getVisibilidades();
            button2.Enabled = false;
            button4.Enabled = false;

            

            SqlDataReader dataReader = dbQueryHandler.cargarPublicaciones();

            while (dataReader.Read())
            {

                Publicacion p = new Publicacion();
                p.setCodigo(dataReader.GetInt32(0).ToString());
                p.setDesc(dataReader.GetString(1));
                p.setPrecio(dataReader.GetDecimal(2).ToString());
                if (dataReader.GetString(3) == "V")
                {
                    p.setEnvio(true);
                }
                else
                {
                    p.setEnvio(false);
                }

                p.setRubro(rubros[dataReader.GetDecimal(4).ToString()]);
                p.setVisibilidad(visibilidades[dataReader.GetDecimal(5).ToString()]);
                p.setTipo(dataReader.GetString(6));

                pList.Add(p);

            }
            dataReader.Close();

            if (pList.Count() < 5){
                button3.Enabled = false;
            }
            int i = 0;

            int fin = pubIndex + 4;

            for (i = pubIndex; i <= fin; i++)
            {

                if (pubIndex <= (pList.Count() - 1))
                {
                    Publicacion p = pList[pubIndex];

                    string envio;

                    if (p.getEnvio() == true)
                    {
                        envio = "Si";
                    }
                    else { envio = "No"; }

                    dataGridView1.Rows.Add(p.getCodigo(),p.getDesc(),p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                    pubIndex++;
                }

              
            }
         
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (pList.Count() < pubIndex + 5) {
                button3.Enabled = false;
                button5.Enabled = false;
            }
           
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            button2.Enabled = true;
            button4.Enabled = true;
            
            int i = 0;

            int fin = pubIndex + 4;

            for (i = pubIndex; i <= fin; i++)
            {

                if (pubIndex <= (pList.Count() - 1))
                {
                    Publicacion p = pList[pubIndex];

                    string envio;

                    if (p.getEnvio() == true)
                    {
                        envio = "Si";
                    }
                    else { envio = "No"; }

                    dataGridView1.Rows.Add(p.getCodigo(), p.getDesc(), p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                    pubIndex++;
                }

            }
 
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (pubIndex == pList.Count)
            {
                int resto = pList.Count % 5;

                if (resto == 0)
                {
                    pubIndex = pubIndex - 10;
                }
                else
                {
                    pubIndex = pubIndex - 5 - resto;
                }

            }
            else {
                pubIndex = pubIndex - 10;
            }

            if (pubIndex == 0){
                button2.Enabled = false;
                button4.Enabled = false;
            }

            button3.Enabled = true;
            button5.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
  

            int i = 0;

            int fin = pubIndex + 4;

            for (i = pubIndex; i <= fin; i++)
            {

                if (pubIndex <= (pList.Count() - 1))
                {
                    Publicacion p = pList[pubIndex];

                    string envio;

                    if (p.getEnvio() == true)
                    {
                        envio = "Si";
                    }
                    else { envio = "No"; }

                    dataGridView1.Rows.Add(p.getCodigo(), p.getDesc(), p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                    pubIndex++;
                }

            }

          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pubIndex = 0;
            int i = 0;
            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button5.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            int fin = pubIndex + 4;

            for (i = pubIndex; i <= fin; i++)
            {

                if (pubIndex <= (pList.Count() - 1))
                {
                    Publicacion p = pList[pubIndex];

                    string envio;

                    if (p.getEnvio() == true)
                    {
                        envio = "Si";
                    }
                    else { envio = "No"; }

                    dataGridView1.Rows.Add(p.getCodigo(), p.getDesc(), p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                    pubIndex++;
                }

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int resto = pList.Count % 5;

            if (resto == 0)
            {
                pubIndex = (pList.Count - 5);
            }
            else {
                pubIndex = pList.Count - resto;
            }

            button3.Enabled = false;
            button5.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            int fin = pubIndex + 4;
            int i = 0;
            for (i = pubIndex; i <= fin; i++)
            {

                if (pubIndex <= (pList.Count() - 1))
                {
                    Publicacion p = pList[pubIndex];

                    string envio;

                    if (p.getEnvio() == true)
                    {
                        envio = "Si";
                    }
                    else { envio = "No"; }

                    dataGridView1.Rows.Add(p.getCodigo(), p.getDesc(), p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                    pubIndex++;
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            int selectedItems = 0;
            bool status = true;
            List<String> rubrosSeleccionadosId = new List<String>();

           


             SqlDataReader dataReader;
            if (radioButton1.Checked == true)
            {
                rubrosSeleccionadosId.Clear();

                foreach (var item in listBox1.SelectedItems)
                {
                    rubrosSeleccionadosId.Add(rubrosId[item.ToString()]);
                    selectedItems++;
                }

                if (selectedItems == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un rubro");
                    status = false;
                    dataReader = null;
                }
                else {
                    
                    pList.Clear();
                    pList = new List<Publicacion>();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    dataReader = dbQueryHandler.cargarPublicacionesPorRubro(rubrosSeleccionadosId);
                }

            }
            else {
                if (richTextBox1.Text == "") {
                    MessageBox.Show("Debe escribir algo en la descripción");
                    status = false;
                    dataReader = null;
                }
                else
                {
                    pList.Clear();
                    pList = new List<Publicacion>();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    dataReader = dbQueryHandler.cargarPublicacionesPorDesc(richTextBox1.Text);
                }
            }


            if (status == true)
            {
                while (dataReader.Read())
                {

                    Publicacion p = new Publicacion();
                    p.setCodigo(dataReader.GetInt32(0).ToString());
                    p.setDesc(dataReader.GetString(1));
                    p.setPrecio(dataReader.GetDecimal(2).ToString());
                    if (dataReader.GetString(3) == "V")
                    {
                        p.setEnvio(true);
                    }
                    else
                    {
                        p.setEnvio(false);
                    }

                    p.setRubro(rubros[dataReader.GetDecimal(4).ToString()]);
                    p.setVisibilidad(visibilidades[dataReader.GetDecimal(5).ToString()]);
                    p.setTipo(dataReader.GetString(6));

                    pList.Add(p);

                }
                dataReader.Close();
                if (pList.Count() < 5)
                {
                    button3.Enabled = false;
                }
                int i = 0;

                int fin = pubIndex + 4;

                for (i = pubIndex; i <= fin; i++)
                {

                    if (pubIndex <= (pList.Count() - 1))
                    {
                        Publicacion p = pList[pubIndex];

                        string envio;

                        if (p.getEnvio() == true)
                        {
                            envio = "Si";
                        }
                        else { envio = "No"; }

                        dataGridView1.Rows.Add(p.getCodigo(), p.getDesc(), p.getTipo(), envio, p.getRubro(), p.getVisibilidad(), p.getPrecio());

                        pubIndex++;
                    }


                }
            }
           
          

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string pubId = Convert.ToString(selectedRow.Cells["Column6"].Value);

               //comprobar que la publicacion no sea de uno mismo

                Form2 f2 = new Form2(Convert.ToInt32(pubId));
                f2.Show();

                this.Close();

            }

            else
            {
                MessageBox.Show("Debe Seleccionar al menos una publicacion");
            }
        }
    }

    class Publicacion { 

        String codigo = "";
        String desc  = "";
        String precio = "";
        String tipo = "";
        bool envio = false;
        String rubro = "";
        String visibilidad = "";


        public String getTipo()
        {
            return tipo;
        }
        public String getCodigo()
        {
            return codigo;
        }
        public String getDesc()
        {
            return desc;
        }
        public String getPrecio()
        {
            return precio;
        }
        public bool getEnvio()
        {
            return envio;
        }
        public String getRubro()
        {
            return rubro;
        }
        public String getVisibilidad()
        {
            return visibilidad;
        }

        public void setCodigo(string cod){
            codigo = cod;
        }
        public void setDesc(string des)
        {
            desc = des;
        }
        public void setPrecio(string prec)
        {
            precio = prec;
        }
        public void setEnvio(bool env)
        {
            envio = env;
        }
        public void setRubro(string rub)
        {
            rubro = rub;
        }
        public void setVisibilidad(string vis)
        {
            visibilidad = vis;
        }
        public void setTipo(string tip)
        {
            tipo = tip;
        }
    }

    class DbQueryHandler
    {

        public SqlDataReader cargarPublicacionesPorRubro(List<String> idrubros)
        {
            String rubros = "";

            foreach (String rubro in idrubros) {

                if (rubros == "")
                {
                    rubros = rubro;
                }
                else
                {
                    rubros = rubros + "," + rubro;
                }
            }



            SqlCommand cmd = new SqlCommand("select Publicacion_Cod, Publicacion_Desc,Publicacion_Precio,Publicacion_Acepta_Envio,Id_Rubro,Visibilidad_Cod,Publicacion_Tipo from GROUP_APROVED.Publicaciones where Id_Rubro in(" + rubros + ") order by Visibilidad_Cod", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            return dataReader;
        }

        public SqlDataReader cargarPublicacionesPorDesc(String desc)
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Cod, Publicacion_Desc,Publicacion_Precio,Publicacion_Acepta_Envio,Id_Rubro,Visibilidad_Cod,Publicacion_Tipo from GROUP_APROVED.Publicaciones where Publicacion_Desc like '%" + desc + "%' order by Visibilidad_Cod", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
           
            return dataReader;
        }

        public SqlDataReader cargarPublicaciones()
        {

            SqlCommand cmd = new SqlCommand("select Publicacion_Cod, Publicacion_Desc,Publicacion_Precio,Publicacion_Acepta_Envio,Id_Rubro,Visibilidad_Cod,Publicacion_Tipo from GROUP_APROVED.Publicaciones order by Visibilidad_Cod", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();

            return dataReader;
        }

        public Dictionary<string, string> getRubros()
        {
            SqlCommand cmd = new SqlCommand("select Id_Rubro, Rubro_Desc_Completa from GROUP_APROVED.Rubros" ,DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            var rubros = new Dictionary<string, string>();

            while (dataReader.Read())
            {
                rubros[dataReader.GetDecimal(0).ToString()] = dataReader.GetString(1);
            }

            dataReader.Close();

            return rubros;
        }

        public Dictionary<string, string> getVisibilidades()
        {
            SqlCommand cmd = new SqlCommand("select Visibilidad_Cod, Visibilidad_Desc from GROUP_APROVED.Visibilidades", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            var visibs = new Dictionary<string, string>();

            while (dataReader.Read())
            {
                visibs[dataReader.GetDecimal(0).ToString()] = dataReader.GetString(1);
            }

            dataReader.Close();

            return visibs;
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

        public void activarPub(String pubId, String estadoId)
        {
            SqlCommand cmd = new SqlCommand("update GROUP_APROVED.Publicaciones set Publicacion_Estado = " + estadoId + " where Publicacion_Cod = " + pubId, DbConnection.connection.getdbconnection());
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

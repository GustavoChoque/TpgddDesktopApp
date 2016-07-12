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

namespace WindowsFormsApplication1.Listado_Estadistico
{
    public partial class listado : Form
    {
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable tablaDatos = new DataTable();
        DbQueryHandlerListado dbQueryHandler = new DbQueryHandlerListado();

        public listado()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                if ((anio<2017) && (anio>1900))
                {llenarTablaPorTrimestre(anio,1,3);};
            }
            catch{MessageBox.Show("Ingresar año válido");};

        }

        private void llenarTablaPorTrimestre(int anio, int mesin, int mesFin)
        {
            SqlDataAdapter dataAdapter = null;
            DataTable tablaDatos = null;
            try
            {
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                dataAdapter.SelectCommand = dbQueryHandler.vendedoresMenosVentas(anio, mesin, mesFin);
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
            }
            catch { }
        }

        private void llenarTablaPorTrimestreConRubro(int anio, int mesin, int mesFin, string rubro)
        {
            SqlDataAdapter dataAdapter = null;
            DataTable tablaDatos = null;
            try
            {
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                dataAdapter.SelectCommand = dbQueryHandler.compradoresMasCompras(anio, mesin, mesFin,rubro);
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
            }
            catch { }
        }

        private void llenarTablaPorAnoYmesVendMasFact(int anio, int mes)
        {
            SqlDataAdapter dataAdapter = null;
            DataTable tablaDatos = null;
            try
            {
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                dataAdapter.SelectCommand = dbQueryHandler.vendedoresMasFacturas(anio, mes);
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
            }
            catch { }
        }

        private void llenarTablaPorAnoYmesVendMasMonto(int anio, int mes)
        {
            SqlDataAdapter dataAdapter = null;
            DataTable tablaDatos = null;
            
                dataAdapter = new SqlDataAdapter();
                tablaDatos = new DataTable();
                dataAdapter.SelectCommand = dbQueryHandler.vendedoresMasMonto(anio, mes);
                dataAdapter.Fill(tablaDatos);
                dataGridView1.DataSource = tablaDatos;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                if ((anio < 2017) && (anio > 1900))
                { llenarTablaPorTrimestre(anio, 4, 6); };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                if ((anio < 2017) && (anio > 1900))
                { llenarTablaPorTrimestre(anio, 7, 9); };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                string rubro;
                if (comboBox1.SelectedIndex != -1) { 
                    rubro = comboBox1.SelectedItem.ToString();
                    if ((anio < 2017) && (anio > 1900))
                    { llenarTablaPorTrimestreConRubro(anio, 1, 3, rubro); }
                else { MessageBox.Show("Seleccionar rubro válido"); };
                 };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                string rubro;
                if (comboBox1.SelectedIndex != -1)
                {
                    rubro = comboBox1.SelectedItem.ToString();
                    if ((anio < 2017) && (anio > 1900))
                    { llenarTablaPorTrimestreConRubro(anio, 4, 6, rubro); }
                    else { MessageBox.Show("Seleccionar rubro válido"); };
                };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                string rubro;
                if (comboBox1.SelectedIndex != -1)
                {
                    rubro = comboBox1.SelectedItem.ToString();
                    if ((anio < 2017) && (anio > 1900))
                    { llenarTablaPorTrimestreConRubro(anio, 7, 9, rubro); }
                    else { MessageBox.Show("Seleccionar rubro válido"); };
                };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                int mes = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                llenarTablaPorAnoYmesVendMasFact(anio, mes);
            }
            catch { MessageBox.Show("Ingresar año y mes válidos"); };
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                int mes = Convert.ToInt32(comboBox3.SelectedItem.ToString());
                llenarTablaPorAnoYmesVendMasMonto(anio, mes);
            }
            catch { MessageBox.Show("Ingresar año y mes válidos"); };
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                if ((anio < 2017) && (anio > 1900))
                { llenarTablaPorTrimestre(anio, 10, 12); };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(textBox1.Text.ToString());
                string rubro;
                if (comboBox1.SelectedIndex != -1)
                {
                    rubro = comboBox1.SelectedItem.ToString();
                    if ((anio < 2017) && (anio > 1900))
                    { llenarTablaPorTrimestreConRubro(anio, 10, 12, rubro); }
                    else { MessageBox.Show("Seleccionar rubro válido"); };
                };
            }
            catch { MessageBox.Show("Ingresar año válido"); };
        }

        


        
    }

    class DbQueryHandlerListado 
    {
        public SqlCommand vendedoresMenosVentas(int añobusacod, int mesinit,int mesfin) 
        {
            SqlCommand comando = new SqlCommand(
            @"select TOP 5 COUNT(P.Publicacion_Stock), U.Username, c.Cli_Nombre, c.Cli_Apellido 
            from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr 
            join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario left 
            join GROUP_APROVED.Visibilidades V ON P.Visibilidad_Cod = v.Visibilidad_Cod
            WHERE year(p.Publicacion_Fecha) = "+añobusacod +
            " and month(p.Publicacion_Fecha) between "+mesinit+
            " and "+mesfin+@" and v.Visibilidad_Porcentaje between 0.15 and 0.30
            GROUP BY U.Username,c.Cli_Nombre, c.Cli_Apellido
            order by 1 DESC,  avg (v.Visibilidad_Porcentaje)", DbConnection.connection.getdbconnection());
            return comando;

        }

        public SqlCommand compradoresMasCompras(int añobusacod, int mesinit,int messfin, string rubroelegido) 
        {
            SqlCommand comando = new SqlCommand(
            @"select TOP 5 u.Username, count(Compra_Cantidad), c1.Cli_Apellido, c1.Cli_Nombre from GROUP_APROVED.Clientes c1 
            join GROUP_APROVED.Usuarios u on c1.Id_Usuario = u.Id_Usr 
            join GROUP_APROVED.Compras c on u.Id_Usr = c.Id_Usuario 
            left join GROUP_APROVED.Publicaciones p on c.Publicacion_Cod = p.Publicacion_Cod 
            join GROUP_APROVED.Rubros r on p.Id_Rubro = r.Id_Rubro
            where year(c.Compra_Fecha) =" + añobusacod +
            "and month(c.Compra_Fecha) between " + mesinit + " and " + messfin +
            "and r.Rubro_Desc_Completa = " + rubroelegido +
            @"group by u.Username, c1.Cli_Apellido, c1.Cli_Nombre
            order by 2 DESC ", DbConnection.connection.getdbconnection());
            return comando;
        }

        public SqlCommand vendedoresMasFacturas(int añobusacod, int mesbuscado) 
        {
            SqlCommand comando = new SqlCommand(
            @"select TOP 5 u.Username, c.Cli_Apellido, c.Cli_Nombre, count(f.Nro_Fact) 
            from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr 
            join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario 
            join GROUP_APROVED.Facturas f on p.Publicacion_Cod = f.Publicacion_Cod
            where year(f.Fact_Fecha) = "+añobusacod+" and month(f.Fact_Fecha) = "+mesbuscado+
            @"group by u.Username, c.Cli_Apellido, c.Cli_Nombre
            order by 4 DESC", DbConnection.connection.getdbconnection());
            return comando;
        }

        public SqlCommand vendedoresMasMonto(int añobusacod, int mesbuscado) 
        {
            SqlCommand comando = new SqlCommand(
            @"select TOP 5 u.Username, c.Cli_Apellido, c.Cli_Nombre, SUM(f.Fact_Total) 
            from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr 
            join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario 
            join GROUP_APROVED.Facturas f on p.Publicacion_Cod = f.Publicacion_Cod
            where year(f.Fact_Fecha) = "+añobusacod+" and month(f.Fact_Fecha) = "+mesbuscado+
            @"group by u.Username, c.Cli_Apellido, c.Cli_Nombre
            order by 4 DESC", DbConnection.connection.getdbconnection());
            return comando;
        }
    }
}

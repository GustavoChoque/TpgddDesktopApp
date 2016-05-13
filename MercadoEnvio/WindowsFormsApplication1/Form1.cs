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

namespace WindowsFormsApplication1
{

    public class DbClass
{
        SqlConnection con;
        SqlCommand cmd;

        public DbClass()
        {
           con = new SqlConnection("data source = WIN-4UI72Q2HO06\\SQLSERVER2012; database = test;user = gd; password = gd2016");
        
           con.Open(); //if needed)
            
        }

        public void closeConnection()
        {
            con.Close();
        }


        public SqlDataReader getUser(String user, String password)
        {
            
            SqlCommand cmd = new SqlCommand("select * from users where (username = '" + user + "' and password = '" + password + "')", con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
    
        }

        public SqlDataReader getUser(String user)
        {

            SqlCommand cmd = new SqlCommand("select * from users where (username = '" + user + "')", con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;

        }

        public int resetUserTries(String user)
        {
            cmd = new SqlCommand("update users set tries = 0 where username = '" + user + "'", con);
            int res = cmd.ExecuteNonQuery();
            return res;
        }

        public int updateUserTries(String user, int tries)
        {
            cmd = new SqlCommand("update users set tries = " + tries + " where username = '" + user + "'", con);
            int res = cmd.ExecuteNonQuery();
            return res;
        }

}

    public partial class Form1 : Form
    {
        private DbClass dbConnection = new DbClass();   //Conectar a la base de datos

        public Form1()
        {
            InitializeComponent();
            this.Text = "Login form";
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            button1.Text = "Login";
            label1.Text = "User";
            label2.Text = "Password";
            textBox1.Text = "user1";
            textBox2.Text = "abc123";
            
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = textBox1.Text;
            String password = textBox2.Text;
            int tries = 0;
            int result;

            String u;
            String p;

            SqlDataReader dataReader = dbConnection.getUser(user,password);

            if (dataReader.Read() != false) //Si encontro el user y pass
            {

                u = dataReader.GetString(0);
                p = dataReader.GetString(1);
                tries = dataReader.GetInt32(2);
                dataReader.Close();

                if (tries >= 3) //Si la cuenta esta bloqueada
                {
                    textBox3.Text = "Usuario bloqueado";
                }
                else
                {
                    result = dbConnection.resetUserTries(user);

                    if (result == 1)
                    {
                        label3.Text = u;
                        label4.Text = p;
                        label5.Text = tries.ToString();
                        textBox3.Text = "Usuario logueado correctamente";
                    }
                    else
                    {
                        textBox3.Text = "Error SQL";
                    }
                }
               

            }
            else //Si el user o pass estan mal
            {

                label3.Text = "Error en el usuario o contraseña";

                dataReader.Close();

                dataReader = dbConnection.getUser(user);
                

                if (dataReader.Read() == false)
                {
                    label5.Text = "No existe usuario";

                }
                else
                {
                    tries = dataReader.GetInt32(2);
                    dataReader.Close();
                    tries = tries + 1;
                    dbConnection.updateUserTries(user,tries);
                    label5.Text = "Existe usuario";
                }
            }

            dataReader.Close();

        }


    }
}

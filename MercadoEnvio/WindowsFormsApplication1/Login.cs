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
using WindowsFormsApplication1.Pantalla_Principal;

namespace WindowsFormsApplication1
{

    

    public partial class Login : Form
    {
        private DbClass dbConnection = new DbClass();   //Conectar a la base de datos

        

        public Login()
        {
            InitializeComponent();
            this.Text = "Login";
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
            button1.Text = "Login";
            label1.Text = "User";
            label2.Text = "Password";
            textBox1.Text = "user1";
            textBox2.Text = "abc123";
            label3.Text = "";
            
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = textBox1.Text;
            String password = textBox2.Text;
            int tries = 0;
            String loginResult;



            //Logueo con clave encriptada con SHA256: devuelve true o false del stored procedure LoginUsuario
            loginResult = dbConnection.userLogin(user,password);
            //

            if (loginResult.Equals("True")) //Si encontro el user y pass
            {
                SqlDataReader dataReader = dbConnection.getUser(user);
                dataReader.Read();
                tries = dataReader.GetInt32(2);
                dataReader.Close();

                if (tries >= 3) //Si la cuenta esta bloqueada
                {
                    MessageBox.Show("Usuario bloqueado");
                }
                else
                {
                    
                    if (dbConnection.resetUserTries(user) == 1)
                    {
                        //Abrir form principal
                        this.Hide();
                        Principal pantallaPrincipal = new Principal();
                        pantallaPrincipal.Show();

                        //agrego asignacion variable usuario actual en Principal - Lautaro
                        pantallaPrincipal.setearUsuarioEnUso(user);
                        //seteo la conexion en pantalla principal
                        this.setearConexionPPpal(pantallaPrincipal);
                        //verifico el acceso del usuario a las funciones ahora que principal ya esta inicializado y conectado
                        pantallaPrincipal.verificarAccesos();
                        
                    }
                    else
                    {
                        label3.Text = "Error SQL";
                    }
                }

            }
            else
            {
                MessageBox.Show("Error en el usuario o contraseña");

                SqlDataReader dataReader = dbConnection.getUser(user);


                if (dataReader.Read() == false) //Si el usuario no existe
                {

                }
                else //Si existe pero la contraseña esta mal
                {
                    tries = dataReader.GetInt32(2);
                    tries = tries + 1;
                    dataReader.Close();
                    dbConnection.updateUserTries(user, tries);

                }
                dataReader.Close();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void setearConexionPPpal(Principal pantalla)
        {
            pantalla.setearConexion(this.dbConnection);
        }

    }
    public class DbClass
    {
        SqlConnection con;
        SqlCommand cmd;

        public DbClass()
        {
            con = new SqlConnection("data source = .\\SQLSERVER2012; database =test;user = gd; password = gd2016");

            con.Open(); 

        }

        public void closeConnection()
        {
           con.Close();
        }

        public String userLogin(String user, String password){
            String ret;
            using (var command = new SqlCommand("LoginUsuario", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                command.Parameters.Add(new SqlParameter("@username", user));
                command.Parameters.Add(new SqlParameter("@password", password));

                SqlParameter retVal = new SqlParameter("@result", SqlDbType.Bit);
                command.Parameters.Add(retVal);
                retVal.Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                ret = command.Parameters["@result"].Value.ToString();
            }
            return ret;
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
        
        public SqlConnection getdbconection()
        {
            return this.con;
        }
    }
}

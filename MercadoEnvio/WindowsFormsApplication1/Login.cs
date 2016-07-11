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
using System.IO;
using WindowsFormsApplication1.Pantalla_Principal;

namespace WindowsFormsApplication1
{

    

    public partial class Login : Form
    {

        DbQueryHandler dbQueryHandler = new DbQueryHandler();

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
            textBox1.Text = "Admin";
            textBox2.Text = "w23e";
            label3.Text = "";

            StreamReader reader = File.OpenText("../../../date.txt");
            String date;

            if ((date = reader.ReadLine()) == null)
            {
                MessageBox.Show("Error al cargar la fecha");
            }

            reader.Close();

            CustomDate.date.setDate(date);

            label4.Text = CustomDate.date.getDate();

            dbQueryHandler.updatePubStates();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = textBox1.Text;
            String password = textBox2.Text;
            Int16 tries = 0;
            String loginResult;
            int userId;
            CurrentUser.user.setUsername(user);


            //Logueo con clave encriptada con SHA256: devuelve true o false del stored procedure LoginUsuario
            loginResult = dbQueryHandler.userLogin(user, password);
            //

            if (loginResult.Equals("True")) //Si encontro el user y pass
            {
                SqlDataReader dataReader = dbQueryHandler.getUser(CurrentUser.user.getUsername());
                dataReader.Read();
                userId = dataReader.GetInt32(0);
                CurrentUser.user.setUserId(userId);
                tries = dataReader.GetInt16(4);
                dataReader.Close();

                if (tries >= 3) //Si la cuenta esta bloqueada
                {
                    MessageBox.Show("Usuario bloqueado");
                }
                else
                {

                    if (dbQueryHandler.resetUserTries(CurrentUser.user.getUsername()) == 1)
                    {
                        //Abrir form principal
                        this.Hide();
                        Principal pantallaPrincipal = new Principal();
                        pantallaPrincipal.Show();
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

                SqlDataReader dataReader = dbQueryHandler.getUser(CurrentUser.user.getUsername());


                if (dataReader.Read() == false) //Si el usuario no existe
                {
                    
                }
                else //Si existe pero la contraseña esta mal
                {
                    tries = dataReader.GetInt16(4);
                    tries++;
                    dataReader.Close();
                    dbQueryHandler.updateUserTries(CurrentUser.user.getUsername(), tries);

                }
                dataReader.Close();
            }

        }

    }

    class CurrentUser
    {
        String userName = "";
        int userId = 0;

        public void setUserId(int id)
        {
            userId = id;
        }
        public int getUserId()
        {
            return userId;
        }
        static CurrentUser instance = new CurrentUser();

        CurrentUser()
        {
        }
        public static CurrentUser user
        {
            get { return instance; }
        }

        public void setUsername(String user)
        {
            userName = user;
        }

        public String getUsername()
        {
            return this.userName;
        }
    }



    class DbConnection
    {
        SqlConnection con;

        static DbConnection dbCon = new DbConnection();

        DbConnection()
        {
            con = new SqlConnection("data source = .\\SQLSERVER2012; database =GD1C2016;user = gd; password = gd2016");

            con.Open(); 

        }
        public static DbConnection connection
        {
            get { return dbCon; }
        }

        public void closeConnection()
        {
            con.Close();
        }
 
        public SqlConnection getdbconnection()
        {
            return this.con;
        }
    }

    class CustomDate
    {
        String currentDate = "";
       
        static CustomDate instance = new CustomDate();

        CustomDate()
        {

        }
        public static CustomDate date
        {
            get { return instance; }
        }

        public void setDate(String date)
        {
       
            currentDate = date;
        }

        public String getDate()
        {
            return this.currentDate;
        }
    }

    public class DbQueryHandler
    {
        SqlCommand cmd;


        public String userLogin(String user, String password){
            String ret;
            using (var command = new SqlCommand("GROUP_APROVED.LoginUsuario", DbConnection.connection.getdbconnection())
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

            SqlCommand cmd = new SqlCommand("select * from GROUP_APROVED.Usuarios where (Username = '" + user + "')", DbConnection.connection.getdbconnection());
            SqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;

        }

        public int resetUserTries(String user)
        {
            cmd = new SqlCommand("update GROUP_APROVED.Usuarios set intentos = 0 where Username = '" + user + "'", DbConnection.connection.getdbconnection());
            int res = cmd.ExecuteNonQuery();
            return res;
        }

        public int updateUserTries(String user, Int16 tries)
        {
            cmd = new SqlCommand("update GROUP_APROVED.Usuarios set intentos = " + tries + " where Username = '" + user + "'", DbConnection.connection.getdbconnection());
            int res = cmd.ExecuteNonQuery();
            return res;
        }

        public void updatePubStates()
        {
            cmd = new SqlCommand("update GROUP_APROVED.Publicaciones  set Publicacion_Estado = CASE WHEN Publicacion_Fecha_Venc <= CAST('" + CustomDate.date.getDate() + "' AS DATE)  THEN 3  ELSE CASE WHEN Publicacion_estado != 0 AND Publicacion_estado != 2 THEN 1 ELSE Publicacion_estado END END ", DbConnection.connection.getdbconnection());
            int res = cmd.ExecuteNonQuery();

            /*using (var command = new SqlCommand("GROUP_APROVED.facturacionSubastasVencidas", DbConnection.connection.getdbconnection())
            {
                CommandType = CommandType.StoredProcedure

            })
            {  
            res = command.ExecuteNonQuery();
            } */

        }
    
    }
}

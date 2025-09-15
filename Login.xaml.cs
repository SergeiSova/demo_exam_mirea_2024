using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Login : Window
    {
        string ConnectionString = @"Server = ";
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT post, password FROM Entity WHERE Login=@logintext";
            string logintext = login.Text;
            string passwordtext = password.Text;

            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("login", logintext);

            if (reader.Read())
            {
                string dbpassword = reader.GetString(1).ToString();

                if (dbpassword == passwordtext)
                {
                    string role = reader.GetString(0).ToString();

                    login.Text = "";
                    password.Text = "";

                    Window menu = role switch
                    {
                        "Users" => new MenuUser(),
                        _ => null,

                    };

                    menu?.Show();
                    Close();
                }

                else
                {
                    MessageBox.Show("");
                }
            }

            else
            {
                MessageBox.Show("");
            }
        }
    }
}

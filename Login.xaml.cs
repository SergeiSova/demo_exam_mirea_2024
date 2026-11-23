using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string loginText = login.Text.Trim();
            if (loginText.Length == 0 || password.Password.Length == 0)
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            try
            {
                using SqlConnection conn = Db.Open();
                using SqlCommand cmd = new SqlCommand("SELECT Post, PasswordHash FROM Users WHERE Login=@login", conn);
                cmd.Parameters.AddWithValue("@login", loginText);
                using SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read() || !PasswordHasher.Verify(password.Password, reader.GetString(1)))
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }

                Window? menu = reader.GetString(0) switch
                {
                    "Users" => new MenuUser(),
                    "Executor" => new MenuExecutor(),
                    "Manager" => new MenuManager(),
                    _ => null,
                };

                if (menu is null)
                {
                    MessageBox.Show("Для этой роли нет доступного меню.");
                    return;
                }

                menu.Show();
                Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось подключиться к базе данных.");
            }
        }
    }
}

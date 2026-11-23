using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullName.Text) || string.IsNullOrWhiteSpace(Phone.Text) ||
                string.IsNullOrWhiteSpace(Device.Text))
            {
                MessageBox.Show("Заполните ФИО, телефон и оборудование.");
                return;
            }

            const string sql = @"INSERT INTO Entity (Name, Phone, Email, Device, SN, CommentsUser)
                                 VALUES (@Name, @Phone, @Email, @Device, @SN, @Comment)";

            try
            {
                using SqlConnection connection = Db.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", FullName.Text.Trim());
                command.Parameters.AddWithValue("@Phone", Phone.Text.Trim());
                command.Parameters.AddWithValue("@Email", Db.Value(Email.Text));
                command.Parameters.AddWithValue("@Device", Device.Text.Trim());
                command.Parameters.AddWithValue("@SN", Db.Value(SN.Text));
                command.Parameters.AddWithValue("@Comment", Db.Value(Com.Text));
                command.ExecuteNonQuery();

                MessageBox.Show("Заявка сохранена.");
                ClearFields();
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось сохранить заявку.");
            }
        }

        private void ClearFields()
        {
            FullName.Clear();
            Phone.Clear();
            Email.Clear();
            Device.Clear();
            SN.Clear();
            Com.Clear();
        }

        private void Button_Click_1_2(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MenuUser().Show();
            Close();
        }
    }
}

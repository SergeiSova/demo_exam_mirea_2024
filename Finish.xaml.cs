using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Finish : Window
    {
        string ConnectionString = @"Server = ";

        public Finish()
        {
            InitializeComponent();
           LoadAppId();
        }

        string Status()
        {
            return "";
        }

        void LoadAppId()
        {
            string sql = "SELECT id FROM Etity";

            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ApplicationIdComboBox.Items.Add(reader["id"].ToString());
            }
        }

        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = @"UPDATE Entity SET Status=@Status, Start, End, Comment";

            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand( sql, conn);
            cmd.Parameters.AddWithValue("start", AcceptedDatePicker.SelectedDate);
            cmd.Parameters.AddWithValue("Comm", CommentsTextBox.Text);
            cmd.Parameters.AddWithValue("id", ApplicationIdComboBox.SelectedItem.ToString());

            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result > 0 ? "" : "");
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MenuExecutor().Show();
            Close();
        }
    }
}

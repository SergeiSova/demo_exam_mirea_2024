using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Create : Window
    {
        string ConnectionString = @"Server = ";
        public Create()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSEAR INTO Enrity () SET (@)";

            Random random = new Random();
            int id = random.Next(1, 10000);

            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            using SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.Parameters.AddWithValue("ФИО", Name.Text);

            try
            {
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("");
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void Button_Click_1_2(object sender, RoutedEventArgs e)
        {
           Name.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MenuExecutor().Show();
            Close();
        }
    }
}

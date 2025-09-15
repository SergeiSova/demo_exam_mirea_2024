using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace Demo
{
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Sova;TrustServerCertificate=True;Trusted_Connection=True;";
            DataTable dt = new("Entity");
            string sqlQuery = "SELECT id, Name, Phone, Email, Device, SN, CommentsUser FROM Entity";

            using (SqlConnection con = new(connectionString))
            using (SqlCommand cmd = new(sqlQuery, con))
            using (SqlDataAdapter dataAdapter = new(cmd))
            {
                dataAdapter.Fill(dt); // Открывает и закрывает соединение автоматически
            }

            UsersDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MenuManager().Show();
            Close();
        }
    }
}

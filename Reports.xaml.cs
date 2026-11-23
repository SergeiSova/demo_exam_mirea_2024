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
            DataTable dt = new("Entity");
            string sqlQuery = "SELECT id, Name, Phone, Email, Device, SN, CommentsUser, Status, Executor, StartDate, EndDate FROM Entity";

            try
            {
                using SqlConnection con = new(Db.ConnectionString);
                using SqlCommand cmd = new(sqlQuery, con);
                using SqlDataAdapter dataAdapter = new(cmd);
                dataAdapter.Fill(dt);
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось загрузить заявки.");
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

using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Statistics : Window
    {
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Sova;TrustServerCertificate=True;Trusted_Connection=True;";
        public Statistics()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Количество зарегистрированных пользователей
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users", connection);
            RegisteredUsersText.Text = $"Количество зарегистрированных пользователей: {command.ExecuteScalar()}";

            // Количество созданных задач
            command.CommandText = "SELECT COUNT(*) FROM Entity";
            TasksCreatedText.Text = $"Создано задач: {command.ExecuteScalar()}";

            // Количество задач по статусам
            command.CommandText = "SELECT Status, COUNT(*) FROM Entity GROUP BY Status";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                TasksStatusText.Text = "Количество задач по статусам:";
                while (reader.Read())
                {
                    TasksStatusText.Text += $"\n{reader["Status"]}: {reader[1]}";
                }
            }

            // Количество уникальных устройств
            command.CommandText = "SELECT COUNT(DISTINCT Device) FROM Entity";
            UniqueDevicesText.Text = $"Количество уникальных устройств: {command.ExecuteScalar()}";

            // Кто из исполнителей выполняет больше всего задач
            command.CommandText = "SELECT TOP 1 Executor, COUNT(*) AS TaskCount FROM Entity GROUP BY Executor ORDER BY TaskCount DESC";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    TopExecutorText.Text = $"Исполнитель с наибольшим количеством задач: {reader["Executor"]} (Задач: {reader["TaskCount"]})";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MenuManager().Show();
            Close();
        }
    }
}

using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
            try
            {
                LoadStatistics();
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось загрузить статистику.");
            }
        }

        private void LoadStatistics()
        {
            using SqlConnection connection = Db.Open();

            // Количество зарегистрированных пользователей
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users", connection);
            RegisteredUsersText.Text = $"Количество зарегистрированных пользователей: {command.ExecuteScalar()}";

            // Количество созданных задач
            command.CommandText = "SELECT COUNT(*) FROM Entity";
            TasksCreatedText.Text = $"Создано задач: {command.ExecuteScalar()}";

            // Среднее время выполнения
            command.CommandText = @"SELECT AVG(CAST(DATEDIFF(DAY, StartDate, EndDate) AS FLOAT)) FROM Entity
                                    WHERE Status = N'Выполнено' AND StartDate IS NOT NULL AND EndDate IS NOT NULL";
            object average = command.ExecuteScalar();
            AverageCompletionTimeText.Text = average is double days
                ? $"Среднее время выполнения: {days:F1} дн."
                : "Среднее время выполнения: нет завершённых заявок";

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
            command.CommandText = "SELECT TOP 1 Executor, COUNT(*) AS TaskCount FROM Entity WHERE Executor IS NOT NULL GROUP BY Executor ORDER BY TaskCount DESC";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    TopExecutorText.Text = $"Исполнитель с наибольшим количеством задач: {reader["Executor"]} (Задач: {reader["TaskCount"]})";
                }
                else
                {
                    TopExecutorText.Text = "Исполнитель с наибольшим количеством задач: пока нет данных";
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

using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Work : Window
    {
        public Work()
        {
            InitializeComponent();
            LoadData();
        }

        private string? Status()
        {
            if (WaitingRadioButton.IsChecked == true) return "В ожидании";
            if (InProgressRadioButton.IsChecked == true) return "В работе";
            if (NotDoneRadioButton.IsChecked == true) return "Не выполнено";
            if (DoneRadioButton.IsChecked == true) return "Выполнено";
            return null;
        }

        private void LoadData()
        {
            try
            {
                using SqlConnection conn = Db.Open();
                using (SqlCommand ids = new SqlCommand("SELECT id FROM Entity ORDER BY id", conn))
                using (SqlDataReader reader = ids.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdComboBox.Items.Add(reader.GetInt32(0));
                    }
                }

                using SqlCommand executors = new SqlCommand("SELECT Name FROM Executor ORDER BY Name", conn);
                using SqlDataReader executorReader = executors.ExecuteReader();
                while (executorReader.Read())
                {
                    ExecutorComboBox.Items.Add(executorReader.GetString(0));
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось загрузить данные.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string? status = Status();
            if (IdComboBox.SelectedItem is not int id || ExecutorComboBox.SelectedItem is not string executor || status is null)
            {
                MessageBox.Show("Выберите заявку, исполнителя и статус.");
                return;
            }

            const string sql = @"UPDATE Entity
                                 SET Status=@Status, Executor=@Executor, RepairType=@RepairType, FaultDescription=@Fault
                                 WHERE id=@id";

            try
            {
                using SqlConnection conn = Db.Open();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Executor", executor);
                cmd.Parameters.AddWithValue("@RepairType", Db.Value(RepairKindTextBox.Text));
                cmd.Parameters.AddWithValue("@Fault", Db.Value(FaultDescriptionTextBox.Text));
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show(cmd.ExecuteNonQuery() > 0 ? "Заявка обновлена." : "Заявка не найдена.");
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось сохранить заявку.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MenuManager().Show();
            Close();
        }
    }
}

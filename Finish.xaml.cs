using System.Data.SqlClient;
using System.Windows;

namespace Demo
{
    public partial class Finish : Window
    {
        public Finish()
        {
            InitializeComponent();
            LoadAppId();
        }

        private string? Status()
        {
            if (WaitingRadioButton.IsChecked == true) return "В ожидании";
            if (InProgressRadioButton.IsChecked == true) return "В работе";
            if (NotDoneRadioButton.IsChecked == true) return "Не выполнено";
            if (DoneRadioButton.IsChecked == true) return "Выполнено";
            return null;
        }

        private void LoadAppId()
        {
            try
            {
                using SqlConnection conn = Db.Open();
                using SqlCommand cmd = new SqlCommand("SELECT id FROM Entity ORDER BY id", conn);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ApplicationIdComboBox.Items.Add(reader.GetInt32(0));
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось загрузить заявки.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string? status = Status();
            if (ApplicationIdComboBox.SelectedItem is not int id || status is null)
            {
                MessageBox.Show("Выберите заявку и статус.");
                return;
            }

            if (AcceptedDatePicker.SelectedDate > CompletedDatePicker.SelectedDate)
            {
                MessageBox.Show("Дата выполнения не может быть раньше даты принятия.");
                return;
            }

            const string sql = @"UPDATE Entity
                                 SET Status=@Status, StartDate=@Start, EndDate=@End, Comment=@Comment
                                 WHERE id=@id";

            try
            {
                using SqlConnection conn = Db.Open();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Start", Db.Value(AcceptedDatePicker.SelectedDate));
                cmd.Parameters.AddWithValue("@End", Db.Value(CompletedDatePicker.SelectedDate));
                cmd.Parameters.AddWithValue("@Comment", Db.Value(CommentsTextBox.Text));
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
            new MenuExecutor().Show();
            Close();
        }
    }
}

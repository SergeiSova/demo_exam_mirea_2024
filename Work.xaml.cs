using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Windows;

namespace Demo
{
    public partial class Work : Window
    {
        string ConnectionString = @"Server = ";
        ObservableCollection<string> Executer {  get; set; } = new ObservableCollection<string>();
        ObservableCollection<string> AppId { get; set; } = new ObservableCollection<string>();


        public Work()
        {
            InitializeComponent();
            
        }

        string Status()
        {
            return "";
        }
        
        void LoadData (string sql, ObservableCollection<string> collection, string ColumnName, System.Windows.Controls.ComboBox comboBox)
        {
            using SqlConnection conn = new SqlConnection();
            conn.Open ();
            using SqlCommand cmd = new SqlCommand (sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader ();
            while (reader.Read ())
            {
                collection.Add(reader[ColumnName].ToString());
            }

            comboBox.ItemsSource = collection;
        }
        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE Entyti SET Status=@Status, Executer, Comment, Fault WHERE id=@id";
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

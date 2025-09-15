using System.Windows;


namespace Demo
{
    public partial class MenuManager : Window
    {
        public MenuManager()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Work().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Statistics().Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new Reports().Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

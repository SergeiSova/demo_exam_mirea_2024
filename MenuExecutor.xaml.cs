using System.Windows;

namespace Demo
{
    public partial class MenuExecutor : Window
    {
        public MenuExecutor()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Finish().Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

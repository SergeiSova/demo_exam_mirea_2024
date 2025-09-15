using System.Windows;

namespace Demo
{
    public partial class MenuUser : Window
    {
        public MenuUser()
        {
            InitializeComponent(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Create().Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new Feedback().Show(); 
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

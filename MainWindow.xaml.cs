using System.Windows;

namespace Demo
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenLoginAfterDelay();
        }

        private async void OpenLoginAfterDelay()
        {
            await Task.Delay(5000);
            new Login().Show();
            Close();
        }
    }
}
using QRCoder;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace Demo
{
    public partial class Feedback : Window
    {
        public Feedback()
        {
            InitializeComponent();
            QrCode("https://google.com");
        }

        void QrCode(string url)
        {
            var QrGenerator = new QRCodeGenerator();
            var QrGeneratorData = QrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var QrCode = new PngByteQRCode(QrGeneratorData);
            var QrCodeImage = QrCode.GetGraphic(20);
            var ms = new MemoryStream(QrCodeImage);
            var Image = new BitmapImage();

            Image.BeginInit();
            Image.CacheOption = BitmapCacheOption.OnLoad;
            Image.StreamSource = ms;
            Image.EndInit();

            QR.Source = Image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MenuUser().Show();
            Close();
        }
    }
}

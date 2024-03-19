using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerShopApplication.View.Card
{
    /// <summary>
    /// Логика взаимодействия для QrCard.xaml
    /// </summary>
    public partial class QrCard : Window
    {
        public ImageSource QrCodeImage { get; set; }
        public byte[] Image { get; set; }

        public QrCard(string qrCodeText)
        {
            CreateQrCode(qrCodeText);
            InitializeComponent();
            DataContext = this;
        }

        private void CreateQrCode(string qrCodeText)
        {
            var data = QRCoder.QRCodeGenerator.GenerateQrCode(qrCodeText, QRCoder.QRCodeGenerator.ECCLevel.H);
            var png = new QRCoder.PngByteQRCode(data).GetGraphic(10);
            Image = png;
        }
    }
}

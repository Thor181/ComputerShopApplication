using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerShopApplication.View.Pages.AddPages
{
    public partial class AddManufacturerPage : Page
    {
        private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public AddManufacturerPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextbox.Text) == false)
            {
                string name = NameTextbox.Text;
                Task.Run(() =>
                {
                    ComputerShopApplication.Model.ComputerDbContext db = new();
                    db.Manufacturers.Add(new Model.Manufacturer
                    {
                        Name = name
                    });
                    db.SaveChanges();
                });
                _mainWindow.FrameCanvas.Navigate(new Uri("/View/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}

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
using ComputerShopApplication.Model;
using MySqlConnector;

namespace ComputerShopApplication.View.Pages.AddPages
{
    public partial class AddAccessoryPage : Page
    {
        private ComputerDbContext _db = new ComputerDbContext();
        private byte[] _image = null!;
        public AddAccessoryPage()
        {
            InitializeComponent();

            ManufacturerCombobox.ItemsSource = _db.Manufacturers.ToList();
            CategoryCombobox.ItemsSource= _db.Categories.ToList();
        }

        private void GridPhoto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PlaceForPhoto.Source = ImageWorker.LoadImage(out _image);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (Service.CheckerNullOrWhiteSpace.Check(NameTextbox.Text,PriceTextbox.Text) == true && ManufacturerCombobox.SelectedIndex != -1 && CategoryCombobox.SelectedIndex != -1)
            {
                _db.Accessories.Add(new Accessory
                {
                    Name = NameTextbox.Text,
                    IdCategory = ((Category)CategoryCombobox.SelectedItem).IdCategory,
                    IdManufacturer = ((Manufacturer)ManufacturerCombobox.SelectedItem).IdManufacturer,
                    IsGaming = (bool)IsGamingCheckbox.IsChecked!,
                    Price = decimal.Parse(PriceTextbox.Text),
                    Image = _image
                });
                _db.SaveChanges();
                ((MainWindow)Application.Current.MainWindow).FrameCanvas.Navigate(new Uri("/View/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}

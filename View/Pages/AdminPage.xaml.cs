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
using ComputerShopApplication.Model.InnerModel;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApplication.View.Pages
{
    public partial class AdminPage : Page
    {
        private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public AdminPage()
        {
            InitializeComponent();
            WorkTableCombobox.ItemsSource = new string[] { "Товары", "Производители", "Категории", "Пользователи" };
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            switch (WorkTableCombobox.SelectedItem)
            {
                case "Товары":
                    _mainWindow.FrameCanvas.Navigate(new Uri("/View/Pages/AddPages/AddAccessoryPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "Производители":
                    _mainWindow.FrameCanvas.Navigate(new Uri("/View/Pages/AddPages/AddManufacturerPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "Категории":
                    _mainWindow.FrameCanvas.Navigate(new Uri("/View/Pages/AddPages/AddCategoryPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "Пользователи":
                    _mainWindow.FrameCanvas.Navigate(new Uri("/View/Pages/AddPages/AddUserPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                default:
                    break;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IdForDeleteTextbox.Text == "")
            {
                return;
            }
            ComputerDbContext db = new();
            int id = int.Parse(IdForDeleteTextbox.Text);
            switch (WorkTableCombobox.SelectedItem)
            {
                case "Товары":
                    var accessory = db.Accessories.Where(x => x.Id == id).First();
                    db.Accessories.Remove(accessory);
                    break;
                case "Производители":
                    var manufacturer = db.Manufacturers.Where(x => x.Id == id).First();
                    db.Manufacturers.Remove(manufacturer);
                    break;
                case "Категории":
                    var category = db.Categories.Where(x => x.Id == id).First();
                    db.Categories.Remove(category);
                    break;
                case "Пользователи":
                    var user = db.Users.Where(x => x.Id == id).First();
                    db.Users.Remove(user);
                    break;
                default:
                    break;
            }
            db.SaveChanges();
            LoadItemsToDataGrid();
        }

        private void WorkTableCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadItemsToDataGrid();            
        }
        private void LoadItemsToDataGrid()
        {
            ComputerDbContext db = new();
            MainDataGridView.Columns.Clear();
            int i = 0;
            switch (WorkTableCombobox.SelectedItem)
            {
                case "Товары":
                    EditStackPanel.Visibility = Visibility.Visible;
                    MainDataGridView.ItemsSource = db.Accessories.Include(x => x.IdManufacturerNavigation)
                                                                 .Include(x => x.IdCategoryNavigation)
                                                                 .Select(x => new 
                                                                 {
                                                                     IdAccessory = x.Id,
                                                                     Name = x.Name,
                                                                     Manufacturer = x.IdManufacturerNavigation.Name,
                                                                     IdManufacturer = x.ManufacturerId,
                                                                     Category = x.IdCategoryNavigation.Name,
                                                                     IdCategory = x.CategoryId,
                                                                     IsGaming = x.IsGaming,
                                                                     Price = x.Price,
                                                                     Image = x.Image
                                                                 }).ToList();
                    
                    MainDataGridView.Columns.Remove(MainDataGridView.Columns.Where(x => (string)x.Header == "Image").FirstOrDefault());
                    MainDataGridView.Columns[i].Header = "Код";
                    MainDataGridView.Columns[++i].Header = "Наименование";
                    MainDataGridView.Columns[++i].Header = "Производитель";
                    MainDataGridView.Columns[++i].Header = "Код производителя";
                    MainDataGridView.Columns[++i].Header = "Категория";
                    MainDataGridView.Columns[++i].Header = "Код категории";
                    MainDataGridView.Columns[++i].Header = "Игровой";
                    MainDataGridView.Columns[++i].Header = "Стоимость";
                    DataGridTemplateColumn columnImage = new DataGridTemplateColumn();
                    columnImage.Header = "Изображение";
                    columnImage.MaxWidth = 150;
                    FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(Image));
                    
                    Binding b1 = new Binding("Image");
                    frameworkElementFactory.SetValue(Image.SourceProperty, b1);
                    DataTemplate cellTemplate = new();
                    cellTemplate.VisualTree = frameworkElementFactory;
                    
                    columnImage.CellTemplate = cellTemplate;
                    MainDataGridView.Columns.Add(columnImage);
                    break;
                case "Производители":
                    EditStackPanel.Visibility = Visibility.Hidden;
                    MainDataGridView.ItemsSource = db.Manufacturers.Select(x => new
                    {
                        x.Id,
                        x.Name
                    }).ToList();
                    MainDataGridView.Columns[i].Header = "Код";
                    MainDataGridView.Columns[++i].Header = "Наименование";
                    break;
                case "Категории":
                    EditStackPanel.Visibility = Visibility.Hidden;
                    MainDataGridView.ItemsSource = db.Categories.ToList();
                    MainDataGridView.Columns[i].Header = "Код";
                    MainDataGridView.Columns[++i].Header = "Наименование";
                    break;
                case "Пользователи":
                    EditStackPanel.Visibility = Visibility.Hidden;
                    MainDataGridView.ItemsSource = db.Users.ToList();
                    MainDataGridView.Columns[i].Header = "ID";
                    MainDataGridView.Columns[++i].Header = "Логин";
                    MainDataGridView.Columns[++i].Header = "Пароль";
                    break;
                default:
                    break;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.CheckerNullOrWhiteSpace.Check(IdForEditPriceTextbox.Text, NewPriceTextbox.Text) == true)
            {
                ComputerDbContext db = new();
                Accessory? accessory = db.Accessories.FirstOrDefault(x => x.Id == int.Parse(IdForEditPriceTextbox.Text));
                if (accessory != null)
                {
                    decimal.TryParse(NewPriceTextbox.Text, out decimal price);
                    accessory.Price = price;
                    db.Update(accessory);
                    db.SaveChanges();
                    LoadItemsToDataGrid();
                }
            }
        }
    }
}

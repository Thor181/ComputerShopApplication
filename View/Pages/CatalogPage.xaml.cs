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
using ComputerShopApplication.View.Card;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApplication.View.Pages
{
    public partial class CatalogPage : Page
    {
        private readonly ComputerDbContext _db;
        public CatalogPage()
        {
            InitializeComponent();
            _db = new ComputerDbContext();
            ConfigFilterMenu();
            LoadItemsToWrapPanel();
        }

        private void ConfigFilterMenu()
        {
            FilterCategoryCombobox.ItemsSource = _db.Categories.ToList();
            FilterManufacturerCombobox.ItemsSource = _db.Manufacturers.ToList();
        }

        private void LoadItemsToWrapPanel()
        {
            PageMainWrapPanel.Children.Clear();
            List<InnerAccessories> accessories = _db.Accessories.Include(x => x.IdManufacturerNavigation)
                                                                .Include(x => x.IdCategoryNavigation)
                                                                .Select(x => new InnerAccessories
                                                                {
                                                                    IdAccessory = x.IdAccessory,
                                                                    Name = x.Name,
                                                                    IdManufacturer = x.IdManufacturer,
                                                                    Manufacturer = x.IdManufacturerNavigation.Name,
                                                                    IdCategory = x.IdCategory,
                                                                    Category = x.IdCategoryNavigation.Name,
                                                                    IsGaming = x.IsGaming,
                                                                    Price = x.Price,
                                                                    Image = x.Image
                                                                }).ToList();
            foreach (var item in accessories)
            {
                PageMainWrapPanel.Children.Add(new AccessoryCard(item));
            }
        }
        private void LoadItemsToWrapPanel(List<InnerAccessories> innerAccessories)
        {
            PageMainWrapPanel.Children.Clear();
            foreach (var item in innerAccessories)
            {
                PageMainWrapPanel.Children.Add(new AccessoryCard(item));
            }
        }


        private void FilterOkButton_Click(object sender, RoutedEventArgs e)
        {
            int filterCategory = -1;
            if (FilterCategoryCombobox.SelectedIndex != -1)
            {
                filterCategory = ((Category)FilterCategoryCombobox.SelectedItem).IdCategory;
            }

            int filterManufacturer = -1;
            if (FilterManufacturerCombobox.SelectedIndex != -1)
            {
                filterManufacturer = ((Manufacturer)FilterManufacturerCombobox.SelectedItem).IdManufacturer;
            }

            string filterName = null!;
            if (string.IsNullOrWhiteSpace(FilterNameTextbox.Text) == false)
            {
                filterName = FilterNameTextbox.Text.ToLower();
            }

            decimal filterPriceFrom = -1m;
            if (string.IsNullOrWhiteSpace(FilterPriceFromTextbox.Text) == false)
            {
                filterPriceFrom = decimal.Parse(FilterPriceFromTextbox.Text);
            }

            decimal filterPriceTo = -1m;
            if (string.IsNullOrWhiteSpace(FilterPriceToTextbox.Text) == false)
            {
                filterPriceTo = decimal.Parse(FilterPriceToTextbox.Text);
            }

            List <Accessory> accessories = _db.Accessories.ToList();
            if (filterCategory != -1)
            {
                accessories = _db.Accessories.Where(x => x.IdCategory == filterCategory).ToList();
            }
            if (filterManufacturer != -1)
            {
                accessories = accessories.Where(x => x.IdManufacturer == filterManufacturer).ToList();
            }
            if (filterName != null)
            {
                accessories = accessories.Where(x => x.Name.Contains(filterName)).ToList();
            }
            if (filterPriceFrom != -1)
            {
                accessories = accessories.Where(x => x.Price >= filterPriceFrom).ToList();
            }
            if (filterPriceTo != -1)
            {
                accessories = accessories.Where(x => x.Price <= filterPriceTo).ToList();
            }



            List<InnerAccessories> innerAccessories = accessories.Join(_db.Categories, x => x.IdCategory, y => y.IdCategory, (x, y) => new
            {
                Name = x.Name,
                IdAccessory = x.IdAccessory,
                IdCategory = x.IdCategory,
                Catergory = y.Name,
                IdManufacturer = x.IdManufacturer,
                IsGaming = x.IsGaming,
                Price = x.Price,
                Image = x.Image
            }).Join(_db.Manufacturers, x => x.IdManufacturer, y => y.IdManufacturer, (x,y) => new InnerAccessories
            {
                Name = x.Name,
                IdAccessory = x.IdAccessory,
                IdCategory = x.IdCategory,
                Category = x.Catergory,
                IdManufacturer = x.IdManufacturer,
                Manufacturer = y.Name,
                IsGaming = x.IsGaming,
                Price = x.Price,
                Image = x.Image
            }).ToList();

            LoadItemsToWrapPanel(innerAccessories);
        }
    }
}

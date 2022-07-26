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
using ComputerShopApplication.Service;
using ComputerShopApplication.Model.InnerModel;
using System.IO;
using System.Diagnostics;

namespace ComputerShopApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadItemsToDataGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.RemoveItem(int.Parse(IdForDeleteTextbox.Text));
            LoadItemsToDataGrid();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.Clear();
            LoadItemsToDataGrid();
        }

        private void LoadItemsToDataGrid()
        {
            MainDataGridView.ItemsSource = new List<InnerAccessories>();
            MainDataGridView.ItemsSource = CartCollection.GetItems();
            DisplayPrice();
        }

        private void DisplayPrice()
        {
            PriceTextblock.Text = string.Format("{0:#.##}", CartCollection.GetSum());
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> receiptText = new List<string>();
            receiptText.Add("COMPUTER ACCESSORIES SHOP\n\n");
            receiptText.Add($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\n");
            foreach (var item in CartCollection.GetItems())
            {
                receiptText.Add($"Товар: {item.Name} = {item.Price:#.##} руб.");
            }
            receiptText.Add($"\nИТОГО: {CartCollection.GetSum():#.##} руб.");

            if (Directory.Exists($"{Environment.CurrentDirectory}\\Receipts") == false)
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\Receipts");
            }
            string filePath = $"{Environment.CurrentDirectory}\\Receipts\\Receipt{DateTime.Now:dd.MM.yyyy HH.mm.ss}.doc";
            using (FileStream fs = File.Create(filePath))
            {
                fs.Close();
            }
            File.WriteAllLines(filePath, receiptText);

            new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

    }
}

using ComputerShopApplication.Model.InnerModel;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShopApplication.View.Card
{
    public partial class AccessoryCard : UserControl
    {
        private InnerAccessories InnerAccessories { get; set; }

        public AccessoryCard(InnerAccessories innerAccessories)
        {
            InitializeComponent();
            CardMainGrid.DataContext = innerAccessories;
            InnerAccessories = innerAccessories;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Service.CartCollection.AddItem(((InnerAccessories)CardMainGrid.DataContext));
        }

        private void QrCodeButton_Click(object sender, RoutedEventArgs e)
        {
            var name = InnerAccessories.Name;
            var price = InnerAccessories.Price;

            new QrCard($"Наименование: {name}\nСтоимость: {price}").ShowDialog();
        }
    }
}

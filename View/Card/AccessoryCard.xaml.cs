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
using ComputerShopApplication.Model.InnerModel;

namespace ComputerShopApplication.View.Card
{
    public partial class AccessoryCard : UserControl
    {
        public AccessoryCard(InnerAccessories innerAccessories)
        {
            InitializeComponent();
            CardMainGrid.DataContext = innerAccessories;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Service.CartCollection.AddItem(((InnerAccessories)CardMainGrid.DataContext));
        }
    }
}

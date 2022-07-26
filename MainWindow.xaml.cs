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
using ComputerShopApplication.View;
using ComputerShopApplication.Model;

namespace ComputerShopApplication
{ 
    public partial class MainWindow : Window
    {
        public readonly User currentUser;

        public MainWindow()
        {
            new AuthWindow().ShowDialog();
            InitializeComponent();
            FrameCanvas.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            this.DataContext = currentUser = Service.CurrentUser.Get();
            if (currentUser.Login == "admin")
            {
                AdminButton.Visibility = Visibility.Visible;
            }
            UserAccountPhoto.Text = currentUser.Login.First().ToString();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FullViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            FrameCanvas.Navigate(new Uri("/View/Pages/CatalogPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            FrameCanvas.Navigate(new Uri("/View/Pages/CartPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            FrameCanvas.Navigate(new Uri("/View/Pages/AccountPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            FrameCanvas.Navigate(new Uri("/View/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

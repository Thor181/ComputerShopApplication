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
using Microsoft.EntityFrameworkCore;
using ComputerShopApplication.Service;

namespace ComputerShopApplication
{ 
    public partial class MainWindow : Window
    {
        public readonly User currentUser;

        public MainWindow()
        {
            InitializeDatabase();

            new AuthWindow().ShowDialog();
            InitializeComponent();
            FrameCanvas.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            this.DataContext = currentUser = Service.CurrentUser.Get();

            if (currentUser.Login == "admin")
                AdminButton.Visibility = Visibility.Visible;

            UserAccountPhoto.Text = currentUser.Login.First().ToString();
        }

        private void InitializeDatabase()
        {
            if (!System.IO.File.Exists(Config.ConfigFileName))
            {
                System.IO.File.WriteAllText(Config.ConfigFileName, "Server=localhost;Database=ComputerShopDb;Trusted_Connection=True;Encrypt=false");

                MessageBox.Show($"Заполните файл {Config.ConfigFileName} в следующем формате\n\nServer=localhost;Database=ComputerShopDb;Trusted_Connection=True;Encrypt=false");
                Environment.Exit(0);
            }

            using var db = new ComputerDbContext();

            if (!db.Database.CanConnect())
                db.Database.Migrate();

            var adminExists = db.Users.Where(x => x.Login == "admin").Any();

            if (!adminExists)
            {
                db.Users.Add(new User() { Login = "admin", Password = "123" });
                db.SaveChanges();
            }

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FullViewButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal 
                ? WindowState.Maximized 
                : WindowState.Normal;
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

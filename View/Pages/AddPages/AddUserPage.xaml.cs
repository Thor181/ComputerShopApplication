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
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.CheckerNullOrWhiteSpace.Check(NameTextbox.Text, PasswordTextbox.Text) == true)
            {
                Model.ComputerDbContext db = new Model.ComputerDbContext();
                var user = db.Users.Where(x => x.Login == NameTextbox.Text).FirstOrDefault();
                if (user == null)
                {
                    db.Add(new Model.User
                    {
                        Login = NameTextbox.Text,
                        Password = PasswordTextbox.Text
                    });
                    db.SaveChanges();
                    ((MainWindow)Application.Current.MainWindow).FrameCanvas.Navigate(new Uri("/View/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ErrorText.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

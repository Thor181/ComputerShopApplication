using ComputerShopApplication.Model;
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

namespace ComputerShopApplication.View.Auth
{
    public partial class AuthView : UserControl
    {
        private readonly ComputerDbContext _db;
        private readonly AuthWindow _authWindow;

        public AuthView(AuthWindow authWindow)
        {
            InitializeComponent();
            _db = new ComputerDbContext();
            _authWindow = authWindow;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.CheckerNullOrWhiteSpace.Check(LoginTextbox.Text, PasswordTextbox.Password) == true)
            {
                User? user = _db.Users.FirstOrDefault(x => x.Login == LoginTextbox.Text && x.Password == PasswordTextbox.Password);

                if (user != null)
                {
                    Service.CurrentUser.Set(user);
                    _authWindow.Close();
                }
            }
            else
            {
                LoginTextbox.Text = default(string);
                PasswordTextbox.Password = default(string);
            }
        }

        private void RegisterHyperlink_Click(object sender, RoutedEventArgs e)
        {
            _authWindow.MainCanvas.Children.Clear();
            _authWindow.MainCanvas.Children.Add(new RegistrationView(_authWindow));
        }
    }
}

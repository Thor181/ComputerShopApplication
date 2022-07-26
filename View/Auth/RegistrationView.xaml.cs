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

namespace ComputerShopApplication.View
{
    public partial class RegistrationView : UserControl
    {
        private readonly AuthWindow _authWindow;
        public RegistrationView(AuthWindow authWindow)
        {
            InitializeComponent();
            _authWindow = authWindow;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.CheckerNullOrWhiteSpace.Check(LoginTextbox.Text, PasswordTextbox.Password, ConfirmPasswordTextbox.Password) == true)
            {
                Model.ComputerDbContext db = new();
                if (PasswordTextbox.Password.Equals(ConfirmPasswordTextbox.Password) == false)
                {
                    ErrorText.Content = "Пароли не совпадают";
                    ErrorText.Visibility = Visibility.Visible;
                    return;
                }
                if (db.Users.Where(x => x.Login == LoginTextbox.Text).FirstOrDefault() != null)
                {
                    ErrorText.Content = "Данный логин занят";
                    ErrorText.Visibility = Visibility.Visible;
                    return;
                }
                (string login, string password) loginPassword = (LoginTextbox.Text, PasswordTextbox.Password);
                Task.Run(() =>
                {
                    db.Users.Add(new Model.User
                    {
                        Login = loginPassword.login,
                        Password = loginPassword.password
                    });
                    db.SaveChanges();
                });
                _authWindow.MainCanvas.Children.Clear();
                _authWindow.MainCanvas.Children.Add(new Auth.AuthView(_authWindow));
            }
        }

        private void IsRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _authWindow.MainCanvas.Children.Clear();
            _authWindow.MainCanvas.Children.Add(new Auth.AuthView(_authWindow));
        }


    }
}

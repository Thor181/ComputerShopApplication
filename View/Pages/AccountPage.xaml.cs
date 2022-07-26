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

namespace ComputerShopApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        private void SaveNewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.CheckerNullOrWhiteSpace.Check(OldPasswordTextbox.Text, NewPasswordTextbox.Text) == true)
            {
                if (NewPasswordTextbox.Text.Equals(OldPasswordTextbox.Text) == false)
                { 
                    User user = Service.CurrentUser.Get();
                    if (user.Password == OldPasswordTextbox.Text)
                    {
                        user.Password = NewPasswordTextbox.Text;
                        using (ComputerDbContext db = new())
                        {
                            db.Users.Update(user);
                            db.SaveChanges();
                        }
                        
                        OldPasswordTextbox.Text = NewPasswordTextbox.Text = default;
                    }    
                }
                else
                {
                    ErrorText.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

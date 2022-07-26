using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopApplication.Model;

namespace ComputerShopApplication.Service
{
    internal class CurrentUser
    {
        private static User _currentUser = null!;

        public static void Set(User user)
        {
            _currentUser = user;
        }
        public static User Get()
        {
            return _currentUser;
        }

    }
}

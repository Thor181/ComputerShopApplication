using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopApplication.Model.InnerModel;

namespace ComputerShopApplication.Service
{
    internal class CartCollection
    {
        private static List<InnerAccessories> CartItems = new List<InnerAccessories>();

        public static List<InnerAccessories> GetItems()
        {
            return CartItems;
        }

        public static void AddItem(InnerAccessories item)
        {
            CartItems.Add(item);
        }

        public static void Clear()
        {
            CartItems.Clear();
        }

        public static decimal GetSum()
        {
            return CartItems.Select(x => x.Price).Sum();
        }

        public static void RemoveItem(int id)
        {
            CartItems.Remove(CartItems.Where(x => x.IdAccessory == id).First());
        }
    }
}

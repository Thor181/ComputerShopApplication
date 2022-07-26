using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopApplication.Model.InnerModel
{
    public class InnerAccessories
    {
        public int IdAccessory { get; set; }

        public string Name { get; set; } = null!;

        public string Manufacturer { get; set; } = null!;
        public int IdManufacturer { get; set; }

        public string Category { get; set; } = null!;
        public int IdCategory { get; set; }

        public string IsGamingString { get; set; }
        public bool IsGaming { get; set; }

        public decimal Price { get; set; }

        public byte[]? Image { get; set; }

        public InnerAccessories()
        {
            IsGamingString = IsGaming == true ? "Нет"  : "Да";
        }
    }
}

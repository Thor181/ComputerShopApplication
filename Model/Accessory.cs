using System;
using System.Collections.Generic;

namespace ComputerShopApplication.Model
{
    public partial class Accessory
    {
        public int IdAccessory { get; set; }
        public string Name { get; set; } = null!;
        public int IdManufacturer { get; set; }
        public int IdCategory { get; set; }
        public bool IsGaming { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;
    }
}

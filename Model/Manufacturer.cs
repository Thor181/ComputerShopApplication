using System;
using System.Collections.Generic;

namespace ComputerShopApplication.Model
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Accessories = new HashSet<Accessory>();
        }

        public int IdManufacturer { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Accessory> Accessories { get; set; }
    }
}

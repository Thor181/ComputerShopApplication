using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopApplication.Service
{
    public class Config
    {
        public const string ConfigFileName = "ConnectionString.txt";

        public static string ConnectionString => System.IO.File.ReadAllText(ConfigFileName);
    }
}

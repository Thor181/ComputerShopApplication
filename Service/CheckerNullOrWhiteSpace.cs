using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopApplication.Service
{
    internal static class CheckerNullOrWhiteSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strings">Строки для проверки</param>
        /// <returns>true - если все элементы последовательности не null и не являются пробелами</returns>
        internal static bool Check(params string[] strings)
        {
            foreach (var item in strings)
            {
                if (string.IsNullOrWhiteSpace(item))
                    return false;
            }

            return true;
        }
    }
}

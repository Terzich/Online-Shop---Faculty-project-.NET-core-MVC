using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class ShowProductsVM
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }


    }
}

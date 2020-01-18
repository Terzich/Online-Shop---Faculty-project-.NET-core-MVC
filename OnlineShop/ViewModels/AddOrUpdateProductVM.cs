using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopPodaci.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class AddOrUpdateProductVM
    {
        public int ProductID { get; set; }
        public string ProductNumber { get; set; }
        public int SubCategoryID{ get; set; }
        public List<SelectListItem> Subcategories { get; set; }
        public int ManufacturerID { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
        public string ProductName { get; set; }
        public string ImageURL{ get; set; }
        public string Description { get; set; }
        public double UnitPrice{ get; set; }
    }
}

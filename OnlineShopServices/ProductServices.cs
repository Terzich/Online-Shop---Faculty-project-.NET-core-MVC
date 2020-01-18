using Microsoft.EntityFrameworkCore;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShopServices
{
    public class ProductServices:IProduct
    {
        private OnlineShopContext baza;
        public ProductServices(OnlineShopContext b)
        {
            baza = b;
        }

        public void AddProduct(Product p)
        {
            baza.Add(p);
            baza.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return baza.product.Include(p => p.SubCategory).Include(p => p.Manufacturer).ToList();
        }
        public Product GetProductByID(int id)
        {
            return GetAllProducts().Where(p => p.ProductID == id).FirstOrDefault();
        }

        public void RemoveProduct(int id)
        {
            baza.Remove(GetProductByID(id));
            baza.SaveChanges();
        }

    }
}

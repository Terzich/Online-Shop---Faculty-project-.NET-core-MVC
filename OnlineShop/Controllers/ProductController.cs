using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private IProduct product;
        private OnlineShopContext _database;

        public ProductController(IProduct p,OnlineShopContext b)
        {
            product = p;
            _database = b;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show()
        {
            var proizvodi = product.GetAllProducts();
            var productForView = proizvodi.Select(p => new ShowProductForManage
            {
                ProductID = p.ProductID,
                ProductNumber = p.ProductNumber,
                SubCategoryName = p.SubCategory.SubCategoryName,
                ManufacturerName = p.Manufacturer.ManufacturerName,
                ProductName = p.ProductName,
                Description = p.Description,
                UnitPrice = p.UnitPrice
            });
            var data = new SohwProductForManageLIST { ListOfProducts = productForView };

            return View(data);
        }

        public IActionResult Delete(int ID)
        {
            product.RemoveProduct(ID);
            return Redirect("/Product/Show");
        }

        public IActionResult AddProduct(int ProductID)
        {
            Product temp;
            if (ProductID != 0)
                temp= _database.product.Find(ProductID);
            else
                temp= new Product();
            var data = new AddOrUpdateProductVM {
                ProductID=temp.ProductID,
                ProductNumber=temp.ProductNumber,
                SubCategoryID=temp.SubCategoryID,
                Subcategories=_database.subcategory.Select(s=> new SelectListItem { Value = s.SubCategoryID.ToString(), Text = s.SubCategoryName }).ToList(),
                ManufacturerID = temp.ManufacturerID,
                Manufacturers = _database.manufacturer.Select(s => new SelectListItem { Value = s.ManufacturerID.ToString(), Text = s.ManufacturerName }).ToList(),
                ProductName=temp.ProductName,
                ImageURL=temp.ImageUrl,
                Description=temp.Description,
                UnitPrice=temp.UnitPrice
            };
            return View(data);
        }

        public IActionResult SaveProduct(AddOrUpdateProductVM model)
        {
            Product neki;

            if ( model.ProductID== 0)
            {
                neki = new Product();
                _database.product.Add(neki);
            }
            else
                neki = _database.product.Find(model.ProductID);
            

            neki.ProductNumber = model.ProductNumber;
            neki.SubCategoryID = model.SubCategoryID;
            neki.ManufacturerID = model.ManufacturerID;
            neki.ProductName = model.ProductName;
            neki.ImageUrl = model.ImageURL;
            neki.Description = model.Description;
            neki.UnitPrice = model.UnitPrice;

            _database.SaveChanges();
            return Redirect("/Product/Show");

        }

        public IActionResult AddManufacturer()
        {
            return View("AddManufacturer");
        }

        public IActionResult SaveManufacturer(string manufacturerName, string logoURL)
        {
            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerName = manufacturerName,
                LogoUrl = logoURL
            };

            _database.manufacturer.Add(manufacturer);
            _database.SaveChanges();
            return View("ManufactureAddMessage");

        }
        public IActionResult Show2()       
        {
           List<ShowCategoriesVM >data = _database.category.Select(c => new ShowCategoriesVM { CategoryID = c.CategoryID, CategoryName = c.CategoryName }).ToList();
            
            return View(data);
        }

        public IActionResult ShowSubcategories(int ID)          // ID kategorije
        {
            List<ShowSubCategoriesVM> lista = _database.subcategory.Where(s => s.CategoryID == ID).
                Select(s => new ShowSubCategoriesVM
            {
                CategoryName=_database.category.Where(c=>c.CategoryID==ID).SingleOrDefault().CategoryName,
                SubCategoryID=s.SubCategoryID,
                SubCategoryName=s.SubCategoryName
            }).ToList();

            return View(lista);
        }

        public IActionResult ShowProducts(int ID)       // ID podkategorije 
        {

            List<ShowProductsVM> products = _database.product.Where(s => s.SubCategoryID == ID).
                Select(p => new ShowProductsVM
            {
                ProductID=p.ProductID,
                ProductName=p.ProductName,
                ManufacturerName=p.Manufacturer.ManufacturerName,
                UnitPrice=p.UnitPrice,
                UnitsInStock=p.UnitsInStock

            }).ToList();

            return View(products);
        }

        public IActionResult ProductDetails(int ID)     // ID proizvoda
        {
            ProductDetailsVM model = _database.product.Where(s=>s.ProductID==ID).Select(a => new ProductDetailsVM
            {
                ProductID =a.ProductID,
                ProductName=a.ProductName,
                ProductNumber =a.ProductNumber,
                SubCategoryName=a.SubCategory.SubCategoryName,
                ManufacturerName=a.Manufacturer.ManufacturerName,
                ImageUrl=a.ImageUrl,
                Description=a.Description,
                UnitPrice=a.UnitPrice,
                UnitsInStock=a.UnitsInStock         // uvijek je 0 tamo
            }).SingleOrDefault();

            return View(model);
        }
    }
}
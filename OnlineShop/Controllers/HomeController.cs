using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // LOGIN 

        public IActionResult LogInForm()
        {
            return View();
        }
        public IActionResult CheckLogIn(string UserMail, string UserPass)
        {
            OnlineShopContext baza = new OnlineShopContext();
            var user = baza.user.Where(u => u.Email == UserMail && u.Password == UserPass).FirstOrDefault();
            if (user == null)
                return Redirect("LogInForm");
            else
                return Redirect("Index");
        }
        public IActionResult Registration()
        {
            OnlineShopContext _database = new OnlineShopContext();
            ViewData["gradovi"] = _database.city.ToList();
            ViewData["spol"] = _database.gender.ToList();

            return View();
        }
        public IActionResult SaveRegistration(string name,string surname,DateTime birthdate,int cityID,string adresa,string email,string password,int genderID)
        {
            OnlineShopContext _database = new OnlineShopContext();

            User user = new User
            {
                Name = name,
                Surname = surname,
                BirthDate = birthdate,
                CityID = cityID,
                Adress = adresa,
                Email = email,
                Password = password,
                GenderID = genderID,
            };


            _database.user.Add(user);
            _database.SaveChanges();
            _database.Dispose();
            return Redirect("RegistrationSuccessful");           
        }
        public IActionResult RegistrationSuccessful()
        {
            return View();
        }
    }
}

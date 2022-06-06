using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuissnesLayer;
using BuissnesLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer;
using PresentationLayer.Models;
using Task4_web.Models;

namespace Task4_web.Controllers
{
    public class HomeController : Controller
    {
        
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;
        public HomeController(DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

        public IActionResult Index()
        {
           
            List<UserViewModel> _dirs = _servicesmanager.Users.GetUseresList();

            return View(_dirs);
        }

        [HttpGet]
        public IActionResult UserLogin(string userEmail, string userPass) // !!!
        {
            return UserLogin(userEmail, userPass);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

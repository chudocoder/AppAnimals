using System;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppAnimals.Models;

namespace AppAnimals.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Animals()
        {
                return View();
        }

        public IActionResult CreateAnimal()
        {
            return View();
        }

        public IActionResult EditAnimal(int? id)
        {
            return View(id);
        }

        public IActionResult DetailsAnimal(int? id)
        {
            return View(id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

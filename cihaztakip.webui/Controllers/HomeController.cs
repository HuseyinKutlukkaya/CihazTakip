using cihaztakip.business.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace cihaztakip.webui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDeviceService _deviceService;

        public HomeController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
         
        }

        public IActionResult Index()
        {
        
     
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
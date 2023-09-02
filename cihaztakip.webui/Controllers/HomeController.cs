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
        private readonly ApplicationDbContext applicationDbContext;

        public HomeController(IDeviceService deviceService, ApplicationDbContext applicationDbContext)
        {
            _deviceService = deviceService;
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            List<Device> list = _deviceService.GetAllWithUserData();
     
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
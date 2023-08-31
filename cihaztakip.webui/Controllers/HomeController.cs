using cihaztakip.data.Concrete.EfCore;
using cihaztakip.webui.Models;
using cihaztakip.webui.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cihaztakip.webui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var list = applicationDbContext.Devices.Select(m => new DeviceViewComponent
            {
                Name = m.Name,
                DeviceId = m.DeviceId,
              
            }).ToList();

            return View(list);
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
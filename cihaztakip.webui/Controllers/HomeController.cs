using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        //public IActionResult Index()
        //{
        //    var list = applicationDbContext.Devices.Select(m => new DeviceViewComponent
        //    {
        //        Name = m.Name,
        //        DeviceId = m.DeviceId,
              
        //    }).ToList();

        //    return View(list);
        //}
        public async Task<IActionResult> Index()
        {
            var list = new List<DeviceModel>();

            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync("http://localhost:5057/api/device"))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        list = JsonConvert.DeserializeObject<List<DeviceViewComponent>>(apiResponse);
            //    }
            //}

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
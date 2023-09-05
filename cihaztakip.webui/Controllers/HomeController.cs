using cihaztakip.business.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.webui.Models;
using cihaztakip.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace cihaztakip.webui.Controllers
{
    public class HomeController : Controller
    {
       

        public HomeController(IDeviceService deviceService)
        {
            
         
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<NavDescriptionModel> list = new List<NavDescriptionModel>()
                {
                    new NavDescriptionModel
                    {
                        Title = "Ana Sayfa",
                        Roles = "Erişilebilir Roller: Tüm roller",
                        Description="Bütün fonksiyonların açıklamaları listelenir.",
                        Route = "/Home/Index"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Kullanıcı Listesi",
                        Roles = "Erişilebilir Roller: Admin",
                        Description="Bütün kullanıcılar listelenir.",
                        Route = "/User/UserList"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Yeni Kullanıcı Ekleme",
                        Roles = "Erişilebilir Roller: Admin",
                        Description="Yeni kullanıcı eklenebilir.",
                        Route = "/User/NewUser"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Cihaz Listesi",
                        Roles = "Erişilebilir Roller: Admin, Authorized",
                        Description="Bütün cihazlar listelenir.",
                        Route = "/Device/DeviceList"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Cihaz Ekleme",
                        Roles = "Erişilebilir Roller: Admin",
                        Description="Yeni cihaz eklenebilir.",
                        Route = "/Device/CreateDevice"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Cihaz Düzenleme",
                        Roles = "Erişilebilir Roller: Admin",
                        Description="Cihazlar listesinden cihazı düzenle seçeneği seçilerek, seçilen cihazın bilgileri güncellenebilir.",
                        Route = "/Device/deviceList"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Cihaza Kullanıcı Atama",
                        Roles = "Erişilebilir Roller: Admin, Authorized",
                        Description="Cihazlar listesinden cihazı düzenle seçeneği seçilerek, seçilen cihaza yeni veya farklı bir kullanıcı atanabilir.",
                        Route = "/Device/deviceList"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Cihazdan Kullanıcı Çıkarma",
                        Roles = "Erişilebilir Roller: Admin, Authorized",
                        Description="Cihazlar listesinden cihazı düzenle seçeneği seçilerek, seçilen cihaza bağlı bir kullanıcı varsa cihazdan kaldırılabilir.",
                        Route = "/Device/deviceList"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Profilim",
                        Roles = "Erişilebilir Roller: Admin, Authorized, User",
                        Description="Profile girerek aktif kullanıcının sahip olduğu cihazları listeler.",
                        Route = "/Account/Profile"
                    },
                    new NavDescriptionModel
                    {
                        Title = "Çıkış",
                        Roles = "Erişilebilir Roller: Admin, Authorized, User",
                        Description="Giriş yapılmış olan kullanıcıdan çıkılmasını sağlar.",
                        Route = "/Account/Logout"
                    }

                };


                return View(list);
            }
            else
                return RedirectToAction("Login", "Account");
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
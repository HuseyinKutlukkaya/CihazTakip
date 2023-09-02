using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.webui.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

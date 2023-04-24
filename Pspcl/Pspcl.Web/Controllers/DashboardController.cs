using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


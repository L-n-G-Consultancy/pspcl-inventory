using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    public class AddStockController : Controller
    {
        public IActionResult AddStock()
        {
            return View();
        }
    }
}

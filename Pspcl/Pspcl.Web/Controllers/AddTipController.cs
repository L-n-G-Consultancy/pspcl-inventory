using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    public class AddTipController : Controller
    {
        public IActionResult AddTipView()
        {
            return View();
        }
    }
}

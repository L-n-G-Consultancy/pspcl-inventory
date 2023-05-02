using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult ReportView()
        {
            return View();
        }
    }
}

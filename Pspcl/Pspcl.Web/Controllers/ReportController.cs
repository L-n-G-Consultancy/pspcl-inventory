using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        public IActionResult ReportView()
        {
            return View();
        }
    }
}

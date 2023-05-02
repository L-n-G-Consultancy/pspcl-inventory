using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class IssueStockController : Controller
    {
        public IActionResult IssueStockView()
        {
            return View();
        }
    }
}

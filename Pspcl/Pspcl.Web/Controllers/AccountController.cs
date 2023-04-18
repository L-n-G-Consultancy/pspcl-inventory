using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }
    }
}

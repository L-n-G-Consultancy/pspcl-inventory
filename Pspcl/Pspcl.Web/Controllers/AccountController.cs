using Microsoft.AspNetCore.Mvc;
using System.Web;

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

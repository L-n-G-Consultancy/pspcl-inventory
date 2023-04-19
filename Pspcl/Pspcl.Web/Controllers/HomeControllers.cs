using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;
        }
        public async Task<IActionResult>  Index()
        {
            _logger.LogInformation($"Logged In Success");
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Pspcl.Web.Models;
using System.Diagnostics;
using System.Text.Json;
using RestSharp;
namespace Pspcl.Web.Controllers
  
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task< IActionResult> Index()
        {
            var _client = new RestClient(_configuration["ApiUrl"]);
            var request = new RestRequest("Weatherforecast", Method.Get);
            var response= await _client.ExecuteGetAsync(request);
            TempData.Keep("Message");
            return View();
        }
        public IActionResult Privacy()

        {
            TempData["Message"] = "Temp Data Content";
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
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
        
        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;
            
        }
        public IActionResult  Index()
        {
           
            return View();
        }
    }
}

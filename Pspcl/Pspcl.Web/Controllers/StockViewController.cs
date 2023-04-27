using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services;
using Pspcl.Web.Models;

namespace Pspcl.Web.Controllers
{
    
    public class StockViewController: Controller
    {
        
        private readonly StockMappingservice _stockMappingService;

        public StockViewController(StockMappingservice stockMappingServices)
        {
           
            _stockMappingService = stockMappingServices;

        }
        [HttpGet]
        public IActionResult AddStock ()
        {
           var models= _stockMappingService.GetAll();
            return View(models);
        }


    }
}

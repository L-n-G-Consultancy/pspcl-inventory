using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.DBConnect;
using Pspcl.Services;
using Pspcl.Web.Models;

namespace Pspcl.Web.Controllers
{
    
    public class AddStockController: Controller
    {
        private readonly StockMappingService _stockMappingService;
        public AddStockController(StockMappingService stockMappingService)
        {
            _stockMappingService = stockMappingService;
        }
        public IActionResult AddStock(Model model)
        {
            var entity = _stockMappingService.Stock(model);
             
            return View(model);
        }
    }
}

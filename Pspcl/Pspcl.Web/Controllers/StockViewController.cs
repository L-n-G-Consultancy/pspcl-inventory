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
        
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        
        [HttpGet]
        public IActionResult AddStock ()
        {
         
            return View("~/Views/StockView/AddStock.cshtml");
        }


    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services;
using Pspcl.Web.Models;
using Pspcl.Core.Domain;
using Microsoft.AspNetCore.Http;
using JasperFx.Core;
using System.Web;
using NuGet.Protocol;
using JasperFx.CodeGeneration.Frames;

namespace Pspcl.Web.Controllers
{
    
	public class StockViewController : Controller
	{
		private readonly IStockService _stockService;
		private readonly IMapper _mapper;

        public StockViewController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet]
		public IActionResult AddStock()
		{
        

			return View();
		}        
        
		[HttpPost]
		public IActionResult AddStock(StockViewModel model, IFormCollection formCollection)
        {
         
			return RedirectToAction("AddStock");
        }


    }
}

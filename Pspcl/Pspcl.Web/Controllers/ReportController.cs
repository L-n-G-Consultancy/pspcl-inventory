using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pspcl.Web.ViewModels;
using Pspcl.Core.Domain;
using Pspcl.DBConnect.Install;
using Pspcl.DBConnect;
using Pspcl.Services;
using Pspcl.Services.Interfaces;


namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockService _stockService;
        private readonly ILogger<DbInitializer> _logger;
        public ReportController(ApplicationDbContext _dbContext, IStockService stockService, ILogger<DbInitializer> logger) {
            _context = _dbContext;
            _stockService = stockService;
            _logger = logger;
        }
        

        public IActionResult StockInReport()
        {

            try
            {
                var stockInModels = _stockService.GetStockInModels();
                return View(stockInModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return View();
        }

        public IActionResult AvailableStock()
        {
            try
            {
                var availableStockModel = _stockService.GetAvailableStock();

                return View(availableStockModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return View();
        }

        public IActionResult StockOutReport()
        {
			try
			{
				var stockOutModels = _stockService.GetStockOutModels();
				return View(stockOutModels);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exception");
			}
			return View();
		}

        [HttpGet]
        public JsonResult FilteredStockInReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var filteredstockInModels = _stockService.GetStockInModels();

                if (fromDate.HasValue && toDate.HasValue)
                {
                    filteredstockInModels = filteredstockInModels.Where(s => s.Stock.GrnDate >= fromDate.Value && s.Stock.GrnDate <= toDate.Value)
                        .OrderByDescending(s => s.Stock.GrnDate)
                        .ToList();
                }

                return Json(filteredstockInModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return Json("");

        }

        [HttpGet]
        public JsonResult FilteredStockOutReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var filteredstockOutModels = _stockService.GetStockOutModels();

                if (fromDate.HasValue && toDate.HasValue)
                {
                    filteredstockOutModels = filteredstockOutModels.Where(s => s.CurrentDate >= fromDate.Value && s.CurrentDate <= toDate.Value)
                        .OrderByDescending(s => s.CurrentDate)
                        .ToList();
                }

                return Json(filteredstockOutModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return Json("");

        }

    }
}

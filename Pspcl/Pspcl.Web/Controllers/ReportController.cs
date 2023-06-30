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
        private readonly IBlobStorageService _blobStorageService;
        private readonly ILogger<DbInitializer> _logger;
        public ReportController(ApplicationDbContext _dbContext, IBlobStorageService blobStorageService, IStockService stockService, ILogger<DbInitializer> logger) {
            _context = _dbContext;
            _stockService = stockService;
            _blobStorageService = blobStorageService;
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
                var filteredStockOutModels = _stockService.GetStockOutModels();

                if (fromDate.HasValue && toDate.HasValue)
                {
                    filteredStockOutModels = filteredStockOutModels.Where(s => s.CurrentDate.Date >= fromDate.Value.Date && s.CurrentDate.Date <= toDate.Value.Date)
                         .OrderByDescending(s => s.CurrentDate)
                         .ToList();
                }

                return Json(filteredStockOutModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return Json("");

        }
        [HttpGet]
        public JsonResult FilteredAvailableStockReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var filteredAvailableStockModels = _stockService.GetAvailableStock();

                if (fromDate.HasValue && toDate.HasValue)
                {
                    filteredAvailableStockModels = filteredAvailableStockModels.Where(s => s.grnDate.Date >= fromDate.Value.Date && s.grnDate.Date <= toDate.Value.Date)
                         .OrderByDescending(s => s.grnDate)
                         .ToList();
                }

                return Json(filteredAvailableStockModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return Json("");

        }

        
        public JsonResult DownloadImage(string filename)
        {
            string downloadStatus = _blobStorageService.DownloadFileFromBlob(filename);

            if (downloadStatus == "DownloadFailed")
            {
                return Json("Failed");
            }
            else
            {
                return Json(downloadStatus + " " + "saved.");
            }

        }

    }
}

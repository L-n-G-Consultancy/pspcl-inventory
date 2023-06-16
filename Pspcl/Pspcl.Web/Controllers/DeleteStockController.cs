using Microsoft.AspNetCore.Mvc;
using Pspcl.DBConnect.Install;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;

namespace Pspcl.Web.Controllers
{
    public class DeleteStockController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockService _stockService;
        private readonly ILogger<DbInitializer> _logger;
        public DeleteStockController(ApplicationDbContext _dbContext, IStockService stockService, ILogger<DbInitializer> logger)
        {
            _context = _dbContext;
            _stockService = stockService;
            _logger = logger;
        }
        public IActionResult DeleteStock()
        {
            try
            {
                var deleteStockModel = _stockService.GetAvailableStock();

                return View(deleteStockModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
            return View();
        }
    }
}
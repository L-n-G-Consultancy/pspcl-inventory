using Microsoft.AspNetCore.Mvc;
using Pspcl.DBConnect.Install;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Pspcl.Services.Models;

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

        //[HttpPost]
        //public IActionResult StockToDelete(List<Object> selectedRows)
        //{
        //    try
        //    {
        //        var DeleteData = _stockService.GetStockToDelete(selectedRows);

        //        return View(DeleteData);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception");
        //    }
        //    return View();
        //}


        [HttpPost]
        public ActionResult StockToDelete(List<List<int>> selectedRows) 
        {

            List<List<int>> selectedRowsToDelete = new List<List<int>>
            {
                new List<int> { 2,151,200,50},
                new List<int> { 3,221,275,55},
                new List<int>  { 5,100,175,65}
            };

            int test = _stockService.UpdateIsDeletedColumn(selectedRowsToDelete);
            int test1 = _stockService.UpdateStockMaterial(selectedRowsToDelete);
            return Json(1); 
        }
        
    }
    public class SelectedRow
    {
        public string StockMaterialId { get; set; }
        public string SrNoFrom { get; set; }
        public string SrNoTo { get; set; }
        public string Quantity { get; set; }
    }

}
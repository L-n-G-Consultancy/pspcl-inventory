using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;

namespace Pspcl.Web.Controllers
{
	public class AddStockController : Controller
	{
		private readonly IStockService _StockService;
		public AddStockController(IStockService addStockService)
		{
			_StockService = addStockService;
		}

		public IActionResult AddStock()
		{
			var materialGroup = _StockService.GetAllMaterialGroups(true);
			//var rating = _StockService.GetAllMaterialRatings(null,null, true);
			StockViewModel viewModel = new StockViewModel();
			viewModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			return View(viewModel);
		}

		public JsonResult getResult(int materialGroupId)
		{
			StockViewModel viewModel = new StockViewModel();
			var materialType = _StockService.GetAllMaterialTypes(materialGroupId, true);
			viewModel.AvailableMaterialTypes = materialType.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			return Json(viewModel.AvailableMaterialTypes);
		}
		
        public JsonResult getRating(int materialTypeId)
        {
            StockViewModel viewModel = new StockViewModel();
            var materialType = _StockService.GetAllMaterialRatings(materialTypeId, true);
            viewModel.AvailableRatings = materialType.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Rating }).ToList();
            return Json(viewModel.AvailableRatings);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services;
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
			//var materialType = _StockService.GetAllMaterialTypes(null, true);
			var rating = _StockService.GetAllMaterialRatings(null,null, true);
			StockViewModel viewModel = new StockViewModel();
			viewModel.AvailableMaterialGroups = materialGroup.Select(x=> new SelectListItem() {Value=x.Id.ToString(),Text=x.Name}).ToList();
			//viewModel.AvailableMaterialTypes = (IList<SelectListItem>)materialType;
			viewModel.AvailableRatings = (IList<SelectListItem>)rating;
			return View(viewModel);
		}
	}
}

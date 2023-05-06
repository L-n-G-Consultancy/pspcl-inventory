using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;

namespace Pspcl.Web.Controllers
{
    public class IssueStockController : Controller
    {
		private readonly IStockService _stockService;
		private readonly IMapper _mapper;

		public IssueStockController(IStockService stockService, IMapper mapper)
		{
			_stockService = stockService;
			_mapper = mapper;
		}
		public IActionResult IssueStockView()
        {
			var subDivisions = _stockService.GetAllSubDivisions();
			IssueStockModel issueStockModel = new IssueStockModel();
			issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			return View(issueStockModel);
			
        }


		//public JsonResult getDivision(int subDivisionId)
		//{
		//	StockViewModel viewModel = new StockViewModel();
		//	var materialType = _stockService.GetAllMaterialTypes(subDivisionId);
		//	viewModel.AvailableMaterialTypes = materialType.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
		//	return Json(viewModel.AvailableMaterialTypes);
		//}
	}
}

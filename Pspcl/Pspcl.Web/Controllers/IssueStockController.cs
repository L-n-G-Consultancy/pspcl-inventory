using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pspcl.Core.Domain;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;
using System.Text.Json;

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

        [HttpGet]
        public IActionResult IssueStockView()
        {
			var subDivisions = _stockService.GetAllSubDivisions();
			var materialGroup = _stockService.GetAllMaterialGroups();
			//var make = _stockService.GetAllMakes();

			IssueStockModel issueStockModel = new IssueStockModel();

			issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			issueStockModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			//issueStockModel.AvailableMakes = make.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Make }).ToList();

			return View(issueStockModel);

		}
		public JsonResult GetCircleAndDivision(int selectedSubDivId)
		{
			List<string> divisionAndCircle = _stockService.GetCircleAndDivision(selectedSubDivId);
			var result = Json(divisionAndCircle);
			return result;
		}

        [HttpPost]
        public ActionResult IssueStockView(IssueStockModel model)
        {

			int selectedMaterialGroupId = model.MaterialGroupId;
			int selectedMaterialTypeId= model.MaterialTypeId;
			int selectedMaterialCodeId = model.MaterialCodeId;

			List<int> Ids = new List<int>();
			Ids.Add(selectedMaterialGroupId);
			Ids.Add(selectedMaterialTypeId);
			Ids.Add(selectedMaterialCodeId);

			List<Stock> Test= _stockService.GetAvailableQuantity(Ids);


			return View();
        }

		//public List<int> GetAvailableQuantities(int selectedMaterialGroupId, int selectedMaterialTypeId, int selectedMaterialCodeId) 
		//{

			

		//	List<int> availableQuantities = new List<int>();
		//	return availableQuantities;
		//}


	}
}

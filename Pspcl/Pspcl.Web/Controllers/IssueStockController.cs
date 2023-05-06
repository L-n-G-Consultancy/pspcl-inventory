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
			var materialGroup = _stockService.GetAllMaterialGroups();

			IssueStockModel issueStockModel = new IssueStockModel();

			issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			issueStockModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			return View(issueStockModel);

		}
	}
}

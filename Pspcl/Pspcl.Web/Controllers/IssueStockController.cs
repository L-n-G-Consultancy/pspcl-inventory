using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pspcl.Core.Domain;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;
using System;
using System.Collections.Generic;
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
			List<string> make = _stockService.GetAllMakes();

			IssueStockModel issueStockModel = new IssueStockModel();

			issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			issueStockModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			//issueStockModel.AvailableMakes = make;
			ViewBag.StringList = make.Any() ? make : new List<string> { "" };

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

			return View();
			
        }	
		public JsonResult GetAvailableStockRows(int materialGroupId, int materialTypeId, int materialId)
		{
			int MaterialGroupId = materialGroupId;
			int MaterialTypeId = materialTypeId;
			int MaterialCodeId = materialId;

			List<int> Ids = new List<int>();
			Ids.Add(MaterialGroupId);
			Ids.Add(MaterialTypeId);
			Ids.Add(MaterialCodeId);
			List<List<int>> Ranges = _stockService.GetAvailableQuantity(Ids);

			//foreach (List<int> range in Ranges)
			//{
			//	Console.WriteLine($"SrNoFrom: {range[0]}, SrNoTo: {range[1]}, Quantity: {range[2]}");
			//}
			//int requiredQuantity = 9;
			//List<List<int>> IssuedStockRanges = new List<List<int>>();
			//for(int i = 0; i<=Ranges.Count; i++)
			//{
			//	var currentRow= Ranges[i];
			//	if (currentRow[2] <= requiredQuantity)
			//	{
			//		IssuedStockRanges.Add(currentRow);
			//		requiredQuantity  -= currentRow[2];
					
			//	}
			//	else
			//	{
			//		int subQuantity= currentRow[2];
			//		currentRow[1] = currentRow[0] + (requiredQuantity - 1);
			//		currentRow[2] = requiredQuantity;
			//		IssuedStockRanges.Add(currentRow);
			//		break;
			//	}
			//	Console.WriteLine(IssuedStockRanges);
			//}

			//List<int> SrNo = new List<int>();
			//foreach(var issueStockRanges in IssuedStockRanges)
			//{
			//	for(int i= issueStockRanges[0]; i<= issueStockRanges[1]; i++)
			//	{
			//		SrNo.Add(i);
			//	}			
			//}
			Console.WriteLine(Ranges);
			return Json(Ranges);
		}

		
		



	}
}

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
			IssueStockModel issueStockModel = new IssueStockModel();

			issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			issueStockModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
			
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
			int MaterialGroupId = model.MaterialGroupId;
			int MaterialTypeId = model.MaterialTypeId;
			int? MaterialCodeId = model.MaterialCodeId;

			int RequiredQuantity = model.Quantity;

			List<int> Ids = new List<int>();
			Ids.Add(MaterialGroupId);
			Ids.Add(MaterialTypeId);
			Ids.Add((int)MaterialCodeId);
			List<List<int>> Ranges = _stockService.GetAvailableQuantity(Ids);


			int requiredQuantity = model.Quantity;

			List<List<int>> IssuedStockRanges = new List<List<int>>();
			for (int i = 0; i <= Ranges.Count; i++)
			{
				var currentRow = Ranges[i];
				if (currentRow[2] <= requiredQuantity)
				{
					IssuedStockRanges.Add(currentRow);
					requiredQuantity -= currentRow[2];

				}
				else
				{
					currentRow[1] = currentRow[0] + requiredQuantity - 1;
					currentRow[2] = requiredQuantity;
					IssuedStockRanges.Add(currentRow);
					break;
				}
			}



			return RedirectToAction("Index", "Home");
        }	
		public JsonResult GetAvailableStockRows(int materialGroupId, int materialTypeId, int materialId)
		{
			int sum=0;
			List<int> Ids = new List<int>();
			Ids.Add(materialGroupId);
			Ids.Add(materialTypeId);
			Ids.Add((int)materialId);
			List<List<int>> Ranges = _stockService.GetAvailableQuantity(Ids);
			foreach (List<int> range in Ranges)
			{
				 sum = sum + range[2];
				//sum = 0;
			}
			return Json(sum);
		}


		public JsonResult GetAllMakes(int materialGroupId, int materialTypeId, int materialId)
		{
			
			List<string> make = _stockService.GetAllMakes(materialGroupId, materialTypeId, materialId);
			List<string> MakeList = make.Any() ? make : new List<string> { "" };

			return Json(MakeList);
		}


	}
}

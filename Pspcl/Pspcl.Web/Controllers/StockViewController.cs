using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services;
using Pspcl.Web.Models;
using Pspcl.Core.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Pspcl.Web.Controllers
{

	public class StockViewController : Controller
	{

		private readonly IStockService? _stockService;
		private readonly IMapper? _mapper;




		[HttpGet]
		public IActionResult AddStock()
		{
			var model = new Pspcl.Web.Models.StockViewModel();

			return View(model);
		}
		[HttpPost]
		public IActionResult AddStock(StockViewModel model, IFormCollection formCollection)
		{
			model.SelectedMaterialCode = formCollection["selectedMaterialCode"];

			DateTime date = DateTime.Parse(formCollection["GRNDate"]);

			model.GrnDate = date;
			model.TestReportReference = formCollection["TestReportReference"];
			model.SelectedMaterialCode = formCollection["materialCode"];
			model.InvoiceDate = DateTime.Parse(formCollection["InvoiceDate"]);
			model.InvoiceNumber = formCollection["invoiceNo"];
			model.SelectedMaterialCode = formCollection["materialCode"];
			model.EnterRate = decimal.Parse(formCollection["rate"]);
			model.SelectedMaterialGroup = formCollection["materialGroups"];
			model.SelectedMaterialType = formCollection["materialType"];
			model.SelectedRating = formCollection["rating"];
			model.EnterRate = long.Parse(formCollection["GrnNO"]);
			model.PrefixNumber = formCollection["PrefixNumber"];


			List<StockMaterial> stockMaterialsList = new List<StockMaterial>();

			for (int i = 12; i < formCollection.Count - 1; i = i + 3)
			{
				var element_from = formCollection.ElementAt(i);
				var element_to = formCollection.ElementAt(i + 1);
				var element_qty = formCollection.ElementAt(i + 2);

				StockMaterial row = new()
				{
					SerialNumberFrom = Convert.ToInt32(element_from.Value),
					SerialNumberTo = int.Parse(element_to.Value),
					Quantity = int.Parse(element_qty.Value),
				};
				stockMaterialsList.Add(row);
			}

			model.stockMaterialLList = stockMaterialsList;
			
			return RedirectToAction("AddStock");
		}


	}
}
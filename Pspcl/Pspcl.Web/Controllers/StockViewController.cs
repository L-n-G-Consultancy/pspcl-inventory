using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Services.Interfaces;
using Pspcl.Core.Domain;
using Pspcl.Web.ViewModels;
using Newtonsoft.Json;

namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class StockViewController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockViewController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddStock()
        {
            var json = TempData["StockViewModel"] as string;
            var materialGroup = _stockService.GetAllMaterialGroups();

            if (json != null)
            {
                var model = JsonConvert.DeserializeObject<StockViewModel>(json);
                model.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
                model.AvailableMaterialTypes = _stockService.GetAllMaterialTypes((int)model.MaterialGroupId).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
                model.AvailableRatings = _stockService.GetAllMaterialRatings((int)model.MaterialTypeId).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Rating }).ToList();
                model.AvailableMaterialCodes = _stockService.GetAllMaterialCodes((int)model.MaterialTypeId).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Code }).ToList();
                return View(model);
            }
            else
            {
                StockViewModel viewModel = new StockViewModel();
                viewModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
                return View(viewModel);
            }
            
        }

        public JsonResult getMaterialTypes(int materialGroupId)
        {
            StockViewModel viewModel = new StockViewModel();
            var materialType = _stockService.GetAllMaterialTypes(materialGroupId);
            viewModel.AvailableMaterialTypes = materialType.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return Json(viewModel.AvailableMaterialTypes);
        }

        public JsonResult getRating(int materialTypeId)
        {
            StockViewModel viewModel = new StockViewModel();
            var materialType = _stockService.GetAllMaterialRatings(materialTypeId);
            viewModel.AvailableRatings = materialType.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Rating }).ToList();
            return Json(viewModel.AvailableRatings);
        }

        public JsonResult getMaterialCodes(int materialTypeId)
        {
            StockViewModel viewModel = new StockViewModel();
            var materialCodes = _stockService.GetAllMaterialCodes(materialTypeId);
            viewModel.AvailableMaterialCodes = materialCodes.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Code }).ToList();
            return Json(viewModel.AvailableMaterialCodes);
        }

        [HttpPost]
        public IActionResult AddStock(IFormCollection formCollection)
        {

            var model = new StockViewModel();

				DateTime date = DateTime.Parse(formCollection["GRNDate"]);
				model.GrnDate = date;
				model.TestReportReference = formCollection["TestReportReference"];
				model.InvoiceDate = DateTime.Parse(formCollection["InvoiceDate"]);
				model.InvoiceNumber = formCollection["InvoiceNumber"];
				model.MaterialIdByCode = int.Parse(formCollection["MaterialIdByCode"]);
				model.Rate = decimal.Parse(formCollection["Rate"]);
				model.MaterialGroupId = int.Parse(formCollection["MaterialGroupId"]);
				model.MaterialTypeId = int.Parse(formCollection["MaterialTypeId"]);
				model.Rating = formCollection["Rating"];
				model.GrnNumber =formCollection["GrnNumber"];
				model.PrefixNumber = formCollection["PrefixNumber"];
                model.Make = formCollection["Make"];

            List<StockMaterial> stockMaterialsList = new List<StockMaterial>();

				for (int i = 12; i < formCollection.Count - 2; i = i + 3)
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
                model.stockMaterialList = stockMaterialsList;
               TempData["StockViewModel"] = JsonConvert.SerializeObject(model);

             return RedirectToAction("Preview", "Preview");
        }
    }
}

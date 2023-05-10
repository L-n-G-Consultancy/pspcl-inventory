using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Services.Interfaces;
using Pspcl.Core.Domain;
using Pspcl.Web.ViewModels;

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
            var materialGroup = _stockService.GetAllMaterialGroups();
            StockViewModel viewModel = new StockViewModel();
            viewModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View(viewModel);
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
			try
			{
				var model = new StockViewModel();
				//var formValues = HttpContext.Request.Form;
				//// Convert the FormCollection to a dictionary
				//var formData = formValues.ToDictionary(x => x.Key, x => x.Value.ToString());
				//// Store the serialized dictionary in the temp data
				//TempData["FormData"] = JsonConvert.SerializeObject(formData);

				model.SelectedMaterialCode = formCollection["selectedMaterialCode"];
				DateTime date = DateTime.Parse(formCollection["GRNDate"]);
				model.GrnDate = date;
				model.TestReportReference = formCollection["TestReportReference"];
				model.InvoiceDate = DateTime.Parse(formCollection["InvoiceDate"]);
				model.InvoiceNumber = formCollection["invoiceNo"];
				model.SelectedMaterialCode = formCollection["materialCode"];
				model.Rate = decimal.Parse(formCollection["rate"]);
				model.MaterialGroupId = int.Parse(formCollection["materialGroup"]);
				model.MaterialTypeId = int.Parse(formCollection["materialType"]);
				model.Rating = formCollection["rating"];
				model.GrnNumber = formCollection["GrnNO"];
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

				model.stockMaterialList = stockMaterialsList;

				if (ModelState.IsValid)
				{
					var stockEntity = _mapper.Map<Stock>(model);

					int materialId = _stockService.GetMaterialByType(model.MaterialTypeId, model.SelectedMaterialCode);
					stockEntity.MaterialId = materialId;

					var newStockId = _stockService.AddStock(stockEntity);


					int displayOrder = 1;
					foreach (var stockMaterial in model.stockMaterialList)
					{
						stockMaterial.StockId = newStockId;
						stockMaterial.DisplayOrder = displayOrder++;
					}

					foreach (var stockMaterialViewModel in model.stockMaterialList)
					{
						var stockMaterialEntity = _mapper.Map<StockMaterial>(stockMaterialViewModel);
						_stockService.AddStockMaterial(stockMaterialEntity);
					}

                    ViewBag.Message = "Stock saved successfully";
                    TempData["Message"] = ViewBag.Message;
                    return RedirectToAction("Index","Home");
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = ex.Message;
				return View();
			}

			return View();

        }
    }
}

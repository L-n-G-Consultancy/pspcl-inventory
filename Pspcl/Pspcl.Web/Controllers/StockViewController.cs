using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pspcl.Core.Domain;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;


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
            var model = new StockViewModel();
            DateTime date = DateTime.Parse(formCollection["GRNDate"]);
            model.GrnDate = date;
            model.TestReportReference = formCollection["TestReportReference"];
            model.InvoiceDate = DateTime.Parse(formCollection["InvoiceDate"]);
            model.InvoiceNumber = formCollection["invoiceNo"];
            model.SelectedMaterialCode = int.Parse(formCollection["materialCode"]);
            model.EnterRate = decimal.Parse(formCollection["rate"]);
            model.MaterialGroupId = int.Parse(formCollection["materialGroup"]);
            model.MaterialTypeId = int.Parse(formCollection["materialType"]);
            model.Rating = formCollection["rating"];
            model.GrnNumber = long.Parse(formCollection["GrnNO"]);
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
            TempData["StockViewModel"]=JsonConvert.SerializeObject(model);
            return RedirectToAction("Preview", "Preview");
        }
    }
}

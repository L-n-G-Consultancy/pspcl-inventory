using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pspcl.Core.Domain;
using Pspcl.Services.Interfaces;
using Pspcl.Web.ViewModels;

namespace Pspcl.Web.Controllers
{
    [Authorize]
    public class PreviewController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        public PreviewController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Preview()
        {
            var jsonResponse = TempData["StockViewModel"] as string;
            var stockViewModel = JsonConvert.DeserializeObject<StockViewModel>(jsonResponse);
            var materialGroupName = _stockService.GetMaterialGroupById(stockViewModel.MaterialGroupId);
            var materialTypeName = _stockService.GetMaterialTypeById(stockViewModel.MaterialTypeId);
            var materialCodeName = _stockService.GetMaterialCodeById(stockViewModel.MaterialIdByCode);
            var ratingName = _stockService.GetRatingNameById(stockViewModel.MaterialTypeId);
            stockViewModel.SelectedMaterialGroupName = materialGroupName;
            stockViewModel.SelectedMaterialTypeName = materialTypeName;
            stockViewModel.SelectedMaterialCodeName = materialCodeName;
            stockViewModel.SelectedRatingName = ratingName;
            return View(stockViewModel);
        }
        [HttpPost]
        public IActionResult Preview(IFormCollection formCollection,string save)
        {
            try
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
                model.Rating = formCollection["rating"];
                model.GrnNumber = formCollection["GrnNumber"];
                model.PrefixNumber = formCollection["PrefixNumber"];
                model.Make = formCollection["Make"];

                List<StockMaterial> stockMaterialsList = new List<StockMaterial>();

                for (int i = 16; i < formCollection.Count - 3; i = i + 3)
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
                    if (save == "save")
                    {
                        var stockEntity = _mapper.Map<Stock>(model);
                        int materialId = (int)model.MaterialIdByCode;
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
                            var stockMaterailId=  _stockService.AddStockMaterial(stockMaterialEntity);
                            for (int i = stockMaterialEntity.SerialNumberFrom; i <= stockMaterialEntity.SerialNumberTo; i++)
                            {
                                var stockmaterailseries = new StockMaterialSeries();
                                stockmaterailseries.StockMaterialId = stockMaterailId;
                                stockmaterailseries.SerialNumber = i;
                                stockmaterailseries.IsIssued = false;
                                _stockService.AddStockMaterialSeries(stockmaterailseries);
                            }

                        }
                        ViewBag.Message = "Stock saved successfully";
                        TempData["Message"] = ViewBag.Message;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["StockViewModel"] = JsonConvert.SerializeObject(model);
                        return RedirectToAction("AddStock", "StockView");

                    }
                   
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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;

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
            var model = JsonConvert.DeserializeObject<StockViewModel>(jsonResponse);
            var materialGroupName = _stockService.GetMaterialGroupById(model.MaterialGroupId);
            var materialTypeName = _stockService.GetMaterialTypeById(model.MaterialTypeId);
            var materialCodeName = _stockService.GetMaterialCodeById(model.SelectedMaterialCode);
            var ratingName = _stockService.GetRatingNameById(model.MaterialTypeId);
            model.SelectedMaterialGroupName=materialGroupName;
            model.SelectedMaterialTypeName=materialTypeName;
            model.SelectedMaterialCodeName=materialCodeName;
            model.SelectedRatingName=ratingName;

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Preview()
        //{
        //try
        //{
        //    var model = new StockViewModel();

        //    if (ModelState.IsValid)
        //    {
        //        var stockEntity = _mapper.Map<Stock>(model);

        //        int materialId = _stockService.GetMaterialByType(model.MaterialTypeId, model.SelectedMaterialCode);
        //        stockEntity.MaterialId = materialId;
        //        var newStockId = _stockService.AddStock(stockEntity);


        //        int displayOrder = 1;
        //        foreach (var stockMaterial in model.stockMaterialList)
        //        {
        //            stockMaterial.StockId = newStockId;
        //            stockMaterial.DisplayOrder = displayOrder++;
        //        }

        //        foreach (var stockMaterialViewModel in model.stockMaterialList)
        //        {
        //            var stockMaterialEntity = _mapper.Map<StockMaterial>(stockMaterialViewModel);
        //            _stockService.AddStockMaterial(stockMaterialEntity);
        //        }

        //        ViewBag.Message = "Stock saved successfully";
        //        TempData["Message"] = ViewBag.Message;
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ViewBag.Message = ex.Message;
        //    return View();
        ////}
        //return View();
    }
}

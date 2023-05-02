using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult AddStock(StockViewModel model, IFormCollection formCollection)
        {
            return RedirectToAction("AddStock");
        }
    }
}

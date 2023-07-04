using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pspcl.Core.Domain;
using Pspcl.Services;
using Pspcl.Services.Interfaces;
using Pspcl.Web.Models;



namespace Pspcl.Web.Controllers
{
    public class IssueStockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IBlobStorageService _blobStorageService;

        private readonly IMapper _mapper;
        private readonly ILogger<StockViewController> _logger;
        public IssueStockController(IStockService stockService, IBlobStorageService blobStorageService, IMapper mapper, ILogger<StockViewController> logger)
        {
            _stockService = stockService;
            _blobStorageService = blobStorageService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult IssueStockView()
        {
            var subDivisions = _stockService.GetAllSubDivisions();
            var materialGroup = _stockService.GetAllMaterialGroups();
            IssueStockModel issueStockModel = new IssueStockModel();

            issueStockModel.SubDivisionList = subDivisions.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            issueStockModel.AvailableMaterialGroups = materialGroup.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var issuedMakesAndRowsJson = TempData["issuedMakesAndRows"] as string;
            if (issuedMakesAndRowsJson != null)
            {
                issueStockModel.IssuedStockRanges = JsonConvert.DeserializeObject<Dictionary<string, List<List<int>>>>(issuedMakesAndRowsJson);
            }

            return View(issueStockModel);

        }
        public JsonResult GetCircleAndDivisionAndLocationCode(int selectedSubDivId)
        {
            List<string> divisionAndCircle = _stockService.GetCircleAndDivisionAndLocationCode(selectedSubDivId);
            var result = Json(divisionAndCircle);
            return result;
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IssueStockView(IFormCollection formCollection)
        {

            int materialGroupId = Convert.ToInt32(formCollection["MaterialGroupId"]);
            int materialTypeId = Convert.ToInt32(formCollection["MaterialTypeId"]);
            int materialCodeId = Convert.ToInt32(formCollection["MaterialId"]);


            Dictionary<string, List<List<int>>> issuedMakesAndRows = new Dictionary<string, List<List<int>>>();

            Dictionary<string, List<List<int>>> availableMakeAndRows = new Dictionary<string, List<List<int>>>();
            availableMakeAndRows = _stockService.GetAvailableMakesAndRows(materialGroupId, materialTypeId, materialCodeId);

            var errorResponse = "-1";

            int x;
            if (formCollection.Files.Count == 0)
            {
                x = 14;
            }
            else x = 13;
            foreach (KeyValuePair<string, List<List<int>>> kvp in availableMakeAndRows)
			 {				
				for (int i = x; i < formCollection.Count - 1;)
				{
					var element_make = formCollection.ElementAt(i).Value;
					var element_availableQty = formCollection.ElementAt(i + 1);
					var element_requiredQty = formCollection.ElementAt(i + 2);

                    string make = element_make.ToString();
                    int availableQty = Convert.ToInt32(element_availableQty.Value);
                    int requiredQty = Convert.ToInt32(element_requiredQty.Value);

                    List<List<int>> IssuedDataRows = new List<List<int>>();

                    if (requiredQty <= availableQty)
                    {

                        List<List<int>> value = kvp.Value;

                        for (int j = 0; j <= value.Count - 1; j++)
                        {
                            var currentRow = value[j];
                            if (currentRow[3] <= requiredQty)
                            {
                                IssuedDataRows.Add(currentRow);
                                requiredQty -= currentRow[3];
                            }
                            else
                            {
                                if (requiredQty > 0)
                                {
                                    currentRow[2] = currentRow[1] + requiredQty - 1;
                                    currentRow[3] = requiredQty;
                                    IssuedDataRows.Add(currentRow);
                                    break;
                                }
                                break;
                            }
                        }
                    }

                    
                    issuedMakesAndRows.Add(make, IssuedDataRows);
                    _stockService.UpdateStockMaterialSeries(IssuedDataRows);
                    x = i + 3;
                    break;
                }
            }

            TempData["issuedMakesAndRows"] = JsonConvert.SerializeObject(issuedMakesAndRows);

            TempData["Message"] = "Stock Issued Successfully..!";

            StockIssueBook stockIssueBook = new StockIssueBook();   

            stockIssueBook.TransactionId = "transaction";
            stockIssueBook.CurrentDate = DateTime.Now;
            stockIssueBook.SrNoDate = DateTime.Parse(formCollection["SrNoDate"]);
            stockIssueBook.SerialNumber = formCollection["SerialNumber"];
            stockIssueBook.DivisionId = int.Parse(formCollection["DivisionId"]);
            stockIssueBook.SubDivisionId = int.Parse(formCollection["SubDivisionId"]);
            stockIssueBook.CircleId = int.Parse(formCollection["CircleId"]);
            stockIssueBook.JuniorEngineerName = formCollection["JuniorEngineerName"];


            StockBookMaterial stockBookMaterial1 = new StockBookMaterial();

            stockBookMaterial1.MaterialGroupId = int.Parse(formCollection["MaterialGroupId"]);
            stockBookMaterial1.MaterialId = int.Parse(formCollection["MaterialId"]);

            if (formCollection.Files.Count != 0 )
            {
                string response = _blobStorageService.UploadImageToAzure(formCollection.Files[0]);
                if(response == "failure")
                {
                    TempData["ImageNotUploaded"] = "An error occurred while uploading picture!";
                }
                
                else
                {
                    stockIssueBook.Image = response == String.Empty ? String.Empty : (response == errorResponse ? errorResponse : response);

                }

                if (stockIssueBook.Image == errorResponse)
                {
                    return View("Error");
                }
            }
            else
            {
                stockIssueBook.Image = String.Empty; 
            }

            StockIssueBook stockIssueBookEntity = _mapper.Map<StockIssueBook>(stockIssueBook);
            int id = _stockService.IssueStock(stockIssueBookEntity);
            foreach (KeyValuePair<string, List<List<int>>> keyValuePair in issuedMakesAndRows)
            {
                if (keyValuePair.Value.Count > 0)
                {

                    stockBookMaterial1.StockIssueBookId = id;

                    int quantity = 0;
                    for (int i = 0; i < keyValuePair.Value.Count; i++)
                    {
                        quantity += keyValuePair.Value[i][3];
                    }

                    stockBookMaterial1.Quantity = quantity;
                    stockBookMaterial1.Make = keyValuePair.Key;
                    stockBookMaterial1.Id = 0;
                    StockBookMaterial stockBookMaterial = _mapper.Map<StockBookMaterial>(stockBookMaterial1);
                    _stockService.StockBookMaterial(stockBookMaterial);
                }
                else
                {
                    continue;
                }
            }
            return RedirectToAction("IssueStockView", "IssueStock");
        }	
		
		public JsonResult DisplayMakeWithQuantity(int materialGroupId, int materialTypeId, int materialId)
		{
			Dictionary <string,int> Result = new Dictionary<string,int>();
			Result = _stockService.AllMakesAndQuantities(materialGroupId, materialTypeId, materialId);
            return Json(Result);
        }

         public JsonResult GetCost(int materialId, int noOfUnits)

         {
            float Result = _stockService.GetCost(materialId, noOfUnits);
            return Json(Result);
         }

    }
}

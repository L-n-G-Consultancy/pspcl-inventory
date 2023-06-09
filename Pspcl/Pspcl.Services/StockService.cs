
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;
using Pspcl.Services.Models;
using System.Collections;
using System.Collections.Generic;

namespace Pspcl.Services
{

    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _dbcontext;
        public StockService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialGroup.ToList();
            }
            return _dbcontext.MaterialGroup.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
        }
        public List<MaterialType> GetAllMaterialTypes(int materialGroupId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialType.Where(x => x.MaterialGroupId == materialGroupId).ToList();
            }
            var materialTypes = _dbcontext.MaterialType.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return materialTypes.Where(x => x.MaterialGroupId == materialGroupId).ToList();

        }
        public List<Tuple<int, string>> GetAllMaterialRatings(int materialTypeId)
        {
            return _dbcontext.Rating
            .Join(_dbcontext.RatingMaterialTypeMapping, rating => rating.Id, mapping => mapping.RatingId, (rating, mapping) => new { rating, mapping })
            .Where(joinResult => joinResult.mapping.MaterialTypeId == materialTypeId)
           .Select(joinResult => Tuple.Create(joinResult.rating.Id, joinResult.rating.Name))
            .ToList();
        }

        public List<Material> GetAllMaterialCodes(int materialTypeId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.Material.Where(x => x.MaterialTypeId == materialTypeId).ToList();
            }
            var materialCodes = _dbcontext.Material.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return materialCodes.Where(x => x.MaterialTypeId == materialTypeId).ToList();
        }

        public List<SubDivision> GetAllSubDivisions(bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.SubDivision.ToList();
            }
            return _dbcontext.SubDivision.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
        }
        public List<string> GetCircleAndDivisionAndLocationCode(int selectedSubDivId, bool? onlyActive = false)
        {
            if (onlyActive.HasValue)
            {
                SubDivision subDivision = _dbcontext.SubDivision.FirstOrDefault(x => x.Id == selectedSubDivId);
                if (subDivision != null)
                {
                    int divId = subDivision.DivisionId;
                    string divisionId = divId.ToString();

                    Division Division = _dbcontext.Division.FirstOrDefault(x => x.Id == divId);
                    string divisionName = Division.Name.ToString();
                    string locationCode = Division.LocationCode.ToString();

                    int circleDiv = Division.CircleId;
                    string circleId = circleDiv.ToString();
                    Circle Circle = _dbcontext.Circle.FirstOrDefault(x => x.Id == circleDiv);
                    string circleName = Circle.Name.ToString();

                    List<string> DivisionCircle = new List<string>();
                    DivisionCircle.Add(divisionName);
                    DivisionCircle.Add(circleName);
                    DivisionCircle.Add(divisionId);
                    DivisionCircle.Add(circleId);
                    DivisionCircle.Add(locationCode);

                    return DivisionCircle;
                }
            }
            return new List<string>();
        }
        //     public List<List<int>> GetAvailableQuantity(List<int> Ids)
        //     {
        //int materialGroupId = Ids[0];
        //int materialTypeId = Ids[1];
        //int materialId = Ids[2];

        //List<Stock> stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId).ToList();
        //List<int> stockIds = stocks.Select(x => x.Id).ToList();
        //List<StockMaterial> Materials = _dbcontext.StockMaterial.Where(x => stockIds.Contains(x.StockId)).ToList();
        //List<int> idList = Materials.Select(x => x.Id).ToList();
        //var query = _dbcontext.StockMaterialSeries.Where(x => idList.Contains(x.StockMaterialId) && !x.IsIssued);
        //string sqlQuery = query.ToString();
        //var MaterialSeries = query.ToList();
        //List<int> quantities = MaterialSeries.Select(x => x.StockMaterialId).ToList();
        //int totalAvailableQuantity = quantities.Count();

        //var materialRanges = MaterialSeries.GroupBy(ms => ms.StockMaterialId).Select(g => new {StockMaterialId = g.Key,
        //       SrNoFrom = g.OrderBy(ms => ms.SerialNumber).First().SerialNumber,
        //       SrNoTo = g.OrderBy(ms => ms.SerialNumber).Last().SerialNumber
        //}) .ToList();

        //List<List<int>> ranges = materialRanges.Select(x => new List<int> { x.StockMaterialId,x.SrNoFrom, x.SrNoTo, (x.SrNoTo - x.SrNoFrom + 1) }).ToList();
        //return ranges;
        //     }
        public int AddStock(Stock stock)
        {
            _dbcontext.Set<Stock>().Add(stock);
            _dbcontext.SaveChanges();
            return stock.Id;
        }
        public int AddStockMaterial(StockMaterial stockMaterial)
        {
            _dbcontext.Set<StockMaterial>().Add(stockMaterial);
            _dbcontext.SaveChanges();
            return stockMaterial.Id;
        }
        public void AddStockMaterialSeries(StockMaterialSeries stockMaterialSeries)
        {
            _dbcontext.Set<StockMaterialSeries>().AddRange(stockMaterialSeries);
            _dbcontext.SaveChanges();
        }
        public List<StockInModel> GetStockInModels()
        {
            var stockInModels = _dbcontext.Stock
               .Select(s => new StockInModel
               {
                   Stock = s,
                   MaterialGroup = _dbcontext.MaterialGroup.Where(mg => mg.Id == s.MaterialGroupId).Select(mg => mg.Name).FirstOrDefault(),
                   MaterialName = _dbcontext.MaterialType.Where(mt => mt.Id == s.MaterialTypeId).Select(mt => mt.Name).FirstOrDefault(),
                   MaterialCode = _dbcontext.Material.Where(mt => mt.Id == s.MaterialId).Select(mt => mt.Code).FirstOrDefault(),
                   Quantity = _dbcontext.StockMaterial.Where(sm => sm.StockId == s.Id).Sum(sm => sm.Quantity)
               })
               .ToList();

            return stockInModels;
        }

        public List<StockOutModel> GetStockOutModels()
        {
            var stockOutModels = new List<StockOutModel>
            {

            };
            return stockOutModels;
        }

        public string GetMaterialGroupById(int? materialGroupId)
        {
            var response = _dbcontext.MaterialGroup.Where(x => x.Id == materialGroupId).Select(x => x.Name).FirstOrDefault();
            string materialGroupName = response.ToString();
            return materialGroupName;
        }
        public string GetMaterialTypeById(int? materialTypeId)
        {
            var response = _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).Select(x => x.Name).FirstOrDefault();
            string materialTypeName = response.ToString();
            return materialTypeName;
        }
        public string GetMaterialCodeById(int? materialIdByCode)
        {
            var response = _dbcontext.Material.Where(x => x.Id == materialIdByCode).Select(x => x.Code).FirstOrDefault();
            if (response == null)
            {
                return "None";
            }
            string materialCodeName = response.ToString();
            return materialCodeName;
        }
        public string GetRatingNameById(int? ratingId)
        {
            var response=_dbcontext.Rating.Where(x=>x.Id==ratingId).Select(x => x.Name).FirstOrDefault();
            if (response == null)
            {
                return "None";
            }
            return response;
        }

        public void UpdateStockMaterialSeries(List<List<int>> requiredIssueData)
        {
            foreach (var Item in requiredIssueData)
            {
                // Get the StockMaterialSeries records that meet the specified conditions
                var recordsToUpdate = _dbcontext.StockMaterialSeries.Where(x => x.StockMaterialId == Item[0] && x.SerialNumber >= Item[1] && x.SerialNumber <= Item[2]);

                // Loop through each record and update the abc column value to 1
                foreach (var record in recordsToUpdate)
                {
                    record.IsIssued = true;
                }
                _dbcontext.SaveChanges();
            }
        }
        public int IssueStock(StockIssueBook stockIssueBook)
        {
            _dbcontext.Set<StockIssueBook>().Add(stockIssueBook);

            _dbcontext.SaveChanges();
            return stockIssueBook.Id;
        }
        public void StockBookMaterial(StockBookMaterial stockBookMaterial)
        {
            _dbcontext.Set<StockBookMaterial>().Add(stockBookMaterial);
            _dbcontext.SaveChanges();
            return;
        }
        public Dictionary<String, int> AllMakesAndQuantitities(int materialGroupId, int materialTypeId, int materialId)
        {
            List<Stock> stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId).ToList();
            List<string> distinctMakes = stocks.Select(x => x.Make).Distinct().ToList();

            Dictionary<string, List<int>> makeWithStockIds = new Dictionary<string, List<int>>();
            List<int> stockId = new List<int>();
            foreach (string make in distinctMakes)
            {
                stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId && x.Make == make).ToList();
                stockId = stocks.Select(x => x.Id).ToList();

                makeWithStockIds.Add(make, stockId);
            }

            Dictionary<String, int> makesAndQuantities = new Dictionary<String, int>();

            foreach (KeyValuePair<string, List<int>> keyValuePair in makeWithStockIds)
            {
                string Make = keyValuePair.Key;
                List<int> stockIdList = keyValuePair.Value.ToList();

                var query = _dbcontext.StockMaterial.Where(x => stockIdList.Contains(x.StockId)).Select(x => x.Id);
                List<int> stockMaterialIdsList = query.ToList();

                List<StockMaterialSeries> Materials = _dbcontext.StockMaterialSeries.Where(x => stockMaterialIdsList.Contains(x.StockMaterialId) && !x.IsIssued).ToList();
                List<int> idList = Materials.Select(x => x.Id).ToList();
                int QuantityAgainstMake = idList.Count();

                makesAndQuantities.Add(Make, QuantityAgainstMake);
            }
            foreach(KeyValuePair<string,int> makeAndQuantity in makesAndQuantities)
            {
                if (makeAndQuantity.Value == 0)
                {
                    makesAndQuantities.Remove(makeAndQuantity.Key);
                }
            }
            return makesAndQuantities;
        }

        public Dictionary<string, List<List<int>>> GetAvailableMakesAndRows(int materialGroupId, int materialTypeId, int materialId)
        {
            Dictionary<string, List<List<int>>> availableMakesAndRows = new Dictionary<string, List<List<int>>>();

            List<Stock> stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId).ToList();
            List<string> distinctMakes = stocks.Select(x => x.Make).Distinct().ToList();

            Dictionary<string, List<int>> makeWithStockIds = new Dictionary<string, List<int>>();
            List<int> stockId = new List<int>();
            foreach (string make in distinctMakes)
            {
                stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId && x.Make == make).ToList();
                stockId = stocks.Select(x => x.Id).ToList();

                makeWithStockIds.Add(make, stockId);
            }
            foreach (KeyValuePair<string, List<int>> keyValuePair in makeWithStockIds)
            {
                string Make = keyValuePair.Key;
                List<int> stockIdList = keyValuePair.Value.ToList();

                var query = _dbcontext.StockMaterial.Where(x => stockIdList.Contains(x.StockId)).Select(x => x.Id);
                List<int> stockMaterialIdsList = query.ToList();

                List<StockMaterialSeries> Materials = _dbcontext.StockMaterialSeries.Where(x => stockMaterialIdsList.Contains(x.StockMaterialId) && !x.IsIssued).ToList();
                List<int> idList = Materials.Select(x => x.Id).ToList();


                var materialRanges = Materials.GroupBy(ms => ms.StockMaterialId).Select(g => new
                {
                    StockMaterialId = g.Key,
                    SrNoFrom = g.OrderBy(ms => ms.SerialNumber).First().SerialNumber,
                    SrNoTo = g.OrderBy(ms => ms.SerialNumber).Last().SerialNumber
                }).ToList();

                List<List<int>> ranges = materialRanges.Select(x => new List<int> { x.StockMaterialId, x.SrNoFrom, x.SrNoTo, (x.SrNoTo - x.SrNoFrom + 1) }).ToList();

                availableMakesAndRows.Add(Make, ranges);

                List<string> keysToRemove = new List<string>();

                foreach (KeyValuePair<string, List<List<int>>> MakeAndRows in availableMakesAndRows)
                {
                    if (MakeAndRows.Value.Count == 0)
                    {
                        keysToRemove.Add(MakeAndRows.Key);
                    }
                }

                foreach (string key in keysToRemove)
                {
                    availableMakesAndRows.Remove(key);
                }

            }
          return availableMakesAndRows;
        }
        public string GetCorrespondingMakeValue(string invoiceNumber)
        {
            List<Stock> stocks = _dbcontext.Stock.Where(x => x.InvoiceNumber == invoiceNumber).ToList();
            if (stocks.Count > 0)
            {
                string Make = stocks.Select(x => x.Make).FirstOrDefault().ToString();
                return Make;
            }
            else
                return "Enter Make";
        }

        public bool isGrnNumberExist(string GrnNumber)
        {
            List<Stock> stocks = _dbcontext.Stock.Where(x => x.GrnNumber == GrnNumber).ToList();
            if (stocks.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool srNoValidationInDatabase(List<int> serialNumbers, int materialGroupId, int materialTypeId, int materialId, string make)
        {

            List<Stock> stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId && x.Make == make).ToList();
            List<int> stockId = stocks.Select(x => x.Id).ToList();

            List<StockMaterial> stockMaterial = _dbcontext.StockMaterial.Where(x => stockId.Contains(x.StockId)).ToList();
            List<int> stockMaterialId = stockMaterial.Select(x => x.Id).ToList();


            List<StockMaterialSeries> stockMaterialSeries = _dbcontext.StockMaterialSeries.Where(x => stockMaterialId.Contains(x.StockMaterialId)).ToList();
            List<int> SerialNumberList = stockMaterialSeries.Select(x => x.SerialNumber).ToList();

            bool isContained = SerialNumberList.Any(x => serialNumbers.Contains(x));

            if (isContained)
            {

                return true;
            }
            return false;
        }

        public int GetCost(int materialId, int noOfUnits )    
        {
            List<Material> material = _dbcontext.Material.Where(x => x.Id == materialId).ToList();
            int testingCharges = material.Select(x => x.TestingCharges).First();

            List<Stock> stocks = _dbcontext.Stock.Where(x =>x.MaterialId == materialId).ToList();
            int rate = Convert.ToInt32(stocks.Select(x => x.Rate).First());

            int totalCost = (rate + ((3 * rate) / 100) + testingCharges) * noOfUnits;


            return totalCost;
        }



    }
         
}


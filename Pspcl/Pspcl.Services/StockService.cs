
using Microsoft.EntityFrameworkCore;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;
using Pspcl.Services.Models;


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

        public List<MaterialType> GetAllMaterialRatings(int materialTypeId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).ToList();
            }
            var rating = _dbcontext.MaterialType.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return rating.Where(x => x.Id == materialTypeId).ToList();
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

        public List<string> GetAllMakes(int materialGroupId, int materialTypeId, int materialId)
        {
			List<string> makes = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId== materialTypeId && x.MaterialId== materialId).Select(s => s.Make).Distinct().ToList();

            return makes;
        }



        public List<string> GetCircleAndDivision(int selectedSubDivId, bool? onlyActive = false)
		{
			if (onlyActive.HasValue)
			{
				SubDivision subDivision = _dbcontext.SubDivision.FirstOrDefault(x => x.Id == selectedSubDivId);			
				if (subDivision != null)
				{
					int divId = subDivision.DivisionId;
                    string divisionId= divId.ToString();

					Division Division = _dbcontext.Division.FirstOrDefault(x => x.Id == divId);
					string divisionName = Division.Name.ToString();

					int circleDiv = Division.CircleId;
                    string circleId= circleDiv.ToString();
					Circle Circle = _dbcontext.Circle.FirstOrDefault(x => x.Id == circleDiv);
					string circleName = Circle.Name.ToString();

					List<string> DivisionCircle = new List<string>();
					DivisionCircle.Add(divisionName);
					DivisionCircle.Add(circleName);
                    DivisionCircle.Add(divisionId);
                    DivisionCircle.Add(circleId);

					return DivisionCircle;
				}
			}
			return new List<string>();
		}
        public List<List<int>> GetAvailableQuantity(List<int> Ids)
        {
			int materialGroupId = Ids[0];
			int materialTypeId = Ids[1];
			int materialId = Ids[2];
			Console.WriteLine("Material Group Id: " + materialGroupId);

			List<Stock> stocks = _dbcontext.Stock.Where(x => x.MaterialGroupId == materialGroupId && x.MaterialTypeId == materialTypeId && x.MaterialId == materialId).ToList();
			List<int> stockIds = stocks.Select(x => x.Id).ToList();
			List<StockMaterial> Materials = _dbcontext.StockMaterial.Where(x => stockIds.Contains(x.StockId)).ToList();
			List<int> idList = Materials.Select(x => x.Id).ToList();
			var query = _dbcontext.StockMaterialSeries.Where(x => idList.Contains(x.StockMaterialId) && !x.IsIssued);
			string sqlQuery = query.ToString();
			var MaterialSeries = query.ToList();
			List<int> quantities = MaterialSeries.Select(x => x.StockMaterialId).ToList();
			int totalAvailableQuantity = quantities.Count();
			Console.WriteLine(totalAvailableQuantity);

			var materialRanges = MaterialSeries.GroupBy(ms => ms.StockMaterialId).Select(g => new {StockMaterialId = g.Key,
		        SrNoFrom = g.OrderBy(ms => ms.SerialNumber).First().SerialNumber,
		        SrNoTo = g.OrderBy(ms => ms.SerialNumber).Last().SerialNumber
			}) .ToList();
            Console.WriteLine(materialRanges);

			List<List<int>> ranges = materialRanges.Select(x => new List<int> { x.StockMaterialId,x.SrNoFrom, x.SrNoTo, (x.SrNoTo - x.SrNoFrom + 1) }).ToList();
			return ranges;
        }

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
        public string GetRatingNameById(int? materialTypeId)
        {
            var response = _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).Select(x => x.Rating).FirstOrDefault();
            if (response == null)
            {
                return "None";
            }
            string Rating = response.ToString();
            return Rating;
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

				// Save the changes to the database
				_dbcontext.SaveChanges();
			}


		}
	
		public int IssueStock(StockIssueBook stockIssueBook)
		{
			_dbcontext.Set<StockIssueBook>().Add(stockIssueBook);
			_dbcontext.SaveChanges();
            return stockIssueBook.Id;
		}

		public void StockBookMaterial(StockBookMaterial stockBookMaterial, int id)
		{
			
			
			stockBookMaterial.StockIssueBookId =id;

			_dbcontext.Set<StockBookMaterial>().Add(stockBookMaterial);
			_dbcontext.SaveChanges();
			return;
		}

	}
}

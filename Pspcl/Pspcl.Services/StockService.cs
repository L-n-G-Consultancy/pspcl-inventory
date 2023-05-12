
using Microsoft.EntityFrameworkCore;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;

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

		//public List<Stock> GetAllMakes()
		//{
		//	return _dbcontext.Stock.ToList();
		//}



		public List<string> GetCircleAndDivision(int selectedSubDivId, bool? onlyActive = false)
		{
			if (onlyActive.HasValue)
			{
				SubDivision subDivision = _dbcontext.SubDivision.FirstOrDefault(x => x.Id == selectedSubDivId);			
				if (subDivision != null)
				{
					int divId = subDivision.DivisionId;
					Division Division = _dbcontext.Division.FirstOrDefault(x => x.Id == divId);
					string divisionName = Division.Name.ToString();

					int circleDiv = Division.CircleId;
					Circle Circle = _dbcontext.Circle.FirstOrDefault(x => x.Id == circleDiv);
					string circleName = Circle.Name.ToString();

					List<string> DivisionCircle = new List<string>();
					DivisionCircle.Add(divisionName);
					DivisionCircle.Add(circleName);

					return DivisionCircle;
				}
			}
			return new List<string>();
		}

		public List<Stock> GetAvailableQuantity(List<int> Ids)
		{
			List<Stock> stocks = (List<Stock>)_dbcontext.Stock.Where(x => x.MaterialGroupId == Ids[0] && x.MaterialTypeId== Ids[1] && x.MaterialId == Ids[2]).Select(x=>x);
			List<StockMaterial> stockMaterials = new List<StockMaterial>();
			foreach (Stock stock in stocks)
			{
				stockMaterials = _dbcontext.StockMaterial.Where(x => x.StockId == stock.Id).ToList();
			}
			List<StockMaterialSeries> series = new List<StockMaterialSeries>();
			foreach(StockMaterial stockMaterial in stockMaterials)
			{
				series= _dbcontext.StockMaterialSeries.Where(x => x.StockMaterialId == stockMaterial.Id).ToList();
			}

			return stocks;
		}
		public int AddStock(Stock stock)
		{
			_dbcontext.Set<Stock>().Add(stock);
			_dbcontext.SaveChanges();
			return stock.Id;
		}

		public void AddStockMaterial(StockMaterial stockMaterial)
		{
			_dbcontext.Set<StockMaterial>().Add(stockMaterial);
			_dbcontext.SaveChanges();
		}

		public int GetMaterialByType(int? typeId, string materialCode)
		{
			var material = _dbcontext.Set<Material>()
				.FirstOrDefault(m => m.MaterialTypeId == typeId);

			return material.Id;
		}

	}
}

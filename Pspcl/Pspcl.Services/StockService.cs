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
			return _dbcontext.MaterialGroup.Where(x => !onlyActive.HasValue || ((onlyActive.Value && x.IsActive) || (!onlyActive.Value && !x.IsActive))).ToList();
		}

		public List<MaterialType> GetAllMaterialTypes(int? materialGroupId, bool? onlyActive)
		{
			List<MaterialType> materialTypes = null;
			if (!materialGroupId.HasValue && !onlyActive.HasValue)
			{
				materialTypes = _dbcontext.MaterialType.ToList();
			}
			else if (!onlyActive.HasValue && materialGroupId.HasValue)
			{
				materialTypes = _dbcontext.MaterialType.Where(x => x.MaterialGroupId == materialGroupId).ToList();
			}
			else if (onlyActive.HasValue && !materialGroupId.HasValue)
			{
				materialTypes= _dbcontext.MaterialType.Where(x=>(onlyActive.Value && x.IsActive) || (!onlyActive.Value && !x.IsActive)).ToList();
			}
			else if (onlyActive.HasValue && materialGroupId.HasValue)
			{
				var response = _dbcontext.MaterialType.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value && !x.IsActive)).ToList();
				materialTypes = response.Where(x => x.MaterialGroupId == materialGroupId).ToList();
			}

			return materialTypes;
		}





		public List<MaterialType> GetAllMaterialRatings(int? materialTypeId, bool? onlyActive)
		{
			var rating = _dbcontext.MaterialType.Where(x => x.IsActive == true && x.Id==materialTypeId).ToList();
			return rating;
		}
	}
}

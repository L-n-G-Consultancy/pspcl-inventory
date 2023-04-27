using Pspcl.Core.Domain;
using Pspcl.DBConnect;

namespace Pspcl.Services
{
    public interface IStockService
    {
		List<MaterialGroup> GetAllMaterialGroups(bool? showHidden);
        //List<MaterialType> GetAllMaterialTypes(int? materialGroupId, bool? showHidden);
        List<String> GetAllMaterialRatings(int? materialGroupId, int? materialTypeId, bool? showHidden);


	}
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _dbcontext;
        public StockService(ApplicationDbContext dbContext) {
            _dbcontext = dbContext;
        }

        public List<MaterialGroup> GetAllMaterialGroups(bool? showHidden)
        {
            List<MaterialGroup> materialGroup = _dbcontext.MaterialGroup.ToList();
			if (showHidden==false)
            {
				materialGroup = _dbcontext.MaterialGroup.Where(x => x.IsActive == true).ToList();
                return materialGroup;
			}else if (showHidden==true)
            {
				materialGroup = _dbcontext.MaterialGroup.ToList();
                return materialGroup;
            }
            return materialGroup;
		}

		//public List<MaterialType> GetAllMaterialTypes(int? materialGroupId, bool? showHidden)
		//{
  //          List<MaterialType> materialType = _dbcontext.MaterialType.Where(x => x.MaterialGroupId).ToList();
  //          if(showHidden==false)
  //          {
		//		materialType = _dbcontext.MaterialType.Where(x => x.IsActive == true && x.MaterialGroupId==materialGroupId).ToList();
  //              return materialType;
		//	}
		//	else if (showHidden == true)
		//	{
		//		materialType = _dbcontext.MaterialType.Where(x=>x.MaterialGroupId).ToList();
		//		return materialType;
		//	}
		//	return materialType;
		//}

        public List<String> GetAllMaterialRatings(int? materialGroupId, int? materialTypeId, bool? showHidden)
        {
            List<MaterialType> rating = _dbcontext.MaterialType.Where(x=>x.IsActive==true).ToList();
            var response = rating.Select(x => x.Rating).ToList();
            Console.WriteLine(response[0]);
			return response;
        }
	}
}

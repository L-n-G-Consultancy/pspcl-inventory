using Pspcl.Core.Domain;

namespace Pspcl.Services.Interfaces
{
    public interface IStockService
    {
        List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false);
        List<MaterialType> GetAllMaterialTypes(int materialGroupId, bool? onlyActive = false);
        List<MaterialType> GetAllMaterialRatings(int materialTypeId, bool? onlyActive = false);
        List<Material> GetAllMaterialCodes(int materialTypeId, bool? onlyActive = false);
		List<SubDivision> GetAllSubDivisions(bool? onlyActive = false);
		List<string> GetCircleAndDivision(int selectedSubDivId, bool? onlyActive = false);
		//List<Stock> GetAllMakes();

		List<Stock> GetAvailableQuantity(List<int> Ids);

		int AddStock(Stock stock);
		void AddStockMaterial(StockMaterial stockMaterial);
		int GetMaterialByType(int? typeId, string materialCode);
	}
}

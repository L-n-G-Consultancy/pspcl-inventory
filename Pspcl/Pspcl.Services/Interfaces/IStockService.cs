using Pspcl.Core.Domain;
using Pspcl.Services.Models;

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
		List<string> GetAllMakes(int materialGrooupId, int materialTypeId, int materialId);
		List<List<int>> GetAvailableQuantity(List<int> Ids);

        void UpdateStockMaterialSeries(List<List<int>> requiredIssueData);
		int IssueStock(StockIssueBook stockIssueBook);
		void StockBookMaterial(StockBookMaterial stockBookMaterial, int id);


		int AddStock(Stock stock);
		int AddStockMaterial(StockMaterial stockMaterial);
        void AddStockMaterialSeries(StockMaterialSeries stockMaterialSeries);
        List<StockInModel> GetStockInModels();
        string GetMaterialGroupById(int? materialGroupId);
        string GetMaterialTypeById(int? materialTypeId);
        string GetMaterialCodeById(int? materialCodeId);
        string GetRatingNameById(int? materialTypeId);
        public string GetCorrespondingMakeValue(string invoiceNumber);
    }
}

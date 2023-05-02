using Pspcl.Core.Domain;

namespace Pspcl.Services.Interfaces
{
    public interface IStockService
    {
        List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false);
        List<MaterialType> GetAllMaterialTypes(int materialGroupId, bool? onlyActive = false);
        List<MaterialType> GetAllMaterialRatings(int materialTypeId, bool? onlyActive = false);
        List<Material> GetAllMaterialCodes(int materialTypeId, bool? onlyActive = false);
    }
}

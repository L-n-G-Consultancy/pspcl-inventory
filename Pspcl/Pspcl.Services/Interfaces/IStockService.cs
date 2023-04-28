using Pspcl.Core.Domain;

namespace Pspcl.Services.Interfaces
{
	public interface IStockService
	{
		List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false);
		List<MaterialType> GetAllMaterialTypes(int? materialGroupId, bool? showHidden);
		List<MaterialType> GetAllMaterialRatings(int? materialTypeId, bool? onlyActive);

	}
}

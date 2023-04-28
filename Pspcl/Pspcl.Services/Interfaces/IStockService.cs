using Pspcl.Core.Domain;

namespace Pspcl.Services.Interfaces
{
	public interface IStockService
	{
		List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false);
		List<MaterialType> GetAllMaterialTypes(int? materialGroupId, bool? showHidden=false);
		List<MaterialType> GetAllMaterialRatings(int? materialTypeId, bool? onlyActive = false);

	}
}

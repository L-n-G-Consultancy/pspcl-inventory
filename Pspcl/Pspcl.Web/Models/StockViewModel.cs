using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;

namespace Pspcl.Web.Models
{
	public class StockViewModel
	{
		public StockViewModel()
		{
			AvailableMaterialGroups = new List<SelectListItem>();
			AvailableMaterialTypes = new List<SelectListItem>();
			AvailableRatings = new List<SelectListItem>();
			AvailableMaterialCodes = new List<SelectListItem>();
		}

        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? MaterialGroupId { get; set; }
        public int? MaterialTypeId { get; set; }
        public string? Rating { get; set; }
        public string? SelectedMaterialCode { get; set; }
        public DateTime? GrnDate { get; set; }
        public long? GrnNumber { get; set; }
        public String? TestReportReference { get; set; }
        public decimal? EnterRate { get; set; }
        public string? PrefixNumber { get; set; }
        public IList<SelectListItem>? AvailableMaterialCodes { get; set; }
		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		public IList<SelectListItem>? AvailableRatings { get; set; }
		public List<StockMaterial> stockMaterialList { get; set; }
	}
}
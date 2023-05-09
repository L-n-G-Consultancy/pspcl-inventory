using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Pspcl.Core.Domain;

namespace Pspcl.Web.Models
{
	//[DataContract]
	//[Serializable]
	public class StockViewModel
	{
		public StockViewModel()
		{
			AvailableMaterialGroups = new List<SelectListItem>();
			AvailableMaterialTypes = new List<SelectListItem>();
			AvailableRatings = new List<SelectListItem>();
			AvailableMaterialCodes = new List<SelectListItem>();
		}
		public DateTime? GrnDate { get; set; }
		public long? GrnNumber { get; set; }
		public String? TestReportReference { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public string? InvoiceNumber { get; set; }
		public IList<SelectListItem>? AvailableMaterialCodes { get; set; }
		public int? SelectedMaterialCode { get; set; }
		public string SelectedMaterialCodeName { get; set; }
		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		public int? MaterialGroupId { get; set; }
        public string SelectedMaterialGroupName { get; set; }

        public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		public int? MaterialTypeId { get; set; }
        public string SelectedMaterialTypeName { get; set; }

        public IList<SelectListItem>? AvailableRatings { get; set; }
		public string? Rating { get; set; }
        public string SelectedRatingName { get; set; }
        public decimal? EnterRate { get; set; }
		public string? PrefixNumber { get; set; }
		public List<StockMaterial> stockMaterialList { get; set; }
	}
}
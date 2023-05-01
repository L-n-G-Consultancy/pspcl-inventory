using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
		public string? SelectedMaterialGroup { get; set; }
		public DateTime? GrnDate { get; set; }
		public long? GrnNumber { get; set; }
		public String? TestReportReference { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public string? InvoiceNumber { get; set; }
		public IList<SelectListItem>? AvailableMaterialCodes{ get; set; }
		public string? SelectedMaterialCode { get; set; }
		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		public string? SelectedMaterialType { get; set; }
		public IList<SelectListItem>? AvailableRatings { get; set; }
		public string? SelectedRating { get; set; }
		public decimal? EnterRate { get; set; }
		public string? PrefixNumber { get; set; }
		public List<StockMaterial> stockMaterialLList { get; set; }
	}
}

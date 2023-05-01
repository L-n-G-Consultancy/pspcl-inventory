using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;
namespace Pspcl.Web.Models
{
	[DataContract]
	[Serializable]
	public class StockViewModel
	{
		public StockViewModel()
		{
			AvailableMaterialGroups = new List<SelectListItem>();
			AvailableMaterialTypes = new List<SelectListItem>();
			AvailableRatings = new List<SelectListItem>();
			AvailableMaterialCodes = new List<SelectListItem>();
		}
		[DataMember]
		public DateTime? GrnDate { get; set; }
		[DataMember]
		public long? GrnNumber { get; set; }
		[DataMember]
		public String? TestReportReference { get; set; }
		[DataMember]
		public DateTime? InvoiceDate { get; set; }
		[DataMember]
		public string? InvoiceNumber { get; set; }
		[DataMember]
		public IList<SelectListItem>? AvailableMaterialCodes { get; set; }
		[DataMember]
		public string? SelectedMaterialCode { get; set; }
		[DataMember]
		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		[DataMember]
		public string? SelectedMaterialGroup { get; set; }
		[DataMember]
		public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		[DataMember]
		public string? SelectedMaterialType { get; set; }
		[DataMember]
		public IList<SelectListItem>? AvailableRatings { get; set; }
		[DataMember]
		public string? SelectedRating { get; set; }
		[DataMember]
		public decimal? EnterRate { get; set; }
		[DataMember]
		public string? PrefixNumber { get; set; }
		public List<StockMaterial> stockMaterialLList { get; set; }
	}
}
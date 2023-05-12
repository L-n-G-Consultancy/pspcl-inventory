using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Pspcl.Core.Domain;
using System.ComponentModel.DataAnnotations;


namespace Pspcl.Web.ViewModels
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
        ///[DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? GrnDate { get; set; }
		//[DataMember]
		public string? GrnNumber { get; set; }
		//[DataMember]
		public String? TestReportReference { get; set; }
        //[DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }
		//[DataMember]
		public string? InvoiceNumber { get; set; }
		//[DataMember]
		public IList<SelectListItem>? AvailableMaterialCodes { get; set; }
		//[DataMember]
		public int? MaterialIdByCode { get; set; }
		//[DataMember]
		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		//[DataMember]
		public int? MaterialGroupId { get; set; }
		//[DataMember]
		public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		//[DataMember]
		public int? MaterialTypeId { get; set; }
		//[DataMember]
		public IList<SelectListItem>? AvailableRatings { get; set; }
		//[DataMember]
		public string? Rating { get; set; }
        //[DataMember]
        public decimal? Rate { get; set; }
		//[DataMember]
		public string? PrefixNumber { get; set; }
		public string Make { get; set; }
		public List<StockMaterial> stockMaterialList { get; set; }
		public string SelectedMaterialGroupName { get; set; }
		public string SelectedMaterialTypeName { get; set;}
		public string SelectedMaterialCodeName { get; set;}
		public string SelectedRatingName { get; set; }

    }
}
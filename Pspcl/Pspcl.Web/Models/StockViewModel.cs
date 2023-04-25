using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

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
       
        public DateTime? GrnDate { get; set; }

  
        public long GrnNumber { get; set; }
        public String TestReportReference { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public IList<SelectListItem> AvailableMaterialCodes{ get; set; }
        public int SelectedMaterialCodeId { get; set; }
        public IList<SelectListItem> AvailableMaterialGroups { get; set; }
        public int SelectedMaterialGroupId { get; set; }
        public IList<SelectListItem> AvailableMaterialTypes { get; set; }
        public int SelectedMaterialTypeId { get; set; }
        public IList<SelectListItem> AvailableRatings { get; set; }
        public int SelectedRatingId { get; set; }
        public decimal EnterRate { get; set; }
        public string PrefixNumber { get; set; }

        public int SerialNumberFrom { get; set; }
        public int SerialNumberTo { get; set; }
        public int Quantity { get; set; }
        

    }
}

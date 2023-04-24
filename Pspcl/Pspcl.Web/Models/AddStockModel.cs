using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pspcl.Web.Models
{
    public class AddStockModel
    {

        public DateOnly GrnDate { get; set; }
        public long GrnNo { get; set; }
        public long TestReportReference { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public long InvoiceNo { get; set; }
        public string MaterialCode { get; set; }
        public IList<SelectListItem> AvailableMaterialGroup { get; set; }
        public IList<SelectListItem> AvailableMaterialType { get; set; }
        public IList<SelectListItem> AvailableRating { get; set; }
        public int EnterRate { get; set; }
        public string QtySNo { get; set; }
        public AddStockModel()
        {
            AvailableMaterialGroup = new List<SelectListItem>();
            AvailableMaterialType = new List<SelectListItem>();
            AvailableRating = new List<SelectListItem>();
        }

    }
}

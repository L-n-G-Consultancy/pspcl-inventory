using MessagePack;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;

namespace Pspcl.Web.Models
{
    public class IssueStockModel
    {
        public IssueStockModel() 
        {
            SubDivisionList = new List<SelectListItem>();

			AvailableMaterialGroups = new List<SelectListItem>();
			AvailableMaterialTypes = new List<SelectListItem>();
			AvailableMaterialCodes = new List<SelectListItem>();

		}
       // public int TxoDo { get; set; }
        public DateOnly CurrentDate { get; set; }
		public int SrNo { get; set; }
		public DateOnly Date { get; set; }
        public int SubDivisionId { get; set; }
		public string Division { get; set; }
		public string Circle { get; set; }
		public string JeName { get; set; }

		public string? SelectedMaterialCode { get; set; }
		public int MaterialTypeId { get; set; }
		public int MaterialGroupId { get; set; }
		public int Quantity { get; set; }
		public int SrNoFrom { get; set; }
		public int SrNoTo { get; set; }		
        public IList<SelectListItem>? SubDivisionList { get; set; }


		public IList<SelectListItem>? AvailableMaterialGroups { get; set; }
		public IList<SelectListItem>? AvailableMaterialTypes { get; set; }
		public IList<SelectListItem>? AvailableMaterialCodes { get; set; }


	}
}

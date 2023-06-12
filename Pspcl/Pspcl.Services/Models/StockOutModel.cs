using Pspcl.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Services.Models
{
	public class StockOutModel
	{
        public Stock Stock { get; set; }
        public string TxnOutID { get; set; }
        public string SrNo { get; set; }
        public DateTime SrNoDate { get; set; }
        public string DivName { get; set; }
        public string LocationCode { get; set; }
        public string SubDivName { get; set; }
        public string JEname { get; set; }
        public string MaterialType { get; set; }
        public string MaterialCode { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
    }
}

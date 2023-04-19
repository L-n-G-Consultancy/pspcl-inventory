﻿using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class StockIssueBook
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string SerialNumber { get; set; }
        //public Division Division { get; set; }
        public int DivisionId { get; set; } // Foreign key
        //public SubDivision SubDivision { get; set; }
        public int SubDivisionId { get; set; }  // Foreign key
        public Circle Circle { get; set; }
        public int CircleId { get; set; }  // Foreign key
        public string JuniorEngineerName { get; set; }

    }
}
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? UserId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Status { get; set; }
        public string? DocNo { get; set; }
    }
}

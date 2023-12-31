﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class TransactionViewModel
    {
        public int? TransactionId { get; set; }
        public int? UserId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Status { get; set; }
        public string? DocNo { get; set; }
    }
}

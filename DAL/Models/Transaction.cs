using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class Transaction
    {
        [Key]
        [Column("transactionId")]
        public int TransactionId { get; set; }
        [Column("vnPayId")]
        [StringLength(255)]
        public string? VnPayId { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("transactionType")]
        [StringLength(255)]
        public string? TransactionType { get; set; }
        [Column("value")]
        public int? Value { get; set; }
        [Column("transactionDate", TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column("paymentDate", TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        [Column("status")]
        [StringLength(255)]
        public string? Status { get; set; }
        [Column("docNo")]
        [StringLength(255)]
        public string? DocNo { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Transactions))]
        public virtual User? UserDetail { get; set; }
    }
}

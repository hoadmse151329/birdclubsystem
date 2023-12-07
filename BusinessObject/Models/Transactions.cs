using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Transactions
    {
        public int transactionId { get; set; }
        public int? userId { get; set; }
        public string? transactionType { get; set; }
        public double? value { get; set; }
        public DateTime? paymentDate { get; set; }
        public DateTime? transactionDate { get; set; }
        public string? status { get; set; }
        public string? docNo { get; set; }
    }
}

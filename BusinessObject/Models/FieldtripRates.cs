using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldtripRates
    {
        public int tripId { get; set; }
        public int rateId { get; set; }
        public string rateType { get; set; } = null!;
        public double price { get; set; }
    }
}

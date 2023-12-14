using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripInclusionViewModel
    {
        public int? TripId { get; set; }
        public int? InclusionId { get; set; }
        public string? Title { get; set; }
        public string? InclusionText { get; set; }
        public string? Type { get; set; }
    }
}

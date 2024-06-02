using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripAssignmentViewModel
    {
        public string? MemberId { get; set; }
        public int? TripId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? Role { get; set; }
    }
}

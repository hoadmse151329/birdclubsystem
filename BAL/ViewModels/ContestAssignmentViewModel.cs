using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class ContestAssignmentViewModel
    {
        public string? MemberId { get; set; }
        public int? ContestId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? Role { get; set; }
    }
}

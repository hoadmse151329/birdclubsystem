using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingAssignmentViewModel
    {
        public string? MemberId { get; set; }
        public int? MeetingId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? Role { get; set; }
    }
}

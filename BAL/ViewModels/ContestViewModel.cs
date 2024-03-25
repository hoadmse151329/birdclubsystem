using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class ContestViewModel
    {
        public int? ContestId { get; set; }
        public string? ContestName { get; set; }
        public string? Description { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public string? Address { get; set; }
        public int? AreaNumber { get; set; }
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BeforeScore { get; set; }
        public int? AfterScore { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Prize { get; set; }
        public string? Host { get; set; }
        public string? Incharge { get; set; }
        public string? Note { get; set; }
        public string? Review { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfParticipantsLimit { get; set; }
        public int? ClubId { get; set; }
    }
}

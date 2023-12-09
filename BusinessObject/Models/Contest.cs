using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Contest
    {
        public Contest()
        {
            ContestMedia = new HashSet<ContestMedia>();
        }

        public int ContestId { get; set; }
        public string ContestName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public int? LocationId { get; set; }
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
        public int? ClubId { get; set; }

        public virtual ICollection<ContestMedia> ContestMedia { get; set; }
    }
}

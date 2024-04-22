using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format:\nArea Number,Street,District,City")]
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
        public int? ParticipationNo { get; set; }
        public int? ClubId { get; set; }

        public List<ContestMediaViewModel>? Media { get; set; }
    }
}

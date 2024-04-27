using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingViewModel
    {
        public int? MeetingId { get; set; }
        public string? MeetingName { get; set; }
        public string? Description { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime? RegistrationDeadline { get; set; }
		[DataType(DataType.Date)]
		public DateTime? StartDate { get; set; }
        [Required]
		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; }
        public int? NumberOfParticipants { get; set; }
        [Range(3,int.MaxValue,ErrorMessage = "Number of Participants Limit must be at least three people")]
        public int? NumberOfParticipantsLimit { get; set; }
        public int? ParticipationNo { get; set; }
        public string? Host { get; set; }
        public string? Incharge { get; set; }
        public string? Note { get; set; }
		[Required(ErrorMessage = "Address is required")]
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
		public string? Address { get; set; }
        public string? AreaNumber { get; set; }
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Status { get; set; }

        public List<MeetingMediaViewModel>? Media { get; set; }
    }
}

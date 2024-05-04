using BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldTripViewModel
    {
        public FieldTripViewModel()
        {
            RegistrationDeadline = DateTime.Now;
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now.AddDays(2);
            FieldtripGettingTheres = new FieldtripGettingThereViewModel();
            FieldtripAdditionalDetails = new List<FieldTripAdditionalDetailViewModel>();
            FieldtripPictures = new List<FieldtripMediaViewModel>();
            FieldtripInclusions = new List<FieldtripInclusionViewModel>();
            FieldtripDaybyDays = new List<FieldtripDaybyDayViewModel>();
        }
        public int? TripId { get; set; }
        [DisplayName("Fieldtrip Name")]
        public string? TripName { get; set; }
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Details")]
        public string? Details { get; set; }
        [Required(ErrorMessage = "Registration Deadline is required")]
        [DisplayName("Registration Deadline")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDeadline { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("Start Date")]
        [DateGreaterThan(comparisonProperty: "RegistrationDeadline", comparisonRange: 10, comparisonType: "Day", ErrorMessage = "Start Date must be greater than Registration Deadline at least 10 days")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        [DisplayName("End Date")]
        [DateGreaterThan(comparisonProperty: "StartDate", comparisonRange: 1, comparisonType: "Day", ErrorMessage = "End Date must be greater than Start Date at least 1 day")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        [RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
        public string? Address { get; set; }
        public int? AreaNumber { get; set; }
        public string? Street {  get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; }
        [DisplayName("Number Of Participants")]
        public int? NumberOfParticipants { get; set; }
        [DisplayName("Maximum Number Of Participants")]
        [Range(3, int.MaxValue, ErrorMessage = "Maximum Number Of Participants must be at least three people")]
        public int? NumberOfParticipantsLimit { get; set; }
        [DisplayName("Fee")]
        [Range(0, int.MaxValue, ErrorMessage = "Fee must be at least 0")]
        public decimal? Fee { get; set; }
        [DisplayName("Participant Number")]
        public int? ParticipationNo { get; set; }
        [DisplayName("Host")]
        public string? Host { get; set; }
        [DisplayName("InCharge")]
        public string? InCharge { get; set; }
        [DisplayName("Note")]
        public string? Note { get; set; }
        [DisplayName("Location Map Image")]
        public FieldtripMediaViewModel? LocationMapImage { get; set; }
        [DisplayName("Spotlight Image")]
        public FieldtripMediaViewModel? SpotlightImage { get; set; }

        public FieldtripGettingThereViewModel FieldtripGettingTheres { get; set; }
        public List<FieldTripAdditionalDetailViewModel> FieldtripAdditionalDetails { get; set; }
        public List<FieldtripMediaViewModel> FieldtripPictures { get; set; }
        public List<FieldtripInclusionViewModel> FieldtripInclusions { get; set; }
        public List<FieldtripDaybyDayViewModel> FieldtripDaybyDays { get; set; }
    }
}

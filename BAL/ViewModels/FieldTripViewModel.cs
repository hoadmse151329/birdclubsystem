using System;
using System.Collections.Generic;
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

            FieldtripGettingTheres = new FieldtripGettingThereViewModel();
            FieldtripAdditionalDetails = new List<FieldTripAdditionalDetailViewModel>();
            FieldtripPictures = new List<FieldtripMediaViewModel>();
            FieldtripInclusions = new List<FieldtripInclusionViewModel>();
            FieldtripDaybyDays = new List<FieldtripDaybyDayViewModel>();
        }
        public int? TripId { get; set; }
        public string? TripName { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format:\nArea Number,Street,District,City")]
		public string? Address { get; set; }
        public int? AreaNumber { get; set; }
        public string? Street {  get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Status { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfParticipantsLimit { get; set; }
        public decimal? Fee { get; set; }
        public int? ParticipationNo { get; set; }
        public string? Host { get; set; }
        public string? InCharge { get; set; }
        public string? Note { get; set; }
        public FieldtripMediaViewModel? LocationMapImage { get; set; }
        public FieldtripMediaViewModel? SpotlightImage { get; set; }

        public FieldtripGettingThereViewModel FieldtripGettingTheres { get; set; }
        public List<FieldTripAdditionalDetailViewModel> FieldtripAdditionalDetails { get; set; }
        public List<FieldtripMediaViewModel> FieldtripPictures { get; set; }
        public List<FieldtripInclusionViewModel> FieldtripInclusions { get; set; }
        public List<FieldtripDaybyDayViewModel> FieldtripDaybyDays { get; set; }
    }
}

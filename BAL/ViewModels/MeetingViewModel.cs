﻿using BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingViewModel
    {
        public MeetingViewModel()
        {
            RegistrationDeadline = DateTime.Now;
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now.AddDays(2);
            Status = "OnHold";
        }
        public int? MeetingId { get; set; }
        [Required(ErrorMessage = "Meeting Name is required")]
        [DisplayName("Meeting Name")]
        public string? MeetingName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
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
        [DateGreaterThan(comparisonProperty: "StartDate", ErrorMessage = "End Date must be greater than Start Date")]
        [DataType(DataType.Date)]
		public DateTime EndDate { get; set; }
        [DisplayName("Number Of Participants")]
        public int? NumberOfParticipants { get; set; }
        [Required(ErrorMessage = "Maximum Participants is required")]
        [DisplayName("Maximum Participants")]
        [Range(3,int.MaxValue,ErrorMessage = "Maximum Participants value must be at least three people")]
        public int? NumberOfParticipantsLimit { get; set; }
        [DisplayName("Participant Number")]
        public int? ParticipationNo { get; set; }
        [Required(ErrorMessage = "Host is required")]
        [DisplayName("Host")]
        public string? Host { get; set; }
        [Required(ErrorMessage = "Incharge is required")]
        [DisplayName("Incharge")]
        public string? Incharge { get; set; }
        [Required(ErrorMessage = "Note is required")]
        [DisplayName("Note")]
        public string? Note { get; set; }
		[Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        [RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
		public string? Address { get; set; }
        public string? AreaNumber { get; set; }
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }

        public List<MeetingMediaViewModel>? Media { get; set; }
    }
}

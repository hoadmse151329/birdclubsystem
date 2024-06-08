﻿using BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAL.ViewModels.Manager
{
    public class UpdateMeetingDetailsVM
    {
        public UpdateMeetingDetailsVM()
        {
            Review = "No Feedback";
            OpenRegistration = DateTime.Now.AddDays(1);
            RegistrationDeadline = DateTime.Now.AddDays(11);
            StartDate = DateTime.Now.AddDays(21);
            EndDate = DateTime.Now.AddDays(22);
            Status = "OnHold";
            NumberOfParticipants = 0;
            NumberOfParticipantsMinReq = 10;
            NumberOfParticipantsLimit = 200;
        }
        public int? MeetingId { get; set; }
        [Required(ErrorMessage = "Meeting Name is required")]
        [DisplayName("Meeting Name")]
        public string? MeetingName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Open Registration Date is required")]
        [DisplayName("Open Registration Date")]
        [DataType(DataType.DateTime)]
        public DateTime OpenRegistration { get; set; }
        [Required(ErrorMessage = "Registration Deadline is required")]
        [DisplayName("Registration Deadline")]
        [DateGreaterThan(comparisonProperty: "OpenRegistration", comparisonRange: 10, comparisonType: "Day", ErrorMessage = "Registration Deadline Date must be greater than Open Registration at least 10 days")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDeadline { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("Start Date")]
        [DateGreaterThan(comparisonProperty: "RegistrationDeadline", comparisonRange: 10, comparisonType: "Day", ErrorMessage = "Start Date must be greater than Registration Deadline at least 10 days")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        [DisplayName("End Date")]
        [DateGreaterThan(comparisonProperty: "StartDate", comparisonRange: 1, comparisonType: "Day", ErrorMessage = "End Date must be greater than Start Date at least 1 day")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [DisplayName("Number Of Participants")]
        public int NumberOfParticipants { get; set; }
        [DisplayName("Minimum Participants")]
        public int NumberOfParticipantsMinReq { get; set; }
        [Required(ErrorMessage = "Maximum Participants is required")]
        [DisplayName("Maximum Participants")]
        [Range(10, 200, ErrorMessage = "Maximum Participants value must be at least 10 people and less than 200 people")]
        public int NumberOfParticipantsLimit { get; set; }
        [DisplayName("Review")]
        public string? Review { get; set; }
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
        [RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s,?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public List<SelectListItem>? MeetingStaffNames { get; set; }
        public List<SelectListItem>? MeetingStatusSelectableList { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldTripViewModel
    {
        public int? TripId { get; set; }
        public string? TripName { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Address { get; set; }
        public int? AreaNumber { get; set; }
        public string? Street {  get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Status { get; set; }
        public decimal? Fee { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfParticipantsLimit { get; set; }
        public int? ParticipationNo { get; set; }
        public string? Host { get; set; }
        public string? InCharge { get; set; }
        public string? Note { get; set; }
        public string? Review { get; set; }

        public List<FieldtripGettingThereViewModel>? GettingThere { get; set; }
        public List<FieldtripRateViewModel>? Rates { get; set; }
        public List<FieldtripMediaViewModel>? Media { get; set; }
        public List<FieldtripInclusionViewModel>? Inclusions { get; set; }
        public List<FieldtripDaybyDayViewModel>? DaybyDays { get; set; }
    }
}

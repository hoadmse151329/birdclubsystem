using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class GetEventParticipation
    {
        public int? EventId { get; set; }
        public string? EventIdFull { get; set; }
        public string? EventName { get; set; }
        public string? EventType { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EventHost { get; set; }
        public string? EventStaff {  get; set; }
        public int? ParticipationNo { get; set; }
        public decimal? Fee { get; set; }
        public string? Status { get; set; }
        public string? CheckInStatus { get; set; }
    }
}

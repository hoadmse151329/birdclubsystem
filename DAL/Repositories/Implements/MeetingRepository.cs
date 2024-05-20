using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class MeetingRepository : RepositoryBase<Meeting>, IMeetingRepository
    {
        private readonly BirdClubContext _context;
        public MeetingRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Meeting> GetAllByRegistrationDeadline(DateTime registrationDeadline)
        {
            return _context.Meetings.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline).ToList();
        }

        public IEnumerable<string> GetAllMeetingName()
        {
            return _context.Meetings.AsNoTracking()
                .Select(m => m.MeetingName)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Meeting> GetSortedMeetings(
            int? meetingId = null,
            string? meetingName = null,
            DateTime? registrationDeadline = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? numberOfParticipants = null,
            List<string>? roads = null,
            List<string>? districts = null,
            List<string>? cities = null,
            List<string>? statuses = null,
            string? orderBy = null,
            bool isMemberOrGuest = false
            )
        {
            var roadLocationIds = roads != null && roads.Any() ? GetLocationIdListByLocationName(roads).ToList() : new List<int>();
            var districtLocationIds = districts != null && districts.Any() ? GetLocationIdListByLocationName(districts).ToList() : new List<int>();
            var cityLocationIds = cities != null && cities.Any() ? GetLocationIdListByLocationName(cities).ToList() : new List<int>();

            var meetings = _context.Meetings.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "OnHold", "OpenRegistration", "ClosedRegistration", "CheckingIn", "Ongoing", "Ended", "Postponed", "Cancelled" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "OpenRegistration" };
            }

            if (meetingId.HasValue)
            {
                meetings = meetings.AsNoTracking().Where(m => m.MeetingId.Equals(meetingId.Value));
            }

            if (!string.IsNullOrEmpty(meetingName))
            {
                meetings = meetings.AsNoTracking().Where(m => m.MeetingName.Contains(meetingName));
            }

            if (registrationDeadline.HasValue)
            {
                meetings = meetings.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline.Value);
            }
            if (startDate.HasValue)
            {
                meetings = meetings.AsNoTracking().Where(m => m.StartDate == startDate.Value);
            }
            if (endDate.HasValue)
            {
                meetings = meetings.AsNoTracking().Where(m => m.EndDate == endDate.Value);
            }
            if (numberOfParticipants.HasValue)
            {
                meetings = meetings.AsNoTracking().Where(m => m.NumberOfParticipants == numberOfParticipants.Value);
            }
            if (roadLocationIds.Any())
            {
                meetings = meetings.Where(m => roadLocationIds.Contains(m.LocationId.Value));
            }
            if (districtLocationIds.Any())
            {
                meetings = meetings.Where(m => districtLocationIds.Contains(m.LocationId.Value));
            }

            if (cityLocationIds.Any())
            {
                meetings = meetings.Where(m => cityLocationIds.Contains(m.LocationId.Value));
            }
            if (statuses != null && statuses.Any())
            {
                meetings = meetings.Where(m => statuses.Contains(m.Status));
            }
            meetings = orderBy switch
            {
                "meetingname_asc" => meetings.OrderBy(m => m.MeetingName),
                "meetingname_desc" => meetings.OrderByDescending(m => m.MeetingName),
                "openregistration_asc" => meetings.OrderBy(m => m.OpenRegistration),
                "openregistration_desc" => meetings.OrderByDescending(m => m.OpenRegistration),
                "registrationdeadline_asc" => meetings.OrderBy(m => m.RegistrationDeadline),
                "registrationdeadline_desc" => meetings.OrderByDescending(m => m.RegistrationDeadline),
                "startdate_asc" => meetings.OrderBy(m => m.StartDate),
                "startdate_desc" => meetings.OrderByDescending(m => m.StartDate),
                "enddate_asc" => meetings.OrderBy(m => m.EndDate),
                "enddate_desc" => meetings.OrderByDescending(m => m.EndDate),
                "status_asc" => meetings.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => meetings.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => meetings.OrderBy(m => m.MeetingId)
            };

            return meetings.ToList();
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetings(string? role)
        {
            if (role == "Manager" || role == "Staff")
            {
                return _context.Meetings.AsNoTracking().ToList();
            }
            else return _context.Meetings.AsNoTracking().Where(meet => meet.Status == "OpenRegistration").ToList();
        }

        public async Task<Meeting?> GetMeetingById(int id)
        {
            return _context.Meetings.AsNoTracking().SingleOrDefault(meet => meet.MeetingId == id);
        }

        public async Task<bool> GetBoolMeetingId(int id)
        {
            var meet = _context.Meetings.SingleOrDefault(m => m.MeetingId == id);
            if (meet != null) return true;
            else return false;
        }

        private List<int> GetLocationIdListByLocationName(List<string>? locationNames)
        {
            var nameLocationList = new List<Location>();
            foreach (var locationName in locationNames)
            {
                var list = _context.Locations.AsNoTracking().Where(l => l.LocationName.Contains(locationName)).ToList();
                nameLocationList.AddRange(list);
            }
            return nameLocationList.Select(l => l.LocationId).ToList();
        }
    }
}

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
    public class FieldTripRepository : RepositoryBase<FieldTrip>, IFieldTripRepository
    {
        private readonly BirdClubContext _context;
        public FieldTripRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FieldTrip>> GetAllFieldTrips(string? role)
        {
            if (role == "Manager" || role == "Staff")
            {
                return _context.FieldTrips.AsNoTracking()
                .Include(f => f.FieldtripPictures.Where(fm => fm.Type.Equals("Spotlight")))
                .ToList();
            }
            return _context.FieldTrips.AsNoTracking()
                .Where(f => f.Status == "OpenRegistration")
                .Include(f => f.FieldtripPictures.Where(fm => fm.Type.Equals("Spotlight")))
                .ToList();
        }

        public IEnumerable<FieldTrip> GetSortedFieldTrips(
            int? tripId, 
            string? tripName, 
            DateTime? registrationDeadline, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? numberOfParticipants, 
            List<string>? roads, 
            List<string>? districts, 
            List<string>? cities, 
            List<string>? statuses, 
            string? orderBy, 
            bool isMemberOrGuest = false)
        {
            var roadLocationIds = roads != null && roads.Any() ? GetLocationIdListByLocationName(roads).ToList() : new List<int>();
            var districtLocationIds = districts != null && districts.Any() ? GetLocationIdListByLocationName(districts).ToList() : new List<int>();
            var cityLocationIds = cities != null && cities.Any() ? GetLocationIdListByLocationName(cities).ToList() : new List<int>();

            var fieldtrips = _context.FieldTrips.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "OnHold", "OpenRegistration", "ClosedRegistration", "CheckingIn", "Ongoing", "Ended", "Postponed", "Cancelled" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "OpenRegistration" };
            }

            if (tripId.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.TripId.Equals(tripId.Value));
            }

            if (!string.IsNullOrEmpty(tripName))
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.TripName.Contains(tripName));
            }

            if (registrationDeadline.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline.Value);
            }
            if (startDate.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.StartDate == startDate.Value);
            }
            if (endDate.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.EndDate == endDate.Value);
            }
            if (numberOfParticipants.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => m.NumberOfParticipants == numberOfParticipants.Value);
            }
            if (roadLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => roadLocationIds.Contains(m.LocationId.Value));
            }
            if (districtLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => districtLocationIds.Contains(m.LocationId.Value));
            }

            if (cityLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => cityLocationIds.Contains(m.LocationId.Value));
            }
            if (statuses != null && statuses.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(m => statuses.Contains(m.Status));
            }
            fieldtrips = orderBy switch
            {
                "fieldtripname_asc" => fieldtrips.OrderBy(m => m.TripName),
                "fieldtripname_desc" => fieldtrips.OrderByDescending(m => m.TripName),
                "openregistration_asc" => fieldtrips.OrderBy(m => m.OpenRegistration),
                "openregistration_desc" => fieldtrips.OrderByDescending(m => m.OpenRegistration),
                "registrationdeadline_asc" => fieldtrips.OrderBy(m => m.RegistrationDeadline),
                "registrationdeadline_desc" => fieldtrips.OrderByDescending(m => m.RegistrationDeadline),
                "startdate_asc" => fieldtrips.OrderBy(m => m.StartDate),
                "startdate_desc" => fieldtrips.OrderByDescending(m => m.StartDate),
                "enddate_asc" => fieldtrips.OrderBy(m => m.EndDate),
                "enddate_desc" => fieldtrips.OrderByDescending(m => m.EndDate),
                "status_asc" => fieldtrips.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => fieldtrips.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => fieldtrips.OrderBy(m => m.TripId)
            };

            return fieldtrips.ToList();
        }

        public async Task<FieldTrip?> GetFieldTripById(int id)
        {
            return _context.FieldTrips.AsNoTracking()
                .Include(f => f.FieldtripDaybyDays.OrderBy(pic => pic.Day))
                .Include(f => f.FieldtripInclusions)
                .Include(f => f.FieldtripGettingTheres)
                .Include(f => f.FieldtripAdditionalDetails)
                .Include(f => f.FieldtripPictures)
                .SingleOrDefault(trip => trip.TripId == id);
        }
        public async Task<bool> GetBoolFieldTripId(int id)
        {
            var trip = _context.FieldTrips.SingleOrDefault(f => f.TripId == id);
            if (trip != null)
            {
                return true;
            }
            else return false;
        }

        public async Task<FieldTrip?> GetFieldTripByIdWithoutInclude(int id)
        {
            return _context.FieldTrips.AsNoTracking()
                .SingleOrDefault(trip => trip.TripId == id);
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

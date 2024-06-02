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
                return await _context.FieldTrips.AsNoTracking()
                .Include(f => f.FieldTripPictures.Where(fm => fm.Type.Equals("Spotlight")))
                .ToListAsync();
            }
            return await _context.FieldTrips.AsNoTracking()
                .Where(f => f.Status == "OpenRegistration")
                .Include(f => f.FieldTripPictures.Where(fm => fm.Type.Equals("Spotlight")))
                .ToListAsync();
        }

        public IEnumerable<FieldTrip> GetSortedFieldTrips(
            int? tripId = null, 
            string? tripName = null, 
            DateTime? openRegistration = null, 
            DateTime? registrationDeadline = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            int? numberOfParticipants = null, 
            List<string>? roads = null, 
            List<string>? districts = null, 
            List<string>? cities = null, 
            List<string>? statuses = null, 
            string? orderBy = null, 
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
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.TripId.Equals(tripId.Value));
            }

            if (!string.IsNullOrEmpty(tripName))
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.TripName.Contains(tripName));
            }

            if (openRegistration.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.OpenRegistration.Value.Date.Equals(openRegistration.Value.Date));
            }

            if (registrationDeadline.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.RegistrationDeadline.Value.Date.Equals(registrationDeadline.Value.Date));
            }
            if (startDate.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.StartDate.Value.Date.Equals(startDate.Value.Date));
            }
            if (endDate.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.EndDate.Value.Date.Equals(endDate.Value.Date));
            }
            if (numberOfParticipants.HasValue)
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => f.NumberOfParticipants == numberOfParticipants.Value);
            }
            if (roadLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => roadLocationIds.Contains(f.LocationId.Value));
            }
            if (districtLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => districtLocationIds.Contains(f.LocationId.Value));
            }

            if (cityLocationIds.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => cityLocationIds.Contains(f.LocationId.Value));
            }
            if (statuses != null && statuses.Any())
            {
                fieldtrips = fieldtrips.AsNoTracking().Where(f => statuses.Contains(f.Status));
            }
            fieldtrips = orderBy switch
            {
                "fieldtripname_asc" => fieldtrips.OrderBy(f => f.TripName),
                "fieldtripname_desc" => fieldtrips.OrderByDescending(f => f.TripName),
                "openregistration_asc" => fieldtrips.OrderBy(f => f.OpenRegistration),
                "openregistration_desc" => fieldtrips.OrderByDescending(f => f.OpenRegistration),
                "registrationdeadline_asc" => fieldtrips.OrderBy(f => f.RegistrationDeadline),
                "registrationdeadline_desc" => fieldtrips.OrderByDescending(f => f.RegistrationDeadline),
                "startdate_asc" => fieldtrips.OrderBy(f => f.StartDate),
                "startdate_desc" => fieldtrips.OrderByDescending(f => f.StartDate),
                "enddate_asc" => fieldtrips.OrderBy(f => f.EndDate),
                "enddate_desc" => fieldtrips.OrderByDescending(f => f.EndDate),
                "status_asc" => fieldtrips.OrderBy(f => statusListDefault.IndexOf(f.Status)),
                "status_desc" => fieldtrips.OrderByDescending(f => statusListDefault.IndexOf(f.Status)),
                _ => fieldtrips.OrderBy(f => f.TripId)
            };

            return fieldtrips.ToList();
        }

        public async Task<FieldTrip?> GetFieldTripById(int id)
        {
            return _context.FieldTrips.AsNoTracking()
                .Include(f => f.FieldTripDaybyDays.OrderBy(pic => pic.Day))
                .Include(f => f.FieldTripInclusions)
                .Include(f => f.FieldTripGettingThereDetails)
                .Include(f => f.FieldTripAdditionalDetails)
                .Include(f => f.FieldTripPictures)
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
            return await _context.FieldTrips.AsNoTracking()
                .SingleOrDefaultAsync(trip => trip.TripId == id);
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
        public async Task<int> CountFieldTrip()
        {
            return _context.FieldTrips.AsNoTracking().Count();
        }

        public async Task<int> CountFieldTripByStatus(string status)
        {
            return _context.FieldTrips.AsNoTracking().Where(f => f.Status == status).Count();
        }
    }
}

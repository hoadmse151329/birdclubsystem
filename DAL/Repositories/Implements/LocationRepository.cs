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
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        private readonly BirdClubContext _context;
        public LocationRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string?>> GetAllLocationName()
        {
            return _context.Locations.AsNoTracking()
                .Select(m => m.LocationName)
                .Distinct()
                .ToList();
        }
        public async Task<Location?> GetLocationByName(string locationName)
        {
            return _context.Locations.AsNoTracking()
                .FirstOrDefault(m => m.LocationName.Equals(locationName));
        }

        public async Task<Location?> GetLocationByMeetingId(int meetingId)
        {
            var met = await _context.Meetings.AsNoTracking().SingleOrDefaultAsync(m => m.MeetingId.Equals(meetingId));

            return _context.Locations.AsNoTracking()
                .FirstOrDefault(m => m.LocationId.Equals(met.LocationId));
        }

        public async Task<Location?> GetLocationByTripId(int tripId)
        {
            var trip = await _context.FieldTrips.AsNoTracking().SingleOrDefaultAsync(f => f.TripId.Equals(tripId));

            return _context.Locations.AsNoTracking()
                .FirstOrDefault(f => f.LocationId.Equals(trip.LocationId));
        }

        public async Task<Location?> GetLocationByContestId(int contestId)
        {
            var con = await _context.Contests.AsNoTracking().SingleOrDefaultAsync(c => c.ContestId.Equals(contestId));

            return _context.Locations.AsNoTracking()
                .FirstOrDefault(c => c.LocationId.Equals(con.LocationId));
        }

        public async Task<string?> GetLocationNameById(int id)
        {
            return _context.Locations.AsNoTracking()
                .SingleOrDefaultAsync(m => m.LocationId.Equals(id)).Result
                .LocationName;
        }
    }
}

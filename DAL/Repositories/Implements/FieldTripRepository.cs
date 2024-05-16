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
    }
}

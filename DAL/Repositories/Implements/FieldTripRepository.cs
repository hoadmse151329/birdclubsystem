using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
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
        public async Task<IEnumerable<FieldTrip>> GetAllFieldTrips()
        {
            return _context.FieldTrips.ToList();
        }
        public async Task<FieldTrip?> GetFieldTripById(int id)
        {
            return _context.FieldTrips.SingleOrDefault(trip => trip.TripId == id);
        }
    }
}

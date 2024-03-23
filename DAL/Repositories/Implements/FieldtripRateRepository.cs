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
    public class FieldtripRateRepository : RepositoryBase<FieldtripRate>, IFieldtripRateRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripRateRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FieldtripRate> GetFieldTripRateById(int tripId, int rateId)
        {
            return _context.FieldtripRates.AsNoTracking().SingleOrDefault(f => f.TripId.Equals(tripId) && f.RateId.Equals( rateId));
        }

        public async Task<IEnumerable<FieldtripRate>> GetFieldTripRatesByTripId(int tripId)
        {
            return _context.FieldtripRates.AsNoTracking().Where(f => f.TripId.Equals(tripId));
        }
    }
}

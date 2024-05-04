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
    public class FIeldtripDaybyDayRepository : RepositoryBase<FieldtripDaybyDay>, IFieldtripDaybyDayRepository
    {
        private readonly BirdClubContext _context;
        public FIeldtripDaybyDayRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FieldtripDaybyDay>> GetAllFieldTripDayByDaysByTripId(int tripId)
        {
            return _context.FieldtripDaybyDays.AsNoTracking().Where(f => f.TripId.Equals(tripId));
        }

        public async Task<FieldtripDaybyDay> GetFieldTripDayByDayById(int tripId, int fieldtripId)
        {
            return _context.FieldtripDaybyDays.AsNoTracking().SingleOrDefault(f => f.TripId.Equals(tripId) && f.DayByDayId.Equals(fieldtripId));
        }
        public async Task<FieldtripDaybyDay> GetFieldTripDayByDayByIdTracking(int tripId, int fieldtripId)
        {
            return _context.FieldtripDaybyDays.SingleOrDefault(f => f.TripId.Equals(tripId) && f.DayByDayId.Equals(fieldtripId));
        }
    }
}

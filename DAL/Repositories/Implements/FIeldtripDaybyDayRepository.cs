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
            return await _context.FieldtripDaybyDays.AsNoTracking().Where(f => f.TripId.Equals(tripId)).ToListAsync();
        }

        public async Task<FieldtripDaybyDay> GetFieldTripDayByDayById(int dayId)
        {
            return await _context.FieldtripDaybyDays.AsNoTracking().SingleOrDefaultAsync(f =>  f.DayByDayId.Equals(dayId));
        }
        public async Task<FieldtripDaybyDay> GetFieldTripDayByDayByIdTracking(int dayId)
        {
            return await _context.FieldtripDaybyDays.SingleOrDefaultAsync(f =>  f.DayByDayId.Equals(dayId));
        }
    }
}

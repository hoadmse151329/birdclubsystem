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
    public class FieldtripInclusionRepository : RepositoryBase<FieldtripInclusion>, IFieldtripInclusionRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripInclusionRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FieldtripInclusion> GetFieldTripInclusionById(int incluId)
        {
            return await _context.FieldtripInclusions.AsNoTracking().SingleOrDefaultAsync(f => f.InclusionId.Equals(incluId));
        }

        public async Task<FieldtripInclusion> GetFieldTripInclusionByIdTracking(int incluId)
        {
            return await _context.FieldtripInclusions.SingleOrDefaultAsync(f => f.InclusionId.Equals(incluId));
        }

        public async Task<IEnumerable< FieldtripInclusion>> GetFieldTripInclusionsById(int tripId)
        {
            return await _context.FieldtripInclusions.AsNoTracking().Where(f => f.TripId.Equals(tripId)).ToListAsync();
        }
    }
}

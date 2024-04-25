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
    public class FieldtripAdditionalDetailRepository : RepositoryBase<FieldtripAdditionalDetail>, IFieldtripAdditionalDetailRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripAdditionalDetailRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<FieldtripAdditionalDetail> GetFieldTripAdditionalDetailById(int tripId, int addDetailId)
        {
            return await _context.FieldtripAdditionalDetails.AsNoTracking().SingleOrDefaultAsync(f => f.TripId.Equals(tripId) && f.TripDetailsId.Equals(addDetailId));
        }

        public async Task<IEnumerable<FieldtripAdditionalDetail>> GetFieldTripAdditionalDetailsByTripId(int tripId)
        {
            return _context.FieldtripAdditionalDetails.AsNoTracking().Where(f => f.TripId.Equals(tripId));
        }
    }
}

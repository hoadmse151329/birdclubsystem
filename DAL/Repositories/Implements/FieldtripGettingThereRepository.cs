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
    public class FieldtripGettingThereRepository : RepositoryBase<FieldtripGettingThere>, IFieldtripGettingThereRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripGettingThereRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<FieldtripGettingThere> GetFieldTripGettingTheresByTripId(int tripId)
        {
            return _context.FieldtripGettingTheres.AsNoTracking().SingleOrDefault(f => f.TripId.Equals(tripId));
        }
    }
}

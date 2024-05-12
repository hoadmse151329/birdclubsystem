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
    public class FieldtripMediaRepository : RepositoryBase<FieldtripMedia>, IFieldtripMediaRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripMediaRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FieldtripMedia> GetFieldTripMediaById(int tripId, int pictureId)
        {
            return _context.FieldtripMedia.AsNoTracking().SingleOrDefault(f => f.TripId.Equals(tripId) && f.PictureId.Equals(pictureId));
        }

        public async Task<FieldtripMedia> GetFieldTripMediaByIdTracking(int tripId, int pictureId)
        {
            return _context.FieldtripMedia.SingleOrDefault(f => f.TripId.Equals(tripId) && f.PictureId.Equals(pictureId));
        }

        public async Task<IEnumerable<FieldtripMedia>> GetFieldTripMediasByTripId(int tripId)
        {
            return _context.FieldtripMedia.AsNoTracking().Where(f => f.TripId.Equals(tripId)).ToList();
        }
    }
}

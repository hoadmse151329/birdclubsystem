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
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        private readonly BirdClubContext _context;
        public LocationRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string?>> GetAllLocationName()
        {
            return _context.Locations
                .Select(m => m.LocationName)
                .Distinct()
                .ToList();
        }

        public async Task<string?> GetLocationNameById(int id)
        {
            return _context.Locations
                .SingleOrDefault(m => m.LocationId.Equals(id))
                .LocationName;
        }
    }
}

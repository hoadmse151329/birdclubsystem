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
    public class GalleryRepository : RepositoryBase<Gallery>, IGalleryRepository
    {
        private readonly BirdClubContext _context;
        public GalleryRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gallery>> GetAllGalleries()
        {
            return await _context.Galleries.ToListAsync();
        }
    }
}

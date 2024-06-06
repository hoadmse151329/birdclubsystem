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
    public class ContestMediaRepository : RepositoryBase<ContestMedia>, IContestMediaRepository
    {
        private readonly BirdClubContext _context;
        public ContestMediaRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ContestMedia> GetContestMediaById(int pictureId)
        {
            return await _context.ContestMedia.AsNoTracking().SingleOrDefaultAsync(c => c.PictureId.Equals(pictureId));
        }

        public async Task<IEnumerable<ContestMedia>> GetContestMediasByContestId(int contestId)
        {
            return await _context.ContestMedia.AsNoTracking().Where(c => c.ContestId.Equals(contestId)).ToListAsync();
        }
        public async Task<ContestMedia> GetContestMediaByContestIdAndType(int contestId, string mediaType)
        {
            return await _context.ContestMedia.AsNoTracking().SingleOrDefaultAsync(m => m.ContestId.Equals(contestId) && m.Type.Equals(mediaType));
        }

        public async Task<ContestMedia> GetContestMediaByIdTracking(int pictureId)
        {
            return await _context.ContestMedia.SingleOrDefaultAsync(m => m.PictureId.Equals(pictureId));
        }
    }
}

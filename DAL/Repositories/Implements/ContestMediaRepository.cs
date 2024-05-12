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

        public async Task<ContestMedia> GetContestMediaById(int contestId, int pictureId)
        {
            return _context.ContestMedia.AsNoTracking().SingleOrDefault(c => c.ContestId.Equals(contestId) && c.PictureId.Equals(pictureId));
        }

        public async Task<IEnumerable<ContestMedia>> GetContestMediasByContestId(int contestId)
        {
            return _context.ContestMedia.AsNoTracking().Where(c => c.ContestId.Equals(contestId)).ToList();
        }
    }
}

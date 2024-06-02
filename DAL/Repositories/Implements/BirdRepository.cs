using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using DAL.Infrastructure;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class BirdRepository : RepositoryBase<Bird>, IBirdRepository
    {
        private readonly BirdClubContext _context;
        public BirdRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bird>> GetBirdsByMemberId(string memberId)
        {
            return await _context.Birds.Where(m => m.MemberId == memberId).OrderBy(b => b.BirdName).ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByMemberIdInclude(string memberId)
        {
            return await _context.Birds.Where(m => m.MemberId == memberId).Include(m => m.MemberDetails).ToListAsync();
        }

        public async Task<int> GetBirdIdByMemberId(string memberId)
        {
            var result = await (from mem in _context.Birds where mem.MemberId == memberId select mem).FirstOrDefaultAsync();
            if (result != null) return result.BirdId;
            return 0;
        }

        public async Task<int> GetELOByBirdId(int birdId)
        {
            var result = await (from bird in _context.Birds where bird.BirdId == birdId select bird).FirstOrDefaultAsync();
            if (result != null) return result.Elo.Value;
            return 0;
        }

        public async Task<Bird> GetBirdById(int birdId)
        {
            return await _context.Birds.AsNoTracking().SingleOrDefaultAsync(b => b.BirdId.Equals(birdId));
        }

        public async Task<Bird> GetBirdByIdTracking(int birdId)
        {
            return await _context.Birds.SingleOrDefaultAsync(b => b.BirdId.Equals(birdId));
        }

        public async Task<Bird> GetBirdByName(string birdName)
        {
            return await _context.Birds.AsNoTracking().SingleOrDefaultAsync(b => b.BirdName.Equals(birdName));
        }

        public async Task<Bird> GetBirdByNameTracking(string birdName)
        {
            return await _context.Birds.SingleOrDefaultAsync(b => b.BirdName.Equals(birdName));
        }

        public async Task<List<Bird>> GetBirdsOrderByElo()
        {
            return await _context.Birds.OrderByDescending(b => b.Elo).ToListAsync();
        }
    }
}

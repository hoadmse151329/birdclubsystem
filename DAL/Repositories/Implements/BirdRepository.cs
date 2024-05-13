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
            return _context.Birds.Where(m => m.MemberId == memberId).OrderBy(b => b.BirdName).ToList();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByMemberIdInclude(string memberId)
        {
            return _context.Birds.Where(m => m.MemberId == memberId).Include(m => m.MemberDetails).ToList();
        }

        public async Task<int> GetBirdIdByMemberId(string memberId)
        {
            var result = (from mem in _context.Birds where mem.MemberId == memberId select mem).FirstOrDefault();
            if (result != null) return result.BirdId;
            return 0;
        }

        public async Task<int> GetELOByBirdId(int birdId)
        {
            var result = (from bird in _context.Birds where bird.BirdId == birdId select bird).FirstOrDefault();
            if (result != null) return result.Elo;
            return 0;
        }

        public async Task<Bird> GetBirdById(int birdId)
        {
            return _context.Birds.AsNoTracking().SingleOrDefault(b => b.BirdId.Equals(birdId));
        }

        public async Task<Bird> GetBirdByIdTracking(int birdId)
        {
            return _context.Birds.SingleOrDefault(b => b.BirdId.Equals(birdId));
        }

        public async Task<Bird> GetBirdByName(string birdName)
        {
            return _context.Birds.AsNoTracking().SingleOrDefault(b => b.BirdName.Equals(birdName));
        }

        public async Task<Bird> GetBirdByNameTracking(string birdName)
        {
            return _context.Birds.SingleOrDefault(b => b.BirdName.Equals(birdName));
        }
    }
}

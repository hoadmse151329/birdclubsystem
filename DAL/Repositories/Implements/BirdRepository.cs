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
            return _context.Birds.Where(m => m.MemberId == memberId).ToList();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByMemberIdInclude(string memberId)
        {
            return _context.Birds.Where(m => m.MemberId == memberId).Include(m => m.Member).ToList();
        }
    }
}

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
    public class MemberRepository : RepositoryBase<Member>, IMemberRepository
    {
        private readonly BirdClubContext _context;
        public MemberRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Member?> GetByEmail(string email)
        {
            return _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefault(mem => mem.Email == email);
        }

        public async Task<Member?> GetByIdNoTracking(string id)
        {
            return await _context.Members.AsNoTrackingWithIdentityResolution().Include(mem => mem.Users).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }
    }
}

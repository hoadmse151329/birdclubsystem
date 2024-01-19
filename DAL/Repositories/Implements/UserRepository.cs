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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly BirdClubContext _context;
        public UserRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmail(string email)
        {
            if(_context.Members.AsNoTracking().SingleOrDefault(mem => mem.Email == email) != null)
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.Member).SingleOrDefault(usr => usr.Member.Email == email);
            return null;
        }

        public async Task<User?> GetByIdNoTracking(int id)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.Member).SingleOrDefault(usr => usr.UserId == id);
        }

        public async Task<User?> GetByLogin(string userName, string passWord)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.Member).SingleOrDefault(usr => usr.UserName == userName && usr.Password == passWord);
        }

        public async Task<string?> GetMemberIdByIdNoTracking(int id)
        {
            var usr = _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.Member).SingleOrDefault(usr => usr.UserId == id);
            if (usr != null)
            {
                return usr.MemberId;
            }
            return null;
        }
    }
}

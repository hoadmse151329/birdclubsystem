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
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.MemberDetail).SingleOrDefault(usr => usr.MemberDetail.Email == email);
            return null;
        }

        public async Task<User?> GetByIdNoTracking(int id)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.MemberDetail).SingleOrDefault(usr => usr.UserId == id);
        }

        public async Task<User?> GetByLogin(string userName, string passWord)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.MemberDetail).SingleOrDefault(usr => usr.UserName == userName && usr.Password == passWord);
        }
        public async Task<bool> ChangeUserAvatar(string usrId, string imageAvatar)
        {
            try
            {
                //Get User
                var user = await _context.Users.SingleOrDefaultAsync(u => u.MemberId == usrId);
                if(user == null)
                {
                    throw new Exception("User not Found!");
                }
                // Update the user's image path
                user.ImagePath = imageAvatar;

                // Save changes to the database (replace with your actual logic)
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true; // Return true if the operation was successful
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while changing the image: {ex.Message}");
                return false; // Return false if an error occurred
            }
        }

        public async Task<string?> GetMemberIdByIdNoTracking(int id)
        {
            var usr = _context.Users.AsNoTrackingWithIdentityResolution().Include(usr => usr.MemberDetail).SingleOrDefault(usr => usr.UserId == id);
            if (usr != null)
            {
                return usr.MemberId;
            }
            return null;
        }

        public async Task<User?> GetByMemberId(string memid)
        {
            return await _context.Users.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(usr => usr.MemberId == memid);
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var result = (from usr in _context.Users where usr.UserName.Trim().ToLower() == username.Trim().ToLower() select usr).FirstOrDefault();
            if (result != null) return result.UserId;
            return 0;
        }
    }
}

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
        public class UserManager
        {
            public async Task<bool> ChangeImage(string userId, string newImagePath)
            {
                try
                {
                    // Replace the following line with your logic to fetch the user from the database
                    User user = GetUserFromDatabase(userId);

                    // Update the user's image path
                    user.ImagePath = newImagePath;

                    // Save changes to the database (replace with your actual logic)
                    await SaveChangesToDatabase(user);

                    return true; // Return true if the operation was successful
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while changing the image: {ex.Message}");
                    return false; // Return false if an error occurred
                }
            }

            // Replace the following methods with your actual database interaction logic
            private User GetUserFromDatabase(string userId)
            {
                // Your logic to fetch the user from the database
                // Example: return dbContext.Users.SingleOrDefault(u => u.UserId == userId);
                throw new NotImplementedException();
            }

            private async Task SaveChangesToDatabase(User user)
            {
                // Your logic to save changes to the database
                // Example: dbContext.SaveChanges();
                throw new NotImplementedException();
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

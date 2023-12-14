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

        public User? GetByEmail(string email)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().SingleOrDefault(usr => usr.Member.Email == email);
        }

        public User? GetByIdNoTracking(int id)
        {
            return _context.Users.AsNoTrackingWithIdentityResolution().SingleOrDefault(usr => usr.UserId == id);
        }

        public User? GetByLogin(string userName, string passWord)
    {
            return _context.Users.AsNoTrackingWithIdentityResolution().SingleOrDefault(usr => usr.UserName == userName && usr.Password == passWord);
        }

        public void Insert(User usr)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

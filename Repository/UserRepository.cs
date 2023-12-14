using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BirdClubContext _context;
        public UserRepository(BirdClubContext context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(usr => usr.Member.Email == email);
        }

        public User? GetById(int id)
        {
            return _context.Users.SingleOrDefault(usr => usr.UserId == id);
        }

        public User? GetByLogin(string userName, string password)
        {
            return _context.Users.SingleOrDefault(usr => usr.UserName == userName && usr.Password == password);
        }
    }
}

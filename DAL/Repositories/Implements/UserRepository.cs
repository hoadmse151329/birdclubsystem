using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
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
    }
}

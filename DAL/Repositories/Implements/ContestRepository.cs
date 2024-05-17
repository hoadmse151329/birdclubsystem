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
    public class ContestRepository : RepositoryBase<Contest>, IContestRepository
    {
        private readonly BirdClubContext _context;
        public ContestRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contest>> GetAllContests(string? role)
        {
            if (role == "Manager" || role == "Staff")
            {
                return _context.Contests.AsNoTracking().ToList();
            }
            return _context.Contests.AsNoTracking().Where(c => c.Status == "OpenRegistration").ToList();
        }
        public async Task<Contest?> GetContestById(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }

        public async Task<bool> GetBoolContestId(int id)
        {
            var con = _context.Contests.SingleOrDefault(c => c.ContestId == id);
            if (con != null) return true;
            else return false;
        }

        public async Task<Contest?> GetContestByIdTracking(int id)
        {
            return _context.Contests.SingleOrDefault(c => c.ContestId == id);
        }

        public async Task<Contest?> GetContestByIdWithoutInclude(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }
    }
}

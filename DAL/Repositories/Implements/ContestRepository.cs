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
    public class ContestRepository : RepositoryBase<Contest>, IContestRepository
    {
        private readonly BirdClubContext _context;
        public ContestRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contest>> GetAllContests()
        {
            return _context.Contests.ToList();
        }
        public async Task<Contest?> GetContestById(int id)
        {
            return _context.Contests.SingleOrDefault(contest => contest.ContestId == id);
        }
    }
}

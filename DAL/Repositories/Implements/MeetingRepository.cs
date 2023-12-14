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
    public class MeetingRepository : RepositoryBase<Meeting>, IMeetingRepository
    {
        private readonly BirdClubContext _context;
        public MeetingRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
    }
}

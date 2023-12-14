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
    public class MeetingMediaRepository : RepositoryBase<MeetingMedia>, IMeetingMediaRepository
    {
        private readonly BirdClubContext _context;
        public MeetingMediaRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
    }
}

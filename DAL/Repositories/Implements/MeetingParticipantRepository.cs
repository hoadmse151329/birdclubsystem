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
    public class MeetingParticipantRepository : RepositoryBase<MeetingParticipant>, IMeetingParticipantRepository
    {
        private readonly BirdClubContext _context;
        public MeetingParticipantRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BirdClubContext _context;
        private IMeetingRepository _meetingRepository;
        private IMeetingMediaRepository _meetingMediaRepository;
        private IMeetingParticipantRepository _meetingParticipantRepository;
        private IMemberRepository _memberRepository;
        private IUserRepository _userRepository;
        public UnitOfWork(BirdClubContext context)
        {
            _context = context;
        }
    }
}

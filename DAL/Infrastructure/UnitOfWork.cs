using DAL.Models;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IMeetingRepository MeetingRepository => _meetingRepository ??= new MeetingRepository(_context);
        public IMeetingMediaRepository MeetingMediaRepository => _meetingMediaRepository ??= new MeetingMediaRepository(_context);
        public IMeetingParticipantRepository MeetingParticipantRepository => _meetingParticipantRepository ??= new MeetingParticipantRepository(_context);
        public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_context);

        public IMeetingRepository MeetingRepository => throw new NotImplementedException();

        public IMeetingMediaRepository MediaRepository => throw new NotImplementedException();

        public IMeetingParticipantRepository ParticipantRepository => throw new NotImplementedException();

        public IMemberRepository MemberRepository => throw new NotImplementedException();

        public UnitOfWork(BirdClubContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

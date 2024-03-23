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
        private ILocationRepository _locationRepository;
        private ITransactionRepository _transactionRepository;
        private IFieldTripRepository _fieldTripRepository;
        private IFieldtripDaybyDayRepository _fieldTripDaybyDayRepository;
        private IFieldtripGettingThereRepository _fieldTripGettingThereRepository;
        private IFieldtripInclusionRepository _fieldTripInclusionRepository;
        private IFieldtripMediaRepository _fieldTripMediaRepository;
        private IFieldtripRateRepository _fieldTripRateRepository;
        private IFieldTripParticipantRepository _fieldTripParticipantRepository;
        private IContestRepository _contestRepository;
		public UnitOfWork(BirdClubContext context)
		{
			_context = context;
		}
		public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
		public IMeetingRepository MeetingRepository => _meetingRepository ??= new MeetingRepository(_context);
		public IMeetingMediaRepository MeetingMediaRepository => _meetingMediaRepository ??= new MeetingMediaRepository(_context);
		public IMeetingParticipantRepository MeetingParticipantRepository => _meetingParticipantRepository ??= new MeetingParticipantRepository(_context);
		public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_context);
        public ILocationRepository LocationRepository => _locationRepository ??= new LocationRepository(_context);
        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_context);
        public IFieldTripRepository FieldTripRepository => _fieldTripRepository ??= new FieldTripRepository(_context);
        public IFieldTripParticipantRepository FieldTripParticipantRepository => _fieldTripParticipantRepository ??= new FieldTripParticipantRepository(_context);
        public IContestRepository ContestRepository => _contestRepository ??= new ContestRepository(_context);

        public IFieldtripDaybyDayRepository FieldTripDaybyDayRepository => _fieldTripDaybyDayRepository ??= new FIeldtripDaybyDayRepository(_context);

        public IFieldtripGettingThereRepository FieldTripGettingThereRepository => _fieldTripGettingThereRepository ??= new FieldtripGettingThereRepository(_context);

        public IFieldtripInclusionRepository FieldTripInclusionRepository => _fieldTripInclusionRepository ??= new FieldtripInclusionRepository(_context);

        public IFieldtripMediaRepository FieldTripMediaRepository => _fieldTripMediaRepository ??= new FieldtripMediaRepository(_context);

        public IFieldtripRateRepository FieldTripRateRepository => _fieldTripRateRepository ??= new FieldtripRateRepository(_context);

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

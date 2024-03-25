using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IMeetingRepository MeetingRepository { get; }
        IMeetingMediaRepository MeetingMediaRepository { get; }
        IMeetingParticipantRepository MeetingParticipantRepository { get; }
        IMemberRepository MemberRepository { get; }
        IUserRepository UserRepository { get; } 
        ILocationRepository LocationRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IFieldTripRepository FieldTripRepository { get; }
        IFieldTripParticipantRepository FieldTripParticipantRepository { get; }
        IContestRepository ContestRepository { get; }
        IContestParticipantRepository ContestParticipantRepository { get; }
        void Save();
    }
}

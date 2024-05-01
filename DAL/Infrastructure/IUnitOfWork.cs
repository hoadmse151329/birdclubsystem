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
        IFieldtripDaybyDayRepository FieldTripDaybyDayRepository { get; }
        IFieldtripGettingThereRepository FieldTripGettingThereRepository { get; }
        IFieldtripInclusionRepository FieldTripInclusionRepository { get; }
        IFieldtripMediaRepository FieldTripMediaRepository { get; }
        IFieldtripAdditionalDetailRepository FieldtripAdditionalDetailRepository { get; }
        IFieldTripParticipantRepository FieldTripParticipantRepository { get; }
        IContestRepository ContestRepository { get; }
        IContestParticipantRepository ContestParticipantRepository { get; }
        IContestMediaRepository ContestMediaRepository { get; }
        IBirdRepository BirdRepository { get; }
        INotificationRepository NotificationRepository { get; }
        void Save();
    }
}

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
        IMeetingMediaRepository MediaRepository { get; }
        IMeetingParticipantRepository ParticipantRepository { get; }
        IMemberRepository MemberRepository { get; }
        IUserRepository UserRepository { get; } 
        void save();
    }
}

using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldTripParticipantRepository : IRepositoryBase<FieldTripParticipant>
    {
        Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByTripId(int tripId);
        Task<int> GetCountFieldTripParticipantsByTripId(int tripId);
        Task<bool> GetBoolFieldTripParticipantById(int tripId, string memberId);
        Task<int> GetParticipationNoFieldTripParticipantById(int tripId, string memberId);
        Task<FieldTripParticipant> GetFieldTripParticipantById(int tripId, string memberId);
        Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberId(string memberId);
        Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberIdInclude(string memberId);
        Task<int> GetCountFieldTripParticipantsByMemberId(string memId);
        Task<IEnumerable<FieldTripParticipant>> UpdateAllFieldTripParticipantStatus(List<FieldTripParticipant> part);
    }
}

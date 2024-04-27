using BAL.ViewModels;
using BAL.ViewModels.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripParticipantService
    {
        Task<IEnumerable<FieldTripParticipantViewModel>> GetAll();
        Task<int> Create(string memId, int tripId);
        Task<int> GetCurrentParticipantAmounts(int tripId);
        Task<int> GetParticipationNo(string memId, int tripId);
        Task<bool> Delete(string memId, int tripId);
        Task<IEnumerable<FieldTripParticipantViewModel>> GetAllByMemberId(string memberId);
        Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId);
        Task<IEnumerable<FieldTripParticipantViewModel>> GetAllByTripId(int tripId);
        Task<bool> UpdateAllFieldTripParticipantStatus(List<FieldTripParticipantViewModel> listPart);
    }
}

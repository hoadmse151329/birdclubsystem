using BAL.ViewModels;
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
    }
}

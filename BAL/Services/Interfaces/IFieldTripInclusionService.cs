using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripInclusionService
    {
        Task<bool> Create(int tripId, FieldtripInclusionViewModel inclusion);
        Task<bool> Delete(int incId, int tripId);
        Task<bool> Update(int tripId, FieldtripInclusionViewModel inclusion);
        Task<FieldtripInclusionViewModel> GetById(int incId);
        Task<IEnumerable<FieldtripInclusionViewModel>> GetAllByTripId(int tripId);
    }
}

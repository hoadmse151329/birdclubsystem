using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripAdditionalDetailService
    {
        Task<bool> Create(int tripId, FieldTripAdditionalDetailViewModel addDetail);
        Task<bool> Delete(int addId, int tripId);
        Task<bool> Update(int tripId, FieldTripAdditionalDetailViewModel addDetail);
        Task<FieldTripAdditionalDetailViewModel> GetById(int addId);
        Task<IEnumerable<FieldTripAdditionalDetailViewModel>> GetAllByTripId(int tripId);
    }
}

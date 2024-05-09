using BAL.ViewModels.Event;
using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripDayByDayService
    {
        Task<bool> Create(int tripId, FieldtripDaybyDayViewModel dayDetail);
        Task<bool> Delete(int dayId, int tripId);
        Task<bool> Update(int tripId, FieldtripDaybyDayViewModel dayDetail);
        Task<FieldtripDaybyDayViewModel> GetById(int dayId);
        Task<IEnumerable<FieldtripDaybyDayViewModel>> GetAllByTripId(int tripId);
    }
}

using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripMediaService
    {
        Task<bool> Create(int tripId, FieldtripMediaViewModel media);
        Task<bool> Delete(int pictureId, int tripId);
        Task<bool> Update(int tripId, FieldtripMediaViewModel media);
        Task<IEnumerable<FieldtripMediaViewModel>> GetAllByTripId(int tripId);
    }
}

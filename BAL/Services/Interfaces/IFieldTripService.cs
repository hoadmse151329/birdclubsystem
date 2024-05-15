using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripService
    {
        Task<FieldTripViewModel?> GetById(int id);
        Task<FieldTripViewModel?> GetByIdWithoutInclude(int id);
        Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips(string? role);
        void Create(FieldTripViewModel entity);
        void Update(FieldTripViewModel entity);
        bool UpdateGettingThere(FieldtripGettingThereViewModel entity);
        Task<bool> GetBoolFieldTripId(int id);
        bool UpdateMedia(FieldtripMediaViewModel entity);
    }
}

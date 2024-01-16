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
        Task<FieldTripViewModel?> GetFieldTripById(int id);
        Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips();
        void Create(FieldTripViewModel entity);
        void Update(FieldTripViewModel entity);
    }
}

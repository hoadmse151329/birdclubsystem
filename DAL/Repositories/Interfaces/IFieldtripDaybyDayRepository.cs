using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripDaybyDayRepository : IRepositoryBase<FieldtripDaybyDay>
    {
        Task<FieldtripDaybyDay> GetFieldTripDayByDayById(int tripId, int dayId);
        Task<FieldtripDaybyDay> GetFieldTripDayByDayByIdTracking(int tripId, int dayId);
        Task<IEnumerable<FieldtripDaybyDay>> GetAllFieldTripDayByDaysByTripId(int tripId);
    }
}

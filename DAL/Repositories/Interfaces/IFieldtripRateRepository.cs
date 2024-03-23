using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripRateRepository : IRepositoryBase<FieldtripRate>
    {
        Task<FieldtripRate> GetFieldTripRateById(int tripId, int rateId);
        Task<IEnumerable<FieldtripRate>> GetFieldTripRatesByTripId(int tripId);
    }
}

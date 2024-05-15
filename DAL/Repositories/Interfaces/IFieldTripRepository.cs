using DAL.Infrastructure;
using DAL.Repositories.Implements;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldTripRepository : IRepositoryBase<FieldTrip>
    {
        Task<IEnumerable<FieldTrip>> GetAllFieldTrips(string? role);
        Task<FieldTrip?> GetFieldTripById(int id);
        Task<FieldTrip?> GetFieldTripByIdWithoutInclude(int id);
        Task<bool> GetBoolFieldTripId(int id);
    }
}

using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripAdditionalDetailRepository : IRepositoryBase<FieldtripAdditionalDetail>
    {
        Task<FieldtripAdditionalDetail> GetFieldTripAdditionalDetailById(int tripId, int addDetailId);
        Task<IEnumerable<FieldtripAdditionalDetail>> GetFieldTripAdditionalDetailsByTripId(int tripId);
    }
}

using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripMediaRepository : IRepositoryBase<FieldtripMedia>
    {
        Task<FieldtripMedia> GetFieldTripMediaById(int tripId, int pictureId);
        Task<FieldtripMedia> GetFieldTripMediaByIdTracking(int tripId, int pictureId);
        Task<IEnumerable<FieldtripMedia>> GetFieldTripMediasByTripId(int tripId);
    }
}

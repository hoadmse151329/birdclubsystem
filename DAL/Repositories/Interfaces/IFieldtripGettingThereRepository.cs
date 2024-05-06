using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripGettingThereRepository : IRepositoryBase<FieldtripGettingThere>
    {
        Task<FieldtripGettingThere> GetFieldTripGettingTheresByTripId(int tripId);
    }
}

using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILocationRepository : IRepositoryBase<Location>
    {
        Task<IEnumerable<string?>> GetAllLocationName();
        Task<string?> GetLocationNameById(int id);
        Task<Location?> GetLocationByName(string name);
        Task<Location?> GetLocationByMeetingId(int meetid);
        Task<Location?> GetLocationByTripId(int tripId);
        Task<Location?> GetLocationByContestId(int contestId);
    }
}

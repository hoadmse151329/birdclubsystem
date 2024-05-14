using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IBirdRepository : IRepositoryBase<Bird>
    {
        Task<IEnumerable<Bird>> GetBirdsByMemberId(string memberId);
        Task<IEnumerable<Bird>> GetBirdsByMemberIdInclude(string memberId);
        Task<int> GetBirdIdByMemberId(string memberId);
        Task<int> GetELOByBirdId(int birdId);
        Task<Bird> GetBirdById(int birdId);
        Task<Bird> GetBirdByIdTracking(int birdId);
        Task<Bird> GetBirdByName(string birdName);
        Task<Bird> GetBirdByNameTracking(string birdName);
    }
}

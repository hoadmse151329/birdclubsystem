using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IBirdService
    {
        Task<IEnumerable<BirdViewModel>> GetBirdsByMemberId(string memberId);
        Task<bool> Create(string memberId, BirdViewModel bird);
        Task<bool> Update(string memberId, BirdViewModel bird);
        Task<bool> Delete(string memberId, int birdId);
        Task<BirdViewModel> GetById(int birdId);
        Task<BirdViewModel> GetByBirdName(string birdName);
    }
}

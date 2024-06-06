using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IContestMediaService
    {
        Task<bool> Create(int contestId, ContestMediaViewModel media);
        Task<bool> Delete(int contestId, int pictureId);
        Task<bool> Update(int contestId, ContestMediaViewModel media);
        Task<ContestMediaViewModel> GetById(int pictureId);
        Task<IEnumerable<ContestMediaViewModel>> GetAllByContestId(int contestId);
    }
}

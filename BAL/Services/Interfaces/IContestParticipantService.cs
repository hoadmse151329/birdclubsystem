using AutoMapper;
using BAL.ViewModels.Event;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IContestParticipantService
    {
        Task<IEnumerable<ContestParticipantViewModel>> GetAll();
        Task<int> Create(string memId, int contestId);
        Task<int> GetCurrentParticipantAmounts(int contestId);
        Task<int> GetParticipationNo(string memId, int contestId);
        Task<bool> Delete(string memId, int contestId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByMemberId(string memberId);
        Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByContestId(int contestId);
    }
}

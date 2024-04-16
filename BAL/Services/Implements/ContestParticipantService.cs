using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class ContestParticipantService : IContestParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContestParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<int> Create(string memId, int contestId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string memId, int contestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContestParticipantViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContestParticipantViewModel>> GetAllByContestId(int contestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContestParticipantViewModel>> GetAllByMemberId(string memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCurrentParticipantAmounts(int contestId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetParticipationNo(string memId, int contestId)
        {
            throw new NotImplementedException();
        }
    }
}

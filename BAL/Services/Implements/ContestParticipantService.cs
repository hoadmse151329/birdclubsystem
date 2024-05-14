using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels.Event;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
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

        public async Task<int> Create(int contestId, string memberId, int birdId)
        {
            int partNo = await _unitOfWork.ContestParticipantRepository.GetParticipationNoContestParticipantById(contestId, memberId);
            if (partNo > 0) return partNo;
            int contestpartCount = await _unitOfWork.ContestParticipantRepository.GetCountContestParticipantsByContestId(contestId);
            if (contestpartCount.Equals(0)) partNo = 1; else partNo = contestpartCount + 1;
            int elo = await _unitOfWork.BirdRepository.GetELOByBirdId(birdId);
            ContestParticipant contestParticipant = new ContestParticipant()
            {
                ContestId = contestId,
                BirdId = birdId,
                MemberId = memberId,
                ParticipantNo = partNo.ToString(),
                Elo = elo
            };
            _unitOfWork.ContestParticipantRepository.Create(contestParticipant);
            _unitOfWork.Save();
            return partNo;
        }

        public async Task<IEnumerable<ContestParticipantViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ContestParticipantViewModel>>(_unitOfWork.ContestRepository.GetAll());
        }

        public async Task<int> GetCurrentParticipantAmounts(int contestId)
        {
            return await _unitOfWork.ContestParticipantRepository.GetCountContestParticipantsByContestId(contestId);
        }

        public async Task<bool> Delete(int contestId, string memberId)
        {
            bool check = await _unitOfWork.ContestParticipantRepository.GetBoolContestParticipantById(contestId, memberId);
            if (!check) return false;
            var contestParticipant = await _unitOfWork.ContestParticipantRepository.GetContestParticipantById(contestId, memberId);
            _unitOfWork.ContestParticipantRepository.Delete(contestParticipant);
            _unitOfWork.Save();
            return true;
        }
        public async Task<IEnumerable<ContestParticipantViewModel>> GetAllByBirdId(int birdId)
        {
            return _mapper.Map<IEnumerable<ContestParticipantViewModel>>(await
                _unitOfWork.ContestParticipantRepository.GetContestParticipantsByBirdId(birdId));
        }

        public async Task<IEnumerable<GetEventParticipation>> GetAllByBirdIdInclude(int birdId)
        {
            return _mapper.Map<IEnumerable<GetEventParticipation>>(await
                _unitOfWork.ContestParticipantRepository.GetContestParticipantsByBirdIdInclude(birdId));
        }

        public async Task<IEnumerable<ContestParticipantViewModel>> GetAllByContestId(int contestId)
        {
            return _mapper.Map<IEnumerable<ContestParticipantViewModel>>(await
                _unitOfWork.ContestParticipantRepository.GetContestParticipantsByContestId(contestId));
        }

        public async Task<IEnumerable<ContestParticipantViewModel>> GetAllByMemberId(string memberId)
        {
            return _mapper.Map<IEnumerable<ContestParticipantViewModel>>(await
                _unitOfWork.ContestParticipantRepository.GetContestParticipantsByMemberId(memberId));
        }

        public async Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId)
        {
            return _mapper.Map<IEnumerable<GetEventParticipation>>(await
                _unitOfWork.ContestParticipantRepository.GetContestParticipantsByMemberIdInclude(memberId));
        }

        public async Task<int> GetParticipationNo(int contestId, string memberId, int? birdId = null)
        {
            return await _unitOfWork.ContestParticipantRepository.GetParticipationNoContestParticipantById(contestId, memberId, birdId);
        }

        public async Task<bool> UpdateAllContestParticipantStatus(List<ContestParticipantViewModel> listPart)
        {
            var part = await _unitOfWork.ContestParticipantRepository
                .UpdateAllContestParticipantStatus(_mapper.Map<List<ContestParticipant>>(listPart));
            if (part != null)
            {
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAllContestParticipantScore(List<ContestParticipantViewModel> listPart, bool isContestEnded = false)
        {
            var part = await _unitOfWork.ContestParticipantRepository
                .UpdateAllContestParticipantScore(_mapper.Map<List<ContestParticipant>>(listPart),isContestEnded);
            if (part != null)
            {
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
﻿using AutoMapper;
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

        public async Task<int> Create(string memberId, int contestId, int? birdId = null)
        {
            int partNo = await _unitOfWork.ContestParticipantRepository.GetParticipationNoContestParticipantById(contestId, memberId, birdId);
            if (partNo > 0) return partNo;
            int contestpartCount = await _unitOfWork.ContestParticipantRepository.GetCountContestParticipantsByContestId(contestId);
            if (contestpartCount.Equals(0)) partNo = 1; else partNo = contestpartCount + 1;
            ContestParticipant contestParticipant = new ContestParticipant()
            {
                ContestId = contestId,
                BirdId = birdId,
                ParticipantNo = partNo.ToString()
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

        public async Task<bool> Delete(string memberId, int contestId, int? birdId = null)
        {
            bool check = await _unitOfWork.ContestParticipantRepository.GetBoolContestParticipantById(contestId, memberId, birdId);
            if (!check) return false;
            ContestParticipant contestParticipant = await _unitOfWork.ContestParticipantRepository.GetContestParticipantById(contestId, memberId, birdId);
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
    }
}
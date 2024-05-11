using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class FieldTripParticipantService : IFieldTripParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Create(string memId, int tripId)
        {
            int partNo = await _unitOfWork.FieldTripParticipantRepository.GetParticipationNoFieldTripParticipantById(tripId, memId);
            if (partNo > 0) return partNo;
            int trippartCount = await _unitOfWork.FieldTripParticipantRepository.GetCountFieldTripParticipantsByTripId(tripId);
            if (trippartCount.Equals(0)) partNo = 1; else partNo = trippartCount + 1;
            FieldTripParticipant fieldTripParticipant = new FieldTripParticipant()
            {
                TripId = tripId,
                MemberId = memId,
                ParticipantNo = partNo.ToString()
            };
            _unitOfWork.FieldTripParticipantRepository.Create(fieldTripParticipant);
            _unitOfWork.Save();
            return partNo;
        }

        public async Task<IEnumerable<FieldTripParticipantViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<FieldTripParticipantViewModel>>(_unitOfWork.FieldTripRepository.GetAll());
        }

        public async Task<int> GetCurrentParticipantAmounts(int tripId)
        {
            return await _unitOfWork.FieldTripParticipantRepository.GetCountFieldTripParticipantsByTripId(tripId);
        }

        public async Task<int> GetParticipationNo(string memId, int tripId)
        {
            return await _unitOfWork.FieldTripParticipantRepository.GetParticipationNoFieldTripParticipantById(tripId, memId);
        }

        public async Task<bool> Delete(string memId, int tripId)
        {
            bool check = await _unitOfWork.FieldTripParticipantRepository.GetBoolFieldTripParticipantById(tripId, memId);
            if (!check) return false;
            FieldTripParticipant fieldTripParticipant = await _unitOfWork.FieldTripParticipantRepository.GetFieldTripParticipantById(tripId, memId);
            _unitOfWork.FieldTripParticipantRepository.Delete(fieldTripParticipant);
            _unitOfWork.Save();
            return true;
        }
        public async Task<IEnumerable<FieldTripParticipantViewModel>> GetAllByMemberId(string memberId)
        {

            return _mapper.Map<IEnumerable<FieldTripParticipantViewModel>>(await
                _unitOfWork.FieldTripParticipantRepository.GetFieldTripParticipantsByMemberId(memberId));
        }

        public async Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId)
        {

            return _mapper.Map<IEnumerable<GetEventParticipation>>(await
                _unitOfWork.FieldTripParticipantRepository.GetFieldTripParticipantsByMemberIdInclude(memberId));
        }

        public async Task<IEnumerable<FieldTripParticipantViewModel>> GetAllByTripId(int tripId)
        {
            return _mapper.Map<IEnumerable<FieldTripParticipantViewModel>>(await
                _unitOfWork.FieldTripParticipantRepository.GetFieldTripParticipantsByTripId(tripId));
        }

        public async Task<bool> UpdateAllFieldTripParticipantStatus(List<FieldTripParticipantViewModel> listPart)
        {
            var part = await _unitOfWork.FieldTripParticipantRepository.UpdateAllFieldTripParticipantStatus
                (_mapper.Map<List<FieldTripParticipant>>(listPart));
            if (part != null)
            {
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}

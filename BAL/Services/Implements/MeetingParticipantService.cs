using AutoMapper;
using AutoMapper.Execution;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class MeetingParticipantService : IMeetingParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MeetingParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Create(string memId, int metId)
        {
            int partNo = await _unitOfWork.MeetingParticipantRepository.GetParticipationNoMeetingParticipantById(metId, memId);
            if (partNo > 0) return partNo;
            int meetpartCount =  await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(metId);
            if (meetpartCount.Equals(0)) partNo = 1; else partNo = meetpartCount + 1;
            MeetingParticipant meetingParticipant = new MeetingParticipant()
            {
                MeetingId= metId,
                MemberId= memId,
                ParticipantNo = partNo.ToString(),
            };
            _unitOfWork.MeetingParticipantRepository.Create(meetingParticipant);
            _unitOfWork.Save();
            return partNo;
        }

        public async Task<IEnumerable<MeetingParticipantViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<MeetingParticipantViewModel>>(_unitOfWork.MeetingRepository.GetAll());
        }

        public async Task<int> GetCurrentParticipantAmounts(int metId)
        {
            return await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(metId);
        }

        public async Task<int> GetParticipationNo(string memId, int metId)
        {
            return await _unitOfWork.MeetingParticipantRepository.GetParticipationNoMeetingParticipantById(metId, memId);
        }

        public async Task<bool> Delete(string memId, int metId)
        {
            bool check = await _unitOfWork.MeetingParticipantRepository.GetBoolMeetingParticipantById(metId, memId);
            if (!check) return false;
            var meetingParticipant = await _unitOfWork.MeetingParticipantRepository.GetMeetingParticipantById(metId, memId);
            _unitOfWork.MeetingParticipantRepository.Delete(meetingParticipant);
            _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<MeetingParticipantViewModel>> GetAllByMemberId(string memberId)
        {
            
            return _mapper.Map<IEnumerable<MeetingParticipantViewModel>>(await 
                _unitOfWork.MeetingParticipantRepository.GetMeetingParticipantsByMemberId(memberId));
        }

        public async Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId)
        {

            return _mapper.Map<IEnumerable<GetEventParticipation>>(await
                _unitOfWork.MeetingParticipantRepository.GetMeetingParticipantsByMemberIdInclude(memberId));
        }

        public async Task<IEnumerable<MeetingParticipantViewModel>> GetAllByMeetingId(int meetId)
        {
            return _mapper.Map<IEnumerable<MeetingParticipantViewModel>>(await
                _unitOfWork.MeetingParticipantRepository.GetMeetingParticipantsByMeetId(meetId));
        }

        public async Task<MeetingParticipantViewModel?> GetById(string memberId, int meetId)
        {
            var meetpart = await _unitOfWork.MeetingParticipantRepository.GetMeetingParticipantById(meetId, memberId);
            if (meetpart != null)
            {
                var meetingpart = _mapper.Map<MeetingParticipantViewModel>(meetpart);
                return meetingpart;
            }
            return null;
        }

        public void Update(MeetingParticipantViewModel entity)
        {
            var meetpart = _mapper.Map<MeetingParticipant>(entity);
            _unitOfWork.MeetingParticipantRepository.Update(meetpart);
            _unitOfWork.Save();
        }

        public async Task<bool> UpdateAllMeetingParticipantStatus(List<MeetingParticipantViewModel> listPart)
        {
            var part = await _unitOfWork.MeetingParticipantRepository.UpdateAllMeetingParticipantStatus
                (_mapper.Map<List<MeetingParticipant>>(listPart));
            if (part != null)
            {
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}

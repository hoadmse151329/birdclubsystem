using AutoMapper;
using BAL.Services.Interfaces;
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
                ParticipantNo = partNo.ToString()
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
    }
}

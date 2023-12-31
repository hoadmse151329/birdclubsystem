﻿using AutoMapper;
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
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MeetingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MeetingViewModel>> GetAll()
        {
            string locationName;
            var listmeet = _unitOfWork.MeetingRepository.GetAll();
            var listmeetview = _mapper.Map<IEnumerable<MeetingViewModel>>(listmeet);
            foreach (var itemview in listmeetview)
            {
                foreach(var item in listmeet)
                {
                    if(item.MeetingId == itemview.MeetingId)
                    {
                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);
                        itemview.Address = locationName;
                        itemview.District = locationName.Split(",")[2];
                        itemview.City = locationName.Split(",")[3];
                    }
                }
            }
            return listmeetview;
        }

        public IEnumerable<MeetingViewModel> GetAllByRegistrationDeadline(DateTime registrationDeadline)
        {
            return _mapper.Map<IEnumerable<MeetingViewModel>>(_unitOfWork.MeetingRepository.GetAllByRegistrationDeadline(registrationDeadline));
        }

        public async Task<MeetingViewModel?> GetById(int id)
        {
            var meet = await _unitOfWork.MeetingRepository.GetMeetingById(id);
            if (meet != null)
            {
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(meet.LocationId.Value);
                var meeting = _mapper.Map<MeetingViewModel>(meet);
                meeting.Address = locationName;
                meeting.District = locationName.Split(",")[2];
                meeting.City = locationName.Split(",")[3];
                return meeting;
            }
            return null;
        }

        public IEnumerable<MeetingViewModel> GetSortedMeetings(int meetingId,
            string? meetingName,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            string? orderBy)
        {
            return _mapper.Map<IEnumerable<MeetingViewModel>>(_unitOfWork.MeetingRepository.GetSortedMeetings(
                meetingId,
                meetingName,
                registrationDeadline,
                startDate,
                endDate,
                numberOfParticipants,
                orderBy
                ));
        }

        public List<string> GetAllMeetingName()
        {
            var meetingNames = _unitOfWork.MeetingRepository.GetAllMeetingName().ToList();
            return meetingNames;
        }

        public void Create(MeetingViewModel entity)
        {
            var meeting = _mapper.Map<Meeting>(entity);
            _unitOfWork.MeetingRepository.Create(meeting);
            _unitOfWork.Save();
        }

        public void Update(MeetingViewModel entity)
        {
            var meeting = _mapper.Map<Meeting>(entity);
            _unitOfWork.MeetingRepository.Update(meeting);
            _unitOfWork.Save();
        }
    }
}

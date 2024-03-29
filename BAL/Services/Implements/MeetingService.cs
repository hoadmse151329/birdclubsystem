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
                        //int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);
                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);
                        
                        string[] temp = locationName.Split(",");

                        itemview.AreaNumber = Int32.Parse(temp[0]);
                        itemview.Street = temp[1];
                        itemview.District = temp[2];
                        itemview.City = temp[3];
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
                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);
                
                var meeting = _mapper.Map<MeetingViewModel>(meet);
                meeting.NumberOfParticipants = meeting.NumberOfParticipantsLimit - partAmount;
                meeting.Address = locationName;

                string[] temp = locationName.Split(",");

                meeting.AreaNumber = Int32.Parse(temp[0]);
                meeting.Street = temp[1];
                meeting.District = temp[2];
                meeting.City = temp[3];
                return meeting;
            }
            return null;
        }

        public IEnumerable<MeetingViewModel> GetSortedMeetings(
            int? meetingId,
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
            var loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;

            if (loc == null)
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address.Trim(),
                    Description = loc.Description
                });
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address).Result;
            }
            var meeting = _mapper.Map<Meeting>(entity);
            meeting.LocationId = loc.LocationId;
            _unitOfWork.MeetingRepository.Create(meeting);
            _unitOfWork.Save();
        }

        public void Update(MeetingViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByMeetingId(entity.MeetingId.Value).Result;

            if (!loc.LocationName.Equals(entity.Address.Trim()))
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address,
                    Description = "Dunno"
                });
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;
            }
            var meeting = _mapper.Map<Meeting>(entity);
            meeting.LocationId = loc.LocationId;
            _unitOfWork.MeetingRepository.Update(meeting);
            _unitOfWork.Save();
        }
    }
}

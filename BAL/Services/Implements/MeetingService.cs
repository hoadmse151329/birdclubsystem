using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Implements;
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
        public async Task<IEnumerable<MeetingViewModel>> GetAllMeetings(string? role)
        {
            string locationName;
            var listmeet = await _unitOfWork.MeetingRepository.GetAllMeetings(role);
            var listmeetview = _mapper.Map<IEnumerable<MeetingViewModel>>(listmeet);

            foreach (var itemview in listmeetview)
            {
                foreach (var item in listmeet)
                {
                    if (item.MeetingId == itemview.MeetingId)
                    {
                        //int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);
                        var media = await _unitOfWork.MeetingMediaRepository.GetAllMeetingMediasByMeetingId(item.MeetingId);
                        itemview.MeetingPictures = (media.Count() > 0) ? _mapper.Map<IEnumerable<MeetingMediaViewModel>>(media).ToList() : itemview.MeetingPictures;

                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);

                        string[] temp = locationName.Split(",");
                        itemview.AreaNumber = temp[0];
                        itemview.Street = temp[1];
                        itemview.District = temp[2];
                        itemview.City = temp[3];
                    }
                }
            }
            return listmeetview;
        }

        /*public async Task<IEnumerable<MeetingViewModel>> GetOpenMeetings()
        {
            string locationName;
            var listmeet = _unitOfWork.MeetingRepository.GetOpenMeetings();
            var listmeetview = _mapper.Map<IEnumerable<MeetingViewModel>>(listmeet);

            foreach (var itemview in listmeetview)
            {
                foreach (var item in listmeet)
                {
                    if (item.MeetingId == itemview.MeetingId)
                    {
                        //int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);
                        var media = await _unitOfWork.MeetingMediaRepository.GetMeetingMediasByMeetingId(item.MeetingId);
                        itemview.MeetingPictures = (media != null) ? _mapper.Map<IEnumerable<MeetingMediaViewModel>>(media).ToList() : null;

                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);

                        string[] temp = locationName.Split(",");
                        itemview.AreaNumber = temp[0];
                        itemview.Street = temp[1];
                        itemview.District = temp[2];
                        itemview.City = temp[3];
                    }
                }
            }
            return listmeetview;
        }*/

        public IEnumerable<MeetingViewModel> GetAllByRegistrationDeadline(DateTime registrationDeadline)
        {
            return _mapper.Map<IEnumerable<MeetingViewModel>>(_unitOfWork.MeetingRepository.GetAllByRegistrationDeadline(registrationDeadline));
        }

        public async Task<MeetingViewModel?> GetById(int id)
        {
            var meet = await _unitOfWork.MeetingRepository.GetMeetingById(id);
            if (meet != null)
            {
                var media = await _unitOfWork.MeetingMediaRepository.GetAllMeetingMediasByMeetingId(meet.MeetingId);
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(meet.LocationId.Value);
                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);

                var meeting = _mapper.Map<MeetingViewModel>(meet);
                meeting.NumberOfParticipants = meeting.NumberOfParticipantsLimit - partAmount;
                meeting.Address = locationName;

                meeting.MeetingPictures = (media.Count() > 0) ? _mapper.Map<IEnumerable<MeetingMediaViewModel>>(media).ToList() : meeting.MeetingPictures;

                string[] temp = locationName.Split(",");

                meeting.AreaNumber = temp[0];
                meeting.Street = temp[1];
                meeting.District = temp[2];
                meeting.City = temp[3];
                foreach (var picture in meeting.MeetingPictures.ToList())
                {
                    if (picture.Type == "Spotlight")
                    {
                        meeting.SpotlightImage = picture;
                        meeting.MeetingPictures.Remove(picture);
                    }
                    else
                    if (picture.Type == "LocationMap")
                    {
                        meeting.LocationMapImage = picture;
                        meeting.MeetingPictures.Remove(picture);
                    }
                }
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
            string? locationAddress,
            string? orderBy)
        {
            return _mapper.Map<IEnumerable<MeetingViewModel>>(_unitOfWork.MeetingRepository.GetSortedMeetings(
                meetingId,
                meetingName,
                registrationDeadline,
                startDate,
                endDate,
                numberOfParticipants,
                locationAddress,
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
                    Description = ""
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;
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
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;
            }
            var media = _unitOfWork.MeetingMediaRepository.GetAllMeetingMediasByMeetingId(entity.MeetingId.Value).Result;

            if (media == null)
            {
                _unitOfWork.MeetingMediaRepository.Update(new MeetingMedia
                {
                    MeetingId = entity.MeetingId.Value,
                });
                _unitOfWork.Save();
                media = _unitOfWork.MeetingMediaRepository.GetAllMeetingMediasByMeetingId(entity.MeetingId.Value).Result;
            }

            var meeting = _mapper.Map<Meeting>(entity);
            meeting.LocationId = loc.LocationId;
            meeting.MeetingPictures = (ICollection<MeetingMedia>)media;
            _unitOfWork.MeetingRepository.Update(meeting);
            _unitOfWork.Save();
        }

        public async Task<bool> GetBoolMeetingId(int id)
        {
            var meet = await _unitOfWork.MeetingRepository.GetBoolMeetingId(id);
            if (!meet) return false;
            return true;
        }
    }
}
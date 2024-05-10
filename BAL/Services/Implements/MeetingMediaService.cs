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
    public class MeetingMediaService : IMeetingMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MeetingMediaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(int meetingId, MeetingMediaViewModel media)
        {
            var meet = await _unitOfWork.MeetingRepository.GetMeetingById(meetingId);
            if (meet == null) return false;
            var pic = _mapper.Map<MeetingMedia>(media);
            pic.MeetingId = meetingId;
            pic.Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png";
            _unitOfWork.MeetingMediaRepository.Create(pic);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int meetingId, int pictureId)
        {
            var meet = await _unitOfWork.MeetingRepository.GetMeetingById(meetingId);
            if (meet == null) return false;
            var pic = await _unitOfWork.MeetingMediaRepository.GetMeetingMediaByIdTracking(pictureId);
            if (pic == null) return false;
            _unitOfWork.MeetingMediaRepository.Delete(pic);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Update(int meetingId, MeetingMediaViewModel media)
        {
            var meet = await _unitOfWork.MeetingRepository.GetMeetingById(meetingId);
            if (meet == null) return false;
            if (media == null || media.PictureId == null) return false;
            var meetmedia = await _unitOfWork.MeetingMediaRepository.GetMeetingMediaById(media.PictureId.Value);
            if (meetmedia == null) return false;
            var pic = _mapper.Map<MeetingMedia>(media);
            pic.MeetingId = meetingId;
            _unitOfWork.Save();
            return true;
        }

        public async Task<MeetingMediaViewModel> GetById(int pictureId)
        {
            return _mapper.Map<MeetingMediaViewModel>(await _unitOfWork.MeetingMediaRepository.GetMeetingMediaById(pictureId));
        }

        public async Task<IEnumerable<MeetingMediaViewModel>> GetAllByMeetingId(int meetingId)
        {
            throw new NotImplementedException();
        }
    }
}

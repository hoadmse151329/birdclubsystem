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
    public class FieldTripMediaService : IFieldTripMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripMediaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(int tripId, FieldtripMediaViewModel media)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var picture = _mapper.Map<FieldtripMedia>(media);
            if (media.Image == null)
            {
                picture.Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_1.png";
            }
            picture.TripId = tripId;
            _unitOfWork.FieldTripMediaRepository.Create(picture);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int pictureId, int tripId)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);
            if (trip == null) return false;
            var pic = await _unitOfWork.FieldTripMediaRepository.GetFieldTripMediaById(pictureId);
            if (pic == null) return false;
            _unitOfWork.FieldTripMediaRepository.Delete(pic);
            _unitOfWork.Save();
            return true;
        }

        public Task<IEnumerable<FieldtripMediaViewModel>> GetAllByTripId(int tripId)
        {
            throw new NotImplementedException();
        }

        public async Task<FieldtripMediaViewModel> GetById(int pictureId)
        {
            return _mapper.Map<FieldtripMediaViewModel>(await _unitOfWork.FieldTripMediaRepository.GetFieldTripMediaById(pictureId));
        }

        public async Task<bool> Update(int tripId, FieldtripMediaViewModel media)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);
            if (trip == null) return false;
            if (media == null || media.PictureId == null) return false;
            var tripmedia = await _unitOfWork.FieldTripMediaRepository.GetFieldTripMediaById(media.PictureId.Value);
            if (tripmedia == null) return false;
            var pic = _mapper.Map<FieldtripMedia>(media);
            pic.TripId = tripId;
            if (media.Image == null)
            {
                pic.Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/contest/contest_image_1.png";
            }
            _unitOfWork.FieldTripMediaRepository.Update(pic);
            _unitOfWork.Save();
            return true;
        }
    }
}

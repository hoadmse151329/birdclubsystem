using AutoMapper;
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
    public class FieldTripMediaService
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
            picture.TripId = tripId;
            _unitOfWork.FieldTripMediaRepository.Create(picture);
            _unitOfWork.Save();
            return true;
        }
    }
}

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
    public class FieldTripAdditionalDetailService : IFieldTripAdditionalDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripAdditionalDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(int tripId, FieldTripAdditionalDetailViewModel addDetail)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var add = _mapper.Map<FieldtripAdditionalDetail>(addDetail);
            add.TripId = tripId;
            _unitOfWork.FieldtripAdditionalDetailRepository.Create(add);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int addId, int tripId)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var add = await _unitOfWork.FieldtripAdditionalDetailRepository.GetFieldTripAdditionalDetailByIdTracking(addId);

            if (add == null) return false;
            _unitOfWork.FieldtripAdditionalDetailRepository.Delete(add);
            _unitOfWork.Save();
            return true;
        }

        public Task<IEnumerable<FieldTripAdditionalDetailViewModel>> GetAllByTripId(int tripId)
        {
            throw new NotImplementedException();
        }

        public async Task<FieldTripAdditionalDetailViewModel> GetById(int addId)
        {
            return _mapper.Map<FieldTripAdditionalDetailViewModel>(await _unitOfWork.FieldtripAdditionalDetailRepository.GetFieldTripAdditionalDetailById(addId));
        }

        public async Task<bool> Update(int tripId, FieldTripAdditionalDetailViewModel addDetail)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            if (addDetail == null || addDetail.TripDetailsId == null) return false;

            var addDe = await _unitOfWork.FieldtripAdditionalDetailRepository.GetFieldTripAdditionalDetailById(addDetail.TripDetailsId.Value);

            if (addDe == null) return false;

            var add = _mapper.Map<FieldtripAdditionalDetail>(addDetail);
            add.TripId = tripId;
            _unitOfWork.FieldtripAdditionalDetailRepository.Update(add);
            _unitOfWork.Save();
            return true;
        }
    }
}

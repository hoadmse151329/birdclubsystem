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
    public class FieldTripInclusionService : IFieldTripInclusionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripInclusionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(int tripId, FieldtripInclusionViewModel inclusion)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var inclu = _mapper.Map<FieldtripInclusion>(inclusion);
            inclu.TripId = tripId;
            _unitOfWork.FieldTripInclusionRepository.Create(inclu);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int incluId, int tripId)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var inclu = _unitOfWork.FieldTripInclusionRepository.GetById(incluId);

            if (inclu == null) return false;
            _unitOfWork.FieldTripInclusionRepository.Delete(inclu);
            _unitOfWork.Save();
            return true;
        }

        public Task<IEnumerable<FieldtripInclusionViewModel>> GetAllByTripId(int tripId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(int tripId, FieldtripInclusionViewModel inclusion)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            if (inclusion == null || inclusion.InclusionId == null) return false;

            var inclu = await _unitOfWork.FieldTripInclusionRepository.GetFieldTripInclusionByIdTracking(tripId, inclusion.InclusionId.Value);

            if (inclu == null) return false;

            inclu.TripId = tripId;
            _unitOfWork.FieldTripInclusionRepository.Update(inclu);
            _unitOfWork.Save();
            return true;
        }
    }
}

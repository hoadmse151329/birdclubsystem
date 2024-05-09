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
    public class FieldTripDayByDayService : IFieldTripDayByDayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripDayByDayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(int tripId, FieldtripDaybyDayViewModel dayDetail)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var day = _mapper.Map<FieldtripDaybyDay>(dayDetail);
            day.TripId = tripId;
            _unitOfWork.FieldTripDaybyDayRepository.Create(day);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int dayId, int tripId)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            var day = await _unitOfWork.FieldTripDaybyDayRepository.GetFieldTripDayByDayByIdTracking(dayId);

            if (day == null) return false;
            _unitOfWork.FieldTripDaybyDayRepository.Delete(day);
            _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<FieldtripDaybyDayViewModel>> GetAllByTripId(int tripId)
        {
            throw new NotImplementedException();
        }

        public async Task<FieldtripDaybyDayViewModel> GetById(int dayId)
        {
            return _mapper.Map<FieldtripDaybyDayViewModel>(await _unitOfWork.FieldTripDaybyDayRepository.GetFieldTripDayByDayById(dayId));
        }

        public async Task<bool> Update(int tripId, FieldtripDaybyDayViewModel dayDetail)
        {
            var ftrip = await _unitOfWork.FieldTripRepository.GetFieldTripById(tripId);

            if (ftrip == null) return false;

            if (dayDetail == null || dayDetail.DaybyDayId == null) return false;

            var tripDay = await _unitOfWork.FieldTripDaybyDayRepository.GetFieldTripDayByDayById(dayDetail.DaybyDayId.Value);

            if (tripDay == null) return false;

            var day = _mapper.Map<FieldtripDaybyDay>(dayDetail);
            day.TripId = tripId;
            _unitOfWork.FieldTripDaybyDayRepository.Update(day);
            _unitOfWork.Save();
            return true;
        }
    }
}

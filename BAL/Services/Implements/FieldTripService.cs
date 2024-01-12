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
    public class FieldTripService : IFieldTripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldTripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FieldTripViewModel?> GetFieldTripById(int id)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetFieldTripById(id);
            if (trip != null)
            {
                var fieldtrip = _mapper.Map<FieldTripViewModel>(trip);
                return fieldtrip;
            }
            return null;
        }
        public void Create(FieldTripViewModel entity)
        {
            var trip = _mapper.Map<FieldTrip>(entity);
            _unitOfWork.FieldTripRepository.Create(trip);
            _unitOfWork.Save();
        }

        public void Update(FieldTripViewModel entity)
        {
            var trip = _mapper.Map<FieldTrip>(entity);
            _unitOfWork.FieldTripRepository.Update(trip);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips()
        {
            var trip = await _unitOfWork.FieldTripRepository.GetAllFieldTrips();
            if(trip != null)
            {
                var fieldtrip = _mapper.Map<IEnumerable<FieldTripViewModel>>(trip);
                return fieldtrip;
            }
            return null;
        }
    }
}

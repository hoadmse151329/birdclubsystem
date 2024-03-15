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

        public async Task<IEnumerable<FieldTripViewModel>> GetAll()
        {
            string locationName;
            var listtrip = _unitOfWork.FieldTripRepository.GetAll();
            var listtripview = _mapper.Map<IEnumerable<FieldTripViewModel>>(listtrip);
            foreach (var itemview in listtripview)
            {
                foreach (var item in listtrip)
                {
                    if (item.TripId == itemview.TripId)
                    {
                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);
                        itemview.AreaNumber = locationName[0];
                        itemview.Street = locationName.Split(",")[1];
                        itemview.District = locationName.Split(",")[2];
                        itemview.City = locationName.Split(",")[3];
                    }
                }
            }
            return listtripview;
        }
        public async Task<FieldTripViewModel?> GetById(int id)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetFieldTripById(id);
            if (trip != null)
            {
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(trip.LocationId.Value);
                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.FieldTripParticipantRepository.GetCountFieldTripParticipantsByTripId(trip.TripId);
                var fieldTrip = _mapper.Map<FieldTripViewModel>(trip);
                fieldTrip.NumberOfParticipantsLimit = fieldTrip.NumberOfParticipants - partAmount;
                fieldTrip.Address = locationName;
                fieldTrip.AreaNumber = locationName[0];
                fieldTrip.Street = locationName.Split(",")[1];
                fieldTrip.District = locationName.Split(",")[2];
                fieldTrip.City = locationName.Split(",")[3];
                return fieldTrip;
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
    }
}

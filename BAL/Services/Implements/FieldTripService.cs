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
                if(itemview.FieldtripMedia != null && itemview.FieldtripMedia.Count > 0)
                {

                }
                /*var 
                locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);
                itemview.AreaNumber = Int32.Parse(locationName.Split(",")[0]);
                itemview.Street = locationName.Split(",")[1];
                itemview.District = locationName.Split(",")[2];
                itemview.City = locationName.Split(",")[3];*/

            }
            return listtripview;
        }
        public async Task<FieldTripViewModel?> GetById(int id)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetFieldTripById(id);
            if (trip != null)
            {
                var gettingThere = await _unitOfWork.FieldTripGettingThereRepository.GetFieldTripGettingTheresByTripId(trip.TripId);
                var addDetails = await _unitOfWork.FieldtripAdditionalDetailRepository.GetFieldTripAdditionalDetailsByTripId(trip.TripId);
                var daysByDays = await _unitOfWork.FieldTripDaybyDayRepository.GetAllFieldTripDayByDaysById(trip.TripId);
                var inclusions = await _unitOfWork.FieldTripInclusionRepository.GetFieldTripInclusionsById(trip.TripId);
                var media = await _unitOfWork.FieldTripMediaRepository.GetFieldTripMediasByTripId(trip.TripId);
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(trip.LocationId.Value);

                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.FieldTripParticipantRepository.GetCountFieldTripParticipantsByTripId(trip.TripId);
                var fieldTrip = _mapper.Map<FieldTripViewModel>(trip);


                fieldTrip.NumberOfParticipants = fieldTrip.NumberOfParticipantsLimit - partAmount;
                fieldTrip.Address = locationName;

                fieldTrip.FieldtripInclusions = (inclusions != null) ? _mapper.Map<IEnumerable<FieldtripInclusionViewModel>>(inclusions).ToList() : null;
                fieldTrip.FieldtripGettingTheres = (gettingThere != null) ? _mapper.Map<IEnumerable<FieldtripGettingThereViewModel>>(gettingThere).ToList() : null;
                fieldTrip.FieldtripDaybyDays = (daysByDays != null) ? _mapper.Map<IEnumerable<FieldtripDaybyDayViewModel>>(daysByDays).ToList() : null;

                fieldTrip.FieldtripAdditionalDetails = (addDetails != null) ? _mapper.Map<IEnumerable<FieldTripAdditionalDetailViewModel>>(addDetails).ToList() : null;

                fieldTrip.FieldtripMedia = (media != null) ? _mapper.Map<IEnumerable<FieldtripMediaViewModel>>(media).ToList() : null;

                fieldTrip.AreaNumber = Int32.Parse(locationName.Split(",")[0]);
                fieldTrip.Street = locationName.Split(",")[1];
                fieldTrip.District = locationName.Split(",")[2];
                fieldTrip.City = locationName.Split(",")[3];

                foreach(var picture in fieldTrip.FieldtripMedia)
                {
                    var day = fieldTrip.FieldtripDaybyDays.SingleOrDefault(f => f.DaybyDayId.Value.Equals(picture.DayByDayId.Value));
                    if(day != null)
                    {
                        day.Media.Add(picture);
                    }
                    
                }

                return fieldTrip;
            }
            return null;
        }
        public void Create(FieldTripViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address).Result;

            if (!loc.Equals(entity.Address.Trim()))
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address,
                    Description = loc.Description
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address).Result;
            }
            var trip = _mapper.Map<FieldTrip>(entity);
            trip.LocationId = loc.LocationId;
            _unitOfWork.FieldTripRepository.Create(trip);
            _unitOfWork.Save();
        }

        public void Update(FieldTripViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByTripId(entity.TripId.Value).Result;

            if (!loc.Equals(entity.Address.Trim()))
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address,
                    Description = loc.Description
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByTripId(entity.TripId.Value).Result;
            }
            var trip = _mapper.Map<FieldTrip>(entity);
            trip.LocationId = loc.LocationId;
            _unitOfWork.FieldTripRepository.Update(trip);
            _unitOfWork.Save();
        }
    }
}

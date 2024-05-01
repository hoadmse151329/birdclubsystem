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

        public async Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips(string? role)
        {
            string[] locationName;
            var listtrip = await _unitOfWork.FieldTripRepository.GetAllFieldTrips(role);
            var listtripview = _mapper.Map<IEnumerable<FieldTripViewModel>>(listtrip);
            foreach (var itemview in listtripview)
            {
                var trip = listtrip.SingleOrDefault(ft => ft.TripId == itemview.TripId);
                if(trip != null)
                {
                    var locationAddress = await _unitOfWork.LocationRepository.GetLocationNameById(trip.LocationId.Value);
                    itemview.Address = locationAddress;

                    locationName = itemview.Address.Split(",");
                    itemview.AreaNumber = Int32.Parse(locationName[0]);
                    itemview.Street = locationName[1];
                    itemview.District = locationName[2];
                    itemview.City = locationName[3];
                }
                if(itemview.FieldtripPictures.Count > 0)
                {
                    foreach (var picture in itemview.FieldtripPictures.ToList())
                    {
                        if (picture.Type == "Spotlight")
                        {
                            itemview.SpotlightImage = picture;
                            itemview.FieldtripPictures.Remove(picture);
                        }
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
                var locationName = await _unitOfWork.LocationRepository.GetLocationNameById(trip.LocationId.Value);
                var locationSplit = locationName.Split(",");

                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.FieldTripParticipantRepository.GetCountFieldTripParticipantsByTripId(trip.TripId);
                var fieldTrip = _mapper.Map<FieldTripViewModel>(trip);

                fieldTrip.NumberOfParticipants = fieldTrip.NumberOfParticipantsLimit - partAmount;

                fieldTrip.AreaNumber = Int32.Parse(locationSplit[0]);
                fieldTrip.Street = locationSplit[1];
                fieldTrip.District = locationSplit[2];
                fieldTrip.City = locationSplit[3];

                foreach(var picture in fieldTrip.FieldtripPictures.ToList())
                {
                    if(picture.Type == "Spotlight")
                    {
                        fieldTrip.SpotlightImage = picture;
                        fieldTrip.FieldtripPictures.Remove(picture);
                    } else
                    if (picture.Type == "LocationMap")
                    {
                        fieldTrip.LocationMapImage = picture;
                        fieldTrip.FieldtripPictures.Remove(picture);
                    } else
                    if(picture.Type == "DayByDay")
                    {
                        var day = fieldTrip.FieldtripDaybyDays.SingleOrDefault(f => f.DaybyDayId.Value.Equals(picture.DayByDayId.Value));
                        if (day != null)
                        {
                            day.Media.Add(picture);
                            fieldTrip.FieldtripPictures.Remove(picture);
                        }
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

        public async Task<bool> GetBoolFieldTripId(int id)
        {
            var trip = await _unitOfWork.FieldTripRepository.GetBoolFieldTripId(id);
            if (!trip) return false;
            return true;
        }
    }
}

using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string?>> GetListAvailableLocationCities()
        {
            IEnumerable<string?> result = await _unitOfWork.LocationRepository.GetAllLocationName();
            List<string?> resultCities = new List<string?>();
            foreach (var location in result)
            {
                var strList = location.Split(",");
                if (!resultCities.Contains(strList[strList.Length - 1]))
                    resultCities.Add(strList[strList.Length - 1]);
            }
            return resultCities;
        }

        public async Task<IEnumerable<string?>> GetListAvailableLocationDistricts()
        {
            IEnumerable<string?> result = await _unitOfWork.LocationRepository.GetAllLocationName();
            List<string?> resultCities = new List<string?>();
            foreach (var location in result)
            {
                var strList = location.Split(",");
                if (!resultCities.Contains(strList[strList.Length - 2]))
                    resultCities.Add(strList[strList.Length - 2]);
            }
            return resultCities;
        }

        public async Task<IEnumerable<string?>> GetListAvailableLocationNumbers()
        {
            IEnumerable<string?> result = await _unitOfWork.LocationRepository.GetAllLocationName();
            List<string?> resultCities = new List<string?>();
            foreach (var location in result)
            {
                var strList = location.Split(",");
                if (!resultCities.Contains(strList[strList.Length - 4]))
                    resultCities.Add(strList[strList.Length - 4]);
            }
            return resultCities;
        }

        public async Task<IEnumerable<string?>> GetListAvailableLocationRoads()
        {
            IEnumerable<string?> result = await _unitOfWork.LocationRepository.GetAllLocationName();
            List<string?> resultCities = new List<string?>();
            foreach (var location in result)
            {
                var strList = location.Split(",");
                if (!resultCities.Contains(strList[strList.Length - 3]))
                    resultCities.Add(strList[strList.Length - 3]);
            }
            return resultCities;
        }

        public async Task<IEnumerable<string?>> GetListAvailableLocations()
        {
            return await _unitOfWork.LocationRepository.GetAllLocationName();
        }

        public async Task<IEnumerable<LocationViewModel?>> GetListLocations()
        {
            return _mapper.Map<IEnumerable<LocationViewModel>>(_unitOfWork.LocationRepository.GetAll());
        }

        public async Task<LocationViewModel?> GetLocationById(int id)
        {
            return _mapper.Map<LocationViewModel>(_unitOfWork.LocationRepository.GetById(id));
        }
    }
}

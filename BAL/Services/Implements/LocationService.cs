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
        public async Task<IEnumerable<LocationViewModel?>> GetListLocation()
        {
            return _mapper.Map<IEnumerable<LocationViewModel>>(_unitOfWork.LocationRepository.GetAll());
        }

        public async Task<LocationViewModel?> GetLocationById(int id)
        {
            return _mapper.Map<LocationViewModel>(_unitOfWork.LocationRepository.GetById(id));
        }
    }
}

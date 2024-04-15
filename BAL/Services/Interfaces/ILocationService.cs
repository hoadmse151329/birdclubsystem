using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationViewModel?> GetLocationById(int id);
        Task<IEnumerable<LocationViewModel?>> GetListLocations();
        Task<IEnumerable<string?>> GetListAvailableLocations();
        Task<IEnumerable<string?>> GetListAvailableLocationRoads();
        Task<IEnumerable<string?>> GetListAvailableLocationDistricts();
        Task<IEnumerable<string?>> GetListAvailableLocationCities();
        Task<IEnumerable<string?>> GetListAvailableLocationNumbers();
    }
}

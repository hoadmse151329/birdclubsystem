using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripService
    {
        Task<FieldTripViewModel?> GetById(int id);
        Task<FieldTripViewModel?> GetByIdWithoutInclude(int id);
        Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips(string? role);
        Task<IEnumerable<FieldTripViewModel>?> GetSortedFieldTrips(
            int? tripId,
            string? tripName,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            List<string>? roads,
            List<string>? districts,
            List<string>? cities,
            List<string>? statuses,
            string? orderBy,
            bool isMemberOrGuest = false
            );
        void Create(FieldTripViewModel entity);
        void Update(FieldTripViewModel entity);
        bool UpdateGettingThere(FieldtripGettingThereViewModel entity);
        Task<bool> GetBoolFieldTripId(int id);
        bool UpdateMedia(FieldtripMediaViewModel entity);
    }
}

using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class ContestRepository : RepositoryBase<Contest>, IContestRepository
    {
        private readonly BirdClubContext _context;
        public ContestRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contest>> GetAllContests(string? role)
        {
            if (role == "Manager" || role == "Staff")
            {
                return _context.Contests.AsNoTracking().ToList();
            }
            return _context.Contests.AsNoTracking().Where(c => c.Status == "OpenRegistration").ToList();
        }
        public async Task<Contest?> GetContestById(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }

        public IEnumerable<Contest> GetSortedContests(
            int? contestId, 
            string? contestName,
            DateTime? registrationDeadline, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? numberOfParticipants, 
            int? reqMinElo, 
            int? reqMaxElo, 
            List<string>? roads, 
            List<string>? districts, 
            List<string>? cities, 
            List<string>? statuses, 
            string? orderBy, 
            bool isMemberOrGuest = false)
        {
            var roadLocationIds = roads != null && roads.Any() ? GetLocationIdListByLocationName(roads).ToList() : new List<int>();
            var districtLocationIds = districts != null && districts.Any() ? GetLocationIdListByLocationName(districts).ToList() : new List<int>();
            var cityLocationIds = cities != null && cities.Any() ? GetLocationIdListByLocationName(cities).ToList() : new List<int>();

            var contests = _context.Contests.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "OnHold", "OpenRegistration", "ClosedRegistration", "CheckingIn", "Ongoing", "Ended", "Postponed", "Cancelled" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "OpenRegistration" };
            }

            if (contestId.HasValue)
            {
                contests = contests.AsNoTracking().Where(m => m.ContestId.Equals(contestId.Value));
            }

            if (!string.IsNullOrEmpty(contestName))
            {
                contests = contests.AsNoTracking().Where(m => m.ContestName.Contains(contestName));
            }

            if (registrationDeadline.HasValue)
            {
                contests = contests.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline.Value);
            }
            if (startDate.HasValue)
            {
                contests = contests.AsNoTracking().Where(m => m.StartDate == startDate.Value);
            }
            if (endDate.HasValue)
            {
                contests = contests.AsNoTracking().Where(m => m.EndDate == endDate.Value);
            }
            if (numberOfParticipants.HasValue)
            {
                contests = contests.AsNoTracking().Where(m => m.NumberOfParticipants == numberOfParticipants.Value);
            }
            if(reqMinElo.HasValue && reqMaxElo.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.ReqMinELO >= reqMinElo.Value && c.ReqMaxELO <= reqMaxElo.Value);
            }
            if (roadLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(m => roadLocationIds.Contains(m.LocationId.Value));
            }
            if (districtLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(m => districtLocationIds.Contains(m.LocationId.Value));
            }

            if (cityLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(m => cityLocationIds.Contains(m.LocationId.Value));
            }
            if (statuses != null && statuses.Any())
            {
                contests = contests.AsNoTracking().Where(m => statuses.Contains(m.Status));
            }
            contests = orderBy switch
            {
                "contestname_asc" => contests.OrderBy(m => m.ContestName),
                "contestname_desc" => contests.OrderByDescending(m => m.ContestName),
                "openregistration_asc" => contests.OrderBy(m => m.OpenRegistration),
                "openregistration_desc" => contests.OrderByDescending(m => m.OpenRegistration),
                "registrationdeadline_asc" => contests.OrderBy(m => m.RegistrationDeadline),
                "registrationdeadline_desc" => contests.OrderByDescending(m => m.RegistrationDeadline),
                "startdate_asc" => contests.OrderBy(m => m.StartDate),
                "startdate_desc" => contests.OrderByDescending(m => m.StartDate),
                "enddate_asc" => contests.OrderBy(m => m.EndDate),
                "enddate_desc" => contests.OrderByDescending(m => m.EndDate),
                "reqminelo_asc" => contests.OrderBy(m => m.ReqMinELO),
                "reqminelo_desc" => contests.OrderByDescending(m => m.ReqMinELO),
                "reqmaxelo_asc" => contests.OrderBy(m => m.ReqMaxELO),
                "reqmaxelo_desc" => contests.OrderByDescending(m => m.ReqMaxELO),
                "status_asc" => contests.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => contests.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => contests.OrderBy(m => m.ContestId)
            };

            return contests.ToList();
        }

        public async Task<bool> GetBoolContestId(int id)
        {
            var con = _context.Contests.SingleOrDefault(c => c.ContestId == id);
            if (con != null) return true;
            else return false;
        }

        public async Task<Contest?> GetContestByIdTracking(int id)
        {
            return _context.Contests.SingleOrDefault(c => c.ContestId == id);
        }

        public async Task<Contest?> GetContestByIdWithoutInclude(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }
        private List<int> GetLocationIdListByLocationName(List<string>? locationNames)
        {
            var nameLocationList = new List<Location>();
            foreach (var locationName in locationNames)
            {
                var list = _context.Locations.AsNoTracking().Where(l => l.LocationName.Contains(locationName)).ToList();
                nameLocationList.AddRange(list);
            }
            return nameLocationList.Select(l => l.LocationId).ToList();
        }
    }
}

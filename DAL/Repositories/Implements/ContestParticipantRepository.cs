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
    public class ContestParticipantRepository : RepositoryBase<ContestParticipant>, IContestParticipantRepository
    {
        private readonly BirdClubContext _context;
        public ContestParticipantRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetCountContestParticipantsByContestId(int contestId)
        {
            return _context.ContestParticipants.Count(con => con.ContestId == contestId);
        }

        public async Task<int> GetCountContestParticipantsByBirdId(int birdId)
        {
            return _context.ContestParticipants.Count(b => b.BirdId == birdId);
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByContestId(int contestId)
        {
            return _context.ContestParticipants.Where(con => con.ContestId == contestId).Include(m => m.MemberDetail).ToList();
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdId(int birdId)
        {
            return _context.ContestParticipants.Where(b => b.BirdId == birdId).ToList();
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdIdInclude(int birdId)
        {
            return _context.ContestParticipants.Where(b => b.BirdId == birdId).Include(c => c.ContestDetail).ToList();
        }

		public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByMemberId(string memberId)
		{
			return _context.ContestParticipants.Where(cp => cp.MemberId == memberId).ToList();
		}

		public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByMemberIdInclude(string memberId)
		{
            return _context.ContestParticipants.AsNoTracking().Where(c => c.MemberId == memberId).Include(c => c.ContestDetail).ToList();
		}

		public async Task<int> GetCountContestParticipantsByMemberId(string memberId)
		{
			return _context.ContestParticipants.Count(b => b.MemberId == memberId);
		}

		public async Task<bool> GetBoolContestParticipantById(int contestId, string memberId, int? birdId = null)
		{
            ContestParticipant cp = null;
            if(birdId.HasValue)
            {
                cp = _context.ContestParticipants.FirstOrDefault(b => b.ContestId == contestId && b.MemberId == memberId && b.BirdId == birdId);
                if(cp != null)
                    return true;
                return false;
			}
			cp = _context.ContestParticipants.FirstOrDefault(b => b.ContestId == contestId && b.MemberId == memberId);
            if (cp != null) return true;
            return false;

		}

		public async Task<int> GetParticipationNoContestParticipantById(int contestId, string memberId, int? birdId = null)
		{
            var cp = _context.ContestParticipants.FirstOrDefault(cp => cp.ContestId == contestId && cp.MemberId == memberId);
            if (cp == null) return 0;
            return Int32.Parse(cp.ParticipantNo);
		}

		public async Task<ContestParticipant> GetContestParticipantById(int contestId, string memberId, int? birdId = null)
		{
            if (birdId != null) return _context.ContestParticipants.FirstOrDefault(b => b.ContestId == contestId && b.MemberId == memberId && b.BirdId == birdId);
			return _context.ContestParticipants.FirstOrDefault(b => b.ContestId == contestId && b.MemberId == memberId);
		}

        public async Task<IEnumerable<ContestParticipant>> UpdateAllContestParticipantStatus(List<ContestParticipant> part)
        {
            foreach (var participant in part)
            {
                var conpart = _context.ContestParticipants
                    .SingleOrDefault(p => p.ContestId == participant.ContestId && p.MemberId == participant.MemberId);
                if (conpart != null)
                {
                    if (conpart.CheckInStatus != participant.CheckInStatus)
                    {
                        conpart.CheckInStatus = participant.CheckInStatus;
                        _context.Update(conpart);
                    }
                }
            }
            return part;
        }
	}
}

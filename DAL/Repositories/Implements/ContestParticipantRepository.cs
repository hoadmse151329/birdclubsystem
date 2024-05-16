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
            return _context.ContestParticipants.Where(con => con.ContestId == contestId).Include(m => m.MemberDetail).Include(b => b.BirdDetails).OrderBy(p => p.ParticipantNo).ToList();
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

        public async Task<IEnumerable<ContestParticipant>> UpdateAllContestParticipantScore(List<ContestParticipant> part, bool isContestEnded = false)
        {
            var conpartList = _context.ContestParticipants.Include(b => b.BirdDetails).Where(c => c.ContestId.Equals(part.FirstOrDefault().ContestId));

            int n = part.Count;
            // Calculate the total points earned by all birds
            int totalPoints = part.Sum(c => c.Score);
            // Calculate the average Elo of all players
            double averageElo = conpartList.Sum(c => c.BirdDetails.Elo) / n;
            // List of Elo change factors
            List<int> Y = new List<int> { 40, 35, 30, 25, 20 }; // Adjust this list as needed

            foreach (var participant in part)
            {
                var conpart = conpartList.SingleOrDefault(p => p.ContestId == participant.ContestId && p.MemberId == participant.MemberId);
                if (conpart != null)
                {
                    if (conpart.Score != participant.Score)
                    {
                        conpart.Score = participant.Score;

                        // Calculate basic Elo change for the player based on the provided parameters
                        double basicEloChange = CalculateBasicEloChange(conpart.BirdDetails.Elo, averageElo, conpart.Score, totalPoints, n, Y);
                        // Update the player's Elo using the calculated Elo change
                        int updatedElo = UpdateElo(conpart.BirdDetails.Elo, (int)Math.Round(basicEloChange, MidpointRounding.AwayFromZero));

                        conpart.Elo = updatedElo;

                        _context.Update(conpart);
                    }
                    if (isContestEnded)
                    {
                        conpart.BirdDetails.Elo = conpart.Elo;
                        _context.Update(conpart);
                    }
                }
            }
            return part;
        }
        // playerCurBirdElo : Bird's current Elo
        // averageContestElo : Arithmetic Mean of all birds Elo (Arithmetic Mean) (Trung bình cộng)
        // Calculate Expected score
        private static double CalculateExpectedScore(int playerCurBirdElo, double averageContestElo)
        {
            // Calculate the expected score of a player based on their Elo and the average Elo of all players
            return 1 / (1 + Math.Pow(10, ((averageContestElo - playerCurBirdElo) / 400)));
        }

        // playerCurBirdElo : Bird's current Elo
        // averageContestElo : Arithmetic Mean of all birds Elo (Arithmetic Mean) (Trung bình cộng)
        // birdContestPoints : Bird's earned score from contest
        // totalbirdContestPoints : Arithmetic Mean of all birds Elo earned from Contest
        // totalAmountOfBird : Amount of Bird joined the Contest
        // Y : 
        // Calculate Basic Elo Change
        private static double CalculateBasicEloChange(int playerCurBirdElo, double averageContestElo, int birdContestPoints, int totalbirdContestPoints, int totalAmountOfBird, List<int> Y)
        {
            // Calculate the expected score of the player
            int expected = (int)Math.Round( CalculateExpectedScore(playerCurBirdElo, averageContestElo), MidpointRounding.AwayFromZero);
            // Determine if the player won or lost the match based on bird points
            int result = birdContestPoints > totalbirdContestPoints / totalAmountOfBird ? 1 : 0;

            // Adjustment Factor Logic
            double adjustmentFactor;
            if (result == 1)
            {
                // If the player won, adjust the factor based on whether their Elo is higher or lower than the average
                adjustmentFactor = playerCurBirdElo > averageContestElo ? 0.5 : 1.5; // Add points
            }
            else
            {
                // If the player lost, adjust the factor based on whether their Elo is higher or lower than the average
                adjustmentFactor = playerCurBirdElo > averageContestElo ? 1.5 : 0.5; // Subtract points
            }

            // Calculate K factor
            double K = adjustmentFactor > 1 ? 1.5 : 0.5;
            // Calculate basic Elo change using the Elo change factor and the difference between expected and actual results
            double basicEloChange = Y[0] * (result - expected);

            // Return the basic Elo change with adjustments
            return basicEloChange * adjustmentFactor * K;
        }

        // Update Elo
        private static int UpdateElo(int originalElo, int realEloChange)
        {
            // Update the player's Elo rating based on the calculated real Elo change
            return originalElo + realEloChange;
        }
    }
}

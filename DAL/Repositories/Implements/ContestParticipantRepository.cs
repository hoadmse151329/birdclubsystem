﻿using DAL.Infrastructure;
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

        public async Task<bool> GetBoolContestParticipantById(int contestId, int birdId)
        {
            var birdpart = _context.ContestParticipants.Find(contestId, birdId);
            if (birdpart != null) return true;
            return false;
        }

        public async Task<int> GetCountContestParticipantsByContestId(int contestId)
        {
            return _context.ContestParticipants.Count(con => con.ContestId == contestId);
        }

        public async Task<int> GetCountContestParticipantsByBirdId(int birdId)
        {
            return _context.ContestParticipants.Count(b => b.BirdId == birdId);
        }

        public async Task<ContestParticipant> GetContestParticipantById(int contestId, int birdId)
        {
            return _context.ContestParticipants.Find(contestId, birdId);
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByContestId(int contestId)
        {
            return _context.ContestParticipants.Where(con => con.ContestId == contestId).ToList();
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdId(int birdId)
        {
            return _context.ContestParticipants.Where(b => b.BirdId == birdId).ToList();
        }

        public async Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdIdInclude(int birdId)
        {
            return _context.ContestParticipants.Where(b => b.BirdId == birdId).Include(c => c.Contest).ToList();
        }

        public async Task<int> GetParticipationNoContestParticipantById(int contestId, int birdId)
        {
            var birdpart = _context.ContestParticipants.Find(contestId, birdId);
            if (birdpart != null) return Int32.Parse(birdpart.ParticipantNo);
            return 0;
        }
    }
}

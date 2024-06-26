﻿using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IContestMediaRepository : IRepositoryBase<ContestMedia>
    {
        Task<ContestMedia> GetContestMediaById(int pictureId);
        Task<ContestMedia> GetContestMediaByIdTracking(int pictureId);
        Task<IEnumerable<ContestMedia>> GetContestMediasByContestId(int contestId);
        Task<ContestMedia> GetContestMediaByContestIdAndType(int contestId, string mediaType);
    }
}

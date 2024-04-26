﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IBirdRepository : IRepositoryBase<Bird>
    {
        Task<IEnumerable<Bird>> GetBirdsByMemberId(string memberId);
        Task<IEnumerable<Bird>> GetBirdsByMemberIdInclude(string memberId);
    }
}

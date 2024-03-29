﻿using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripInclusionRepository : IRepositoryBase<FieldtripInclusion>
    {
        Task<IEnumerable<FieldtripInclusion>> GetFieldTripInclusionsById(int tripId);
    }
}

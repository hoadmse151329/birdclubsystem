﻿using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetByLogin(string userName, string passWord);
        Task<User?> GetByIdNoTracking(int id);
        Task<User?> GetByEmail(string email);

    }
}

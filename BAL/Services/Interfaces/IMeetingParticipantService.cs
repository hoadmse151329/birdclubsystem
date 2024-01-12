﻿using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingParticipantService
    {
        Task<IEnumerable<MeetingParticipantViewModel>> GetAll();
        Task<int> Create(int memId, int metId);
        Task<bool> Delete(int memId, int metId);
    }
}

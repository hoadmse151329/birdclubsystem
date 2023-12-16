﻿using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingMediaService
    {
        IEnumerable<MeetingMediaViewModel> GetAll();
    }
}

﻿using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryViewModel>> GetAllGalleries();
    }
}

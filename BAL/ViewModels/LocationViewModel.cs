﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class LocationViewModel
    {
        public int? LocationId { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
    }
}

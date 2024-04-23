using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Member
{
    public class UpdateMemberAvatar
    {
        public string? MemberId { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

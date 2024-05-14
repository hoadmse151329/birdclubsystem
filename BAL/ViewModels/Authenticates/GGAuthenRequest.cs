using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Authenticates
{
    public class GGAuthenRequest
    {
        public string Email { get; set; }

        [PasswordPropertyText]
        public string? ImagePath { get; set; }
    }
}

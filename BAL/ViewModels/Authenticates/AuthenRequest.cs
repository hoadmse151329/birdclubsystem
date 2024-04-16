using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Authenticates
{
    public class AuthenRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string? ImagePath { get; set; }
    }
}

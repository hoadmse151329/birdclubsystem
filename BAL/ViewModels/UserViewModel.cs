using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class UserViewModel
    {
        public int? UserId { get; set; }
        public int? ClubId { get; set; }
        public string? UserName { get; set; }
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? MemberId { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? ImagePath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Member
{
    public class UpdateMemberPassword
    {

        [Required]
        public string userId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string password { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "New Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Newpassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "New Confirm Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string NewConfirmPassword { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
    }
}

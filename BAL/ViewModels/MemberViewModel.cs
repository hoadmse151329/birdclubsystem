using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MemberViewModel
    {
        public int? MemberId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Address is invalid")]
        public string? Address { get; set; }
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}

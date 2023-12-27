using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Member
{
	public class CreateNewMember
	{
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username is invalid")]
		public string? UserName { get; set; }
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
		public string? FullName { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
		public string? Email { get; set; }
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
		public string? Gender { get; set; }
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Address is invalid")]
		public string? Address { get; set; }
		[Phone]
		[DataType(DataType.PhoneNumber)]
		public string? Phone { get; set; }
		[PasswordPropertyText]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; }
		[PasswordPropertyText]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Confirm Password is required")]
		public string? ConfirmPassword { get; set; }
	}
}

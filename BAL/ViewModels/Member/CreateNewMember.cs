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
		[Required(ErrorMessage = "Account Username is required")]
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username is invalid")]
		public string? UserName { get; set; }
		[Required(ErrorMessage = "Full Name is required")]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name is invalid")]
		public string? FullName { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is required")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Gender is required")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
		public string? Gender { get; set; }
		[Required(ErrorMessage = "Address is required")]
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
		public string? Address { get; set; }
		[Phone]
		[Required(ErrorMessage = "Phone Number is required")]
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
		[DataType(DataType.Currency)]
		[Range(1, int.MaxValue, ErrorMessage = "Payment Amount must be more than 0")]
		[Required(ErrorMessage = "Payment Amount is Required")]
		public decimal PayAmount { get; set; }
	}
}

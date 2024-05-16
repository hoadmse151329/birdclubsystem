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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Username is required")]
        [StringLength(20, ErrorMessage = "Username must have more than or equal 6 characters and less than or equal 20 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string? UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must have more than or equal 6 characters and less than or equal 50 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string? FullName { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Gender is required")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
		public string? Gender { get; set; }
		/*[Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
		public string? Address { get; set; }*/
		[Phone(ErrorMessage = "Please enter a valid Phone No")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
		[DataType(DataType.PhoneNumber)]
		public string? Phone { get; set; }
        [PasswordPropertyText]
        [Compare(otherProperty: "Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        public string? ConfirmPassword { get; set; }
        [PasswordPropertyText]
		[DataType(DataType.Password)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
		public string? Password { get; set; }
		[DataType(DataType.Currency)]
		[Range(1, int.MaxValue, ErrorMessage = "Payment Amount must be more than 0")]
		[Required(ErrorMessage = "Payment Amount is Required")]
		public decimal PayAmount { get; set; }
		public string? ImagePath { get; set; }
	}
}

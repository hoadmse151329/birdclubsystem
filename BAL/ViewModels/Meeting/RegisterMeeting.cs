using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Meeting
{
	public class RegisterMeeting
	{
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username is invalid")]
		public string? UserName { get; set; }
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
		public string? FullName { get; set; }
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
		public string? Gender { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
		public string? Email { get; set; }
		[Phone]
		[DataType(DataType.PhoneNumber)]
		public string? PhoneNumber { get; set; }
	}
}

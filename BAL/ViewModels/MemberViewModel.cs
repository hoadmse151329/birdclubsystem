﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MemberViewModel
    {
        public string? MemberId { get; set; }
		[Required(ErrorMessage = "Account Username is required")]
		[RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string UserName { get; set; }
		[Required(ErrorMessage = "Full Name is required")]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string FullName { get; set; }
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
		/*[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Address is invalid")]*/
		public string? Address { get; set; }
		[Phone]
		[Required(ErrorMessage = "Phone Number is required")]
		[DataType(DataType.PhoneNumber)]
		public string? Phone { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}

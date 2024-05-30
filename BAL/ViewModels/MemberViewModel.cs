using AutoMapper.Configuration.Annotations;
using BAL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MemberViewModel
    {
		public MemberViewModel()
		{
			Birds = new List<BirdViewModel>();
			DefaultUserGenderSelectList = new List<SelectListItem>() {
				new SelectListItem() { Text = "Gender", Value = ""},
				new SelectListItem { Text = "Male", Value = "Male" },
				new SelectListItem { Text = "Female", Value = "Female" },
				new SelectListItem { Text = "Other", Value = "Other" },
			};
        }
		public int UserId { get; set; }
        public string? MemberId { get; set; }
		[Required(ErrorMessage = "Account Username is required")]
        [StringLength(20, ErrorMessage = "Username must have more than or equal 6 characters and less than or equal 20 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
		[DisplayName("Username")]
        public string? UserName { get; set; }
		[Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must have more than or equal 6 characters and less than or equal 50 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is required")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
		[DisplayName("Email Address")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Gender is required")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
		[DisplayName("Gender")]
		public string? Gender { get; set; }
		[Required(ErrorMessage = "Address is required")]
		[RegularExpression(@"^[a-zA-Z0-9\/?\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z0-9\s?]+,[a-zA-Z\s?]{4,}$", ErrorMessage = "Address is Invalid, it must be writen in this format: Area Number,Street,District,City")]
		[DisplayName("Address")]
		public string? Address { get; set; }
		[Phone]
		[Required(ErrorMessage = "Phone Number is required")]
		[DataType(DataType.PhoneNumber)]
		[DisplayName("Phone Number")]
		public string? Phone { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

		public List<BirdViewModel> Birds { get; set; }
		public List<SelectListItem> DefaultUserGenderSelectList { get; set; }
    }
}

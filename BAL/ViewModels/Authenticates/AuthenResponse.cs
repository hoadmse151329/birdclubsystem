using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Authenticates
{
    public class AuthenResponse
    {
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string UserId { get; set; }
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string? AccessToken { get; set; }
        public string? ImagePath { get; set; }
		public string? Status { get; set; }
	}
}

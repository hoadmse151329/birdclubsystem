﻿using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Auth
{
	public class GetAuthenResponse
	{
		public bool Status { get; set; }
		public AuthenResponse Data { get; set; }
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}
}

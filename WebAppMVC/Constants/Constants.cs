namespace WebAppMVC.Constants
{
	public static class Constants
	{
		public static string ADMIN_URL = "../Admin/AdminIndex";
		public static string MEMBER_URL = "../Home/Index";
		public static string MANAGER_URL = "../Manager/Index";
		public static string STAFF_URL = "../Staff/Index";
		public static string NOTFOUND_URL = "../Auth/NotFound";
		public static string LOGIN_URL = "../Auth/Login";
		public static string NEW_MEMBER_CONFIRM_REGISTRATION_URL = "../Auth/SignUp";

		public static string NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE = "New-Membership-Registration";
		public static string CREATE_MEETING_VALID = "CMeetingValid";
        public static string UPDATE_MEETING_VALID = "UMeetingValid";
        public static string CREATE_FIELDTRIP_VALID = "CFieldTripValid";
        public static string ADMIN = "Admin";
		public static string MEMBER = "Member";
		public static string TEMPMEMBER = "TempMember";
		public readonly static string STAFF = "Staff";
		public readonly static string MANAGER = "Manager";
		public static string GET_METHOD = "GET";
        public static string POST_METHOD = "POST";
		public static string PUT_METHOD = "PUT";
		public static string DELETE_METHOD = "DELETE";
    }
}

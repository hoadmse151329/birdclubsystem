namespace WebAppMVC.Constants
{
	public static class Constants
	{
		public static string ADMIN_URL = "../Admin/Index";
		public static string MEMBER_URL = "../Home/Index";
		public static string MANAGER_URL = "../Manager/Index";
		public static string STAFF_URL = "../Staff/Index";
		public static string NOTFOUND_URL = "../Auth/NotFound";
		public static string LOGIN_URL = "../Auth/Login";
		public static string NEW_MEMBER_CONFIRM_REGISTRATION_URL = "../Auth/SignUp";

		public static string NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE = "New-Membership-Registration";
		public static string MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_TYPE = "Member-FieldTrip-Registration";

		public static string CREATE_MEETING_VALID = "CMeetingValid";
        public static string UPDATE_MEETING_VALID = "UMeetingValid";
        public static string CREATE_FIELDTRIP_VALID = "CFieldTripValid";
        public static string UPDATE_FIELDTRIP_VALID = "UFieldTripValid";
        public static string UPDATE_FIELDTRIP_GETTHERE_VALID = "UFieldTripGettingThereValid";
        public static string CREATE_FIELDTRIP_DAYBYDAY_VALID = "CFieldTripDayByDayValid";
        public static string UPDATE_FIELDTRIP_DAYBYDAY_VALID = "UFieldTripDayByDayValid";
        public static string CREATE_FIELDTRIP_INCLUSION_VALID = "CFieldTripInclusionValid";
        public static string UPDATE_FIELDTRIP_INCLUSION_VALID = "UFieldTripInclusionValid";
        public static string CREATE_FIELDTRIP_TOURFEATURES_VALID = "CFieldTripTourFeaturesValid";
        public static string UPDATE_FIELDTRIP_TOURFEATURES_VALID = "UFieldTripTourFeaturesValid";
        public static string CREATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "CFieldTripImportantValid";
        public static string UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "UFieldTripImportantValid";
        public static string CREATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "CFieldTripActAndTrasValid";
        public static string UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "UFieldTripActAndTrasValid";
        public static string CREATE_CONTEST_VALID = "CContestValid";
        public static string UPDATE_CONTEST_VALID = "UContestValid";

		public static string EVENT_STATUS_ON_HOLD = "OnHold";
        public static string EVENT_STATUS_NAME_ON_HOLD = "On Hold";
        public static string EVENT_STATUS_POSTPONED = "Postponed";
        public static string EVENT_STATUS_NAME_POSTPONED = "Postponed";
        public static string EVENT_STATUS_CANCELLED = "Cancelled";
        public static string EVENT_STATUS_NAME_CANCELLED = "Cancelled";
        public static string EVENT_STATUS_ENDED = "Ended";
        public static string EVENT_STATUS_NAME_ENDED = "Ended";
        public static string EVENT_STATUS_OPEN_REGISTRATION = "OpenRegistration";
        public static string EVENT_STATUS_NAME_OPEN_REGISTRATION = "Open Registration";
        public static string EVENT_STATUS_CLOSED_REGISTRATION = "ClosedRegistration";
        public static string EVENT_STATUS_NAME_CLOSED_REGISTRATION = "Closed Registration";
        public static string EVENT_STATUS_CHECKING_IN = "CheckingIn";
        public static string EVENT_STATUS_NAME_CHECKING_IN = "Checking In";
        public static string EVENT_STATUS_ONGOING = "Ongoing";
        public static string EVENT_STATUS_NAME_ONGOING = "Ongoing";

        public static string EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN = "Not Checked-In";
        public static string EVENT_PARTICIPANT_STATUS_CHECKED_IN = "Checked-In";

        public static string FIELDTRIP_INCLUSION_TYPE_INCLUDED = "Included";
        public static string FIELDTRIP_INCLUSION_TYPE_EXCLUDED = "Excluded";

        public static string ROLE_NAME = "ROLE_NAME";
        public static string ACC_TOKEN = "ACCESS_TOKEN";
        public static string USR_ID = "USER_ID";
        public static string USR_NAME = "USER_NAME";
        public static string USR_IMAGE = "IMAGE_PATH";

        public static string ADMIN = "Admin";
		public static string MEMBER = "Member";
		public static string TEMPMEMBER = "TempMember";
        public static string GUEST = "Guest";
		public readonly static string STAFF = "Staff";
		public readonly static string MANAGER = "Manager";
		public static string GET_METHOD = "GET";
        public static string POST_METHOD = "POST";
		public static string PUT_METHOD = "PUT";
		public static string DELETE_METHOD = "DELETE";
    }
}

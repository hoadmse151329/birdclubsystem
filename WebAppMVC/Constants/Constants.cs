using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebAppMVC.Constants
{
	public static class Constants
	{
		public readonly static string ADMIN_URL = "../Admin/Index";
		public readonly static string MEMBER_URL = "../Home/Index";
		public readonly static string MANAGER_URL = "../Manager/Index";
		public readonly static string STAFF_URL = "../Staff/Index";
		public readonly static string NOTFOUND_URL = "../Auth/NotFound";
		public readonly static string LOGIN_URL = "../Auth/Login";
		public readonly static string NEW_MEMBER_CONFIRM_REGISTRATION_URL = "../Auth/SignUp";

        public readonly static string NEW_MEMBER_REGISTRATION_COOKIE = "New-Membership-Registration-Cookie";
        public readonly static string NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE = "New-Membership-Registration-Transaction-Cookie";
        public readonly static string MEMBER_FIELDTRIP_REGISTRATION_COOKIE = "Member-FieldTrip-Registration-Cookie";
        public readonly static string MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_COOKIE = "Member-FieldTrip-Registration-Transaction-Cookie";
        public readonly static string MEMBER_CONTEST_REGISTRATION_COOKIE = "Member-Contest-Registration-Cookie";
        public readonly static string MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE = "Member-Contest-Bird-Registration-Cookie";
        public readonly static string MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE = "Member-Contest-Registration-Transaction-Cookie";

        public readonly static string NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE = "New-Membership-Registration";
		    public readonly static string MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_TYPE = "Member-FieldTrip-Registration";
        public readonly static string MEMBER_CONTEST_REGISTRATION_TRANSACTION_TYPE = "Member-Contest-Registration";
    
        public readonly static string CREATE_BIRD_VALID = "CBirdValid";
        public readonly static string UPDATE_BIRD_VALID = "UBirdValid";
        public readonly static string CREATE_MEETING_VALID = "CMeetingValid";
        public readonly static string UPDATE_MEETING_VALID = "UMeetingValid";
        public readonly static string CREATE_MEETING_MEDIA_VALID = "CMeetingMediaValid";
        public readonly static string UPDATE_MEETING_MEDIA_VALID = "UMeetingMediaValid";
        public readonly static string CREATE_FIELDTRIP_VALID = "CFieldTripValid";
        public readonly static string UPDATE_FIELDTRIP_VALID = "UFieldTripValid";
        public readonly static string UPDATE_FIELDTRIP_GETTHERE_VALID = "UFieldTripGettingThereValid";
        public readonly static string CREATE_FIELDTRIP_DAYBYDAY_VALID = "CFieldTripDayByDayValid";
        public readonly static string UPDATE_FIELDTRIP_DAYBYDAY_VALID = "UFieldTripDayByDayValid";
        public readonly static string CREATE_FIELDTRIP_INCLUSION_VALID = "CFieldTripInclusionValid";
        public readonly static string UPDATE_FIELDTRIP_INCLUSION_VALID = "UFieldTripInclusionValid";
        public readonly static string CREATE_FIELDTRIP_TOURFEATURES_VALID = "CFieldTripTourFeaturesValid";
        public readonly static string UPDATE_FIELDTRIP_TOURFEATURES_VALID = "UFieldTripTourFeaturesValid";
        public readonly static string CREATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "CFieldTripImportantValid";
        public readonly static string UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "UFieldTripImportantValid";
        public readonly static string CREATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "CFieldTripActAndTrasValid";
        public readonly static string UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "UFieldTripActAndTrasValid";
        public readonly static string CREATE_CONTEST_VALID = "CContestValid";
        public readonly static string UPDATE_CONTEST_VALID = "UContestValid";

		    public readonly static string EVENT_STATUS_ON_HOLD = "OnHold";
        public readonly static string EVENT_STATUS_NAME_ON_HOLD = "On Hold";
        public readonly static string EVENT_STATUS_POSTPONED = "Postponed";
        public readonly static string EVENT_STATUS_NAME_POSTPONED = "Postponed";
        public readonly static string EVENT_STATUS_CANCELLED = "Cancelled";
        public readonly static string EVENT_STATUS_NAME_CANCELLED = "Cancelled";
        public readonly static string EVENT_STATUS_ENDED = "Ended";
        public readonly static string EVENT_STATUS_NAME_ENDED = "Ended";
        public readonly static string EVENT_STATUS_OPEN_REGISTRATION = "OpenRegistration";
        public readonly static string EVENT_STATUS_NAME_OPEN_REGISTRATION = "Open Registration";
        public readonly static string EVENT_STATUS_CLOSED_REGISTRATION = "ClosedRegistration";
        public readonly static string EVENT_STATUS_NAME_CLOSED_REGISTRATION = "Closed Registration";
        public readonly static string EVENT_STATUS_CHECKING_IN = "CheckingIn";
        public readonly static string EVENT_STATUS_NAME_CHECKING_IN = "Checking In";
        public readonly static string EVENT_STATUS_ONGOING = "Ongoing";
        public readonly static string EVENT_STATUS_NAME_ONGOING = "Ongoing";

        public readonly static string BIRD_STATUS_AVAILABLE = "Available";
        public readonly static string BIRD_STATUS_INJURED = "Injured";
        public readonly static string BIRD_STATUS_UNAVAILABLE = "Inactive";

        public readonly static string EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN = "Not Checked-In";
        public readonly static string EVENT_PARTICIPANT_STATUS_CHECKED_IN = "Checked-In";

        public readonly static string FIELDTRIP_INCLUSION_TYPE_INCLUDED = "Included";
        public readonly static string FIELDTRIP_INCLUSION_TYPE_EXCLUDED = "Excluded";

        public readonly static string ROLE_NAME = "ROLE_NAME";
        public readonly static string ACC_TOKEN = "ACCESS_TOKEN";
        public readonly static string USR_ID = "USER_ID";
        public readonly static string USR_NAME = "USER_NAME";
        public readonly static string USR_IMAGE = "IMAGE_PATH";

        public readonly static string ADMIN = "Admin";
		public readonly static string MEMBER = "Member";
		public readonly static string TEMPMEMBER = "TempMember";
        public readonly static string GUEST = "Guest";
		public readonly static string STAFF = "Staff";
		public readonly static string MANAGER = "Manager";
		public readonly static string GET_METHOD = "GET";
        public readonly static string POST_METHOD = "POST";
		public readonly static string PUT_METHOD = "PUT";
		public readonly static string DELETE_METHOD = "DELETE";
    }
}

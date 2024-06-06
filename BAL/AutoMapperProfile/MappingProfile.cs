using AutoMapper;
using BAL.ViewModels;
using BAL.ViewModels.Admin;
using BAL.ViewModels.Blog;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
using BAL.ViewModels.News;
using DAL.Models;

namespace BAL.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                /*.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Member.FullName))*/
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.AllowNull();
                    opt.MapFrom(src => src.MemberDetails != null ? src.MemberDetails.Email : "");
                })
                .ReverseMap();
            CreateMap<Member, MemberViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.UserId = src.UserDetails.UserId;
                    if (src.UserDetails != null && src.UserDetails.ImagePath != null)
                    {
                        dest.ImagePath = src.UserDetails.ImagePath;
                    }
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.UserDetails = new();
                    dest.UserDetails.ImagePath = src.ImagePath;
                })
                ;
            CreateMap<Member, GetMemberStatus>().ReverseMap();
            CreateMap<Member, GetEmployeeStatus>().ReverseMap();
            CreateMap<Member, GetMembershipExpire>().ReverseMap();
            CreateMap<Member, GetStaffName>().ReverseMap();
            CreateMap<MeetingParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.MeetingId;
                    dest.EventIdFull = "meeting" + src.MeetingId;
                    dest.EventName = src.MeetingDetails.MeetingName;
                    dest.EventType = "Meeting";
                    dest.EventHost = src.MeetingDetails.Host;
                    dest.EventStaff = src.MeetingDetails.Incharge;
                    dest.StartDate = src.MeetingDetails.StartDate;
                    dest.EndDate = src.MeetingDetails.EndDate;
                    dest.Fee = 0;
                    dest.RegistrationDeadline = src.MeetingDetails.RegistrationDeadline;
                    dest.Status = src.MeetingDetails.Status;
                    dest.ParticipationNo = src.ParticipantNo;
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            /*.AfterMap((src, dest) =>
            {
                dest.MeetingId = src.MeetingId;
                dest.MeetingName = src.Meeting.MeetingName;
                dest.StartDate = src.Meeting.StartDate;
                dest.EndDate = src.Meeting.EndDate;
                dest.RegistrationDeadline = src.Meeting.RegistrationDeadline;
                dest.Status = src.Meeting.Status == 0 ? "Inactive" : "Active";
                dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                dest.Incharge = src.Meeting.Incharge;
            })*/
            ;
            CreateMap<CreateNewMeetingVM, Meeting>();
            CreateMap<UpdateMeetingDetailsVM, Meeting>();
            CreateMap<CreateNewContestVM, Contest>();
            CreateMap<UpdateContestDetailsVM, Contest>();
            CreateMap<CreateNewFieldtripVM, FieldTrip>();
            CreateMap<UpdateFieldtripDetailsVM, FieldTrip>();
            CreateMap<FieldTripParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.TripId;
                    dest.EventIdFull = "fieldtrip" + src.TripId;
                    dest.EventName = src.Trip.TripName;
                    dest.EventType = "FieldTrip";
                    dest.EventHost = src.Trip.Host;
                    dest.EventStaff = src.Trip.InCharge;
                    dest.StartDate = src.Trip.StartDate;
                    dest.EndDate = src.Trip.EndDate;
                    dest.RegistrationDeadline = src.Trip.RegistrationDeadline;
                    dest.Status = src.Trip.Status;
                    dest.Fee = src.Trip.Fee;
                    dest.ParticipationNo = src.ParticipantNo;
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            CreateMap<ContestParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.ContestId;
                    dest.EventIdFull = "contest" + src.ContestId;
                    dest.EventName = src.ContestDetails.ContestName;
                    dest.EventType = "Contest";
                    dest.EventHost = src.ContestDetails.Host;
                    dest.EventStaff = src.ContestDetails.Incharge;
                    dest.StartDate = src.ContestDetails.StartDate;
                    dest.EndDate = src.ContestDetails.EndDate;
                    dest.RegistrationDeadline = src.ContestDetails.RegistrationDeadline;
                    dest.Status = src.ContestDetails.Status;
                    dest.Fee = src.ContestDetails.Fee;
                    dest.ParticipationNo = src.ParticipantNo;
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            CreateMap<MeetingParticipant, MeetingParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.MemberDetails.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetails = new();
                    dest.MemberDetails.FullName = src.MemberName;
                })
                ;
            CreateMap<FieldTripParticipant, FieldTripParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.MemberDetails.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetails = new();
                    dest.MemberDetails.FullName = src.MemberName;
                });
            CreateMap<ContestParticipant, ContestParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.ContestElo = src.Elo;
                    if (src.BirdDetails != null)
                    {
                        dest.ParticipantElo = src.BirdDetails.Elo.Value;
                    }
                    dest.MemberName = src.MemberDetails.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetails = new();
                    if (src.ContestElo != null)
                    {
                        dest.Elo = src.ContestElo.Value;
                    }
                    if (dest.BirdDetails != null)
                    {
                        dest.BirdDetails.Elo = src.ParticipantElo;
                    }
                    dest.MemberDetails.FullName = src.MemberName;
                });
            CreateMap<Meeting, MeetingViewModel>()
                .ReverseMap();
            CreateMap<MeetingMedia, MeetingMediaViewModel>()
                .ReverseMap();
            CreateMap<FieldTrip, FieldTripViewModel>()
                .ReverseMap();
            CreateMap<FieldtripDaybyDay, FieldtripDaybyDayViewModel>()
                .ReverseMap();
            CreateMap<FieldtripInclusion, FieldtripInclusionViewModel>()
                .ReverseMap();
            CreateMap<FieldtripGettingThere, FieldtripGettingThereViewModel>()
                .ReverseMap();
            CreateMap<FieldtripAdditionalDetail, FieldTripAdditionalDetailViewModel>()
                .ReverseMap();
            CreateMap<FieldtripMedia, FieldtripMediaViewModel>()
                .ReverseMap();
            CreateMap<Contest, ContestViewModel>()
                .ReverseMap();
            CreateMap<ContestMedia, ContestMediaViewModel>()
                .ReverseMap();
            CreateMap<Location, LocationViewModel>()
                .AfterMap((src, dest) =>
                {
                    string[] address = src.LocationName.Split(',');
                    dest.AreaNumber = address[0];
                    dest.Street = address[1];
                    dest.District = address[2];
                    dest.City = address[3];
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.LocationName = src.AreaNumber + "," + src.Street + "," + src.District + "," + src.City;
                });
            CreateMap<Transaction, TransactionViewModel>()
                .ReverseMap();
            CreateMap<Bird, BirdViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<Blog, BlogViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Fullname = src.UserDetails.MemberDetails.FullName;
                    dest.MemberAvatar = src.UserDetails.ImagePath;
                })
                .ReverseMap();
            CreateMap<CreateNewBlog, Blog>();
            CreateMap<Comment, CommentViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.UserFullName = src.UserDetails.MemberDetails.FullName;
                })
                .ReverseMap();
            CreateMap<News, NewsViewModel>().ReverseMap();
            CreateMap<News, CreateNewNews>().ReverseMap();
            CreateMap<Feedback, GetFeedbackResponse>()
                .AfterMap((src, dest) =>
                {
                    dest.Fullname = src.UserDetails.MemberDetails.FullName;
                    dest.AvatarImage = src.UserDetails.ImagePath;
                })
                .ReverseMap();
        }
    }
}
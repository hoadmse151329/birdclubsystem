using AutoMapper;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    opt.MapFrom(src => src.MemberDetail != null ? src.MemberDetail.Email : "");
                })
                .ReverseMap();
            CreateMap<Member, MemberViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.UserId = src.UserDetail.UserId;
                    if(src.UserDetail != null && src.UserDetail.ImagePath != null)
                    {
                        dest.ImagePath = src.UserDetail.ImagePath;
                    }
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.UserDetail.ImagePath = src.ImagePath;
                })
                ;
            CreateMap<Member, GetMemberStatus>().ReverseMap();
            CreateMap<MeetingParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.MeetingId;
                    dest.EventName = src.MeetingDetail.MeetingName;
                    dest.EventType = "Meeting";
                    dest.EventHost = src.MeetingDetail.Host;
                    dest.EventStaff = src.MeetingDetail.Incharge;
                    dest.StartDate = src.MeetingDetail.StartDate;
                    dest.EndDate = src.MeetingDetail.EndDate;
                    dest.Fee = 0;
                    dest.RegistrationDeadline = src.MeetingDetail.RegistrationDeadline;
                    dest.Status = src.MeetingDetail.Status;
                    dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
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
                })*/;
            CreateMap<FieldTripParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.TripId;
                    dest.EventName = src.Trip.TripName;
                    dest.EventType = "FieldTrip";
                    dest.EventHost = src.Trip.Host;
                    dest.EventStaff = src.Trip.InCharge;
                    dest.StartDate = src.Trip.StartDate;
                    dest.EndDate = src.Trip.EndDate;
                    dest.RegistrationDeadline = src.Trip.RegistrationDeadline;
                    dest.Status = src.Trip.Status;
                    dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            CreateMap<MeetingParticipant, MeetingParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.MemberDetail.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetail = new();
                    dest.MemberDetail.FullName = src.MemberName;
                })
                ;
            CreateMap<FieldTripParticipant, FieldTripParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.Member.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.Member = new();
                    dest.Member.FullName = src.MemberName;
                })
                ;
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
                .AfterMap((src, dest) =>
                {
                    dest.MemberId = src.UserDetail.MemberId;
                })
                .ReverseMap();
        }
    }
}

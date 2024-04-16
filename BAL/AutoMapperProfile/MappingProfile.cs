using AutoMapper;
using BAL.ViewModels;
using BAL.ViewModels.Event;
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
                    opt.MapFrom(src => src.Member != null ? src.Member.Email : "");
                })
                .ReverseMap();
            CreateMap<Member, MemberViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.ImagePath = src.MemberUser.ImagePath;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberUser.ImagePath = src.ImagePath;
                })
                ;
            CreateMap<MeetingParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.MeetingId;
                    dest.EventName = src.Meeting.MeetingName;
                    dest.EventType = "Meeting";
                    dest.EventHost = src.Meeting.Host;
                    dest.EventStaff = src.Meeting.Incharge;
                    dest.StartDate = src.Meeting.StartDate;
                    dest.EndDate = src.Meeting.EndDate;
                    dest.RegistrationDeadline = src.Meeting.RegistrationDeadline;
                    dest.Status = src.Meeting.Status;
                    dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                })
                .ReverseMap()
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
                    dest.EventType = "Meeting";
                    dest.EventHost = src.Trip.Host;
                    dest.EventStaff = src.Trip.InCharge;
                    dest.StartDate = src.Trip.StartDate;
                    dest.EndDate = src.Trip.EndDate;
                    dest.RegistrationDeadline = src.Trip.RegistrationDeadline;
                    dest.Status = src.Trip.Status;
                    dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                })
                .ReverseMap()
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
            CreateMap<MeetingParticipant, MeetingParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.Member.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.Member.FullName = src.MemberName;
                });
            CreateMap<FieldTripParticipant, FieldTripParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.Member.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.Member.FullName = src.MemberName;
                });
            CreateMap<Meeting, MeetingViewModel>()
                .ReverseMap();
            CreateMap<MeetingMedia, MeetingMediaViewModel>()
                .ReverseMap();
            CreateMap<FieldTrip, FieldTripViewModel>()
                .ReverseMap();
            CreateMap<FieldtripDaybyDay, FieldtripDaybyDayViewModel>()
                .ReverseMap();
            CreateMap<FieldtripRate, FieldtripRateViewModel>()
                .ReverseMap();
            CreateMap<FieldtripInclusion, FieldtripInclusionViewModel>()
                .ReverseMap();
            CreateMap<FieldtripGettingThere, FieldtripGettingThereViewModel>()
                .ReverseMap();
            CreateMap<FieldTripOverview, FIeldTripOverviewViewModel>()
                .ReverseMap();
            CreateMap<FieldtripMedia, FieldtripMediaViewModel>()
                .ReverseMap();
            CreateMap<Contest, ContestViewModel>().ReverseMap();
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
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}

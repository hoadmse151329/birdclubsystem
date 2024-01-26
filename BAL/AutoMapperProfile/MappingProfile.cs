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
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<MeetingParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.MeetingId;
                    dest.EventName = src.Meeting.MeetingName;
                    dest.EventType = "Meeting";
                    dest.StartDate = src.Meeting.StartDate;
                    dest.EndDate = src.Meeting.EndDate;
                    dest.RegistrationDeadline = src.Meeting.RegistrationDeadline;
                    switch (src.Meeting.Status)
                    {
                        case 0:
                            {
                                dest.Status = "InPreperation";
                                break;
                            }
                        case 1:
                            {
                                dest.Status = "OnGoing";
                                break;
                            }
                        case 2:
                            {
                                dest.Status = "Ended";
                                break;
                            }
                        default:
                            {
                                dest.Status = "Canceled";
                                break;
                            }
                    }
                    dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                    dest.Fee = 30000;
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
            CreateMap<Meeting, MeetingViewModel>()
                .ReverseMap();
            CreateMap<FieldTrip, FieldTripViewModel>()
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

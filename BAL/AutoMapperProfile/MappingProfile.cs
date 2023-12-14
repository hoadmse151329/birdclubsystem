using AutoMapper;
using BAL.ViewModels;
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
            CreateMap<Meeting, MeetingViewModel>().ReverseMap();
            CreateMap<MeetingMedia, MeetingMediaViewModel>().ReverseMap();
            CreateMap<MeetingParticipant, MeetingParticipantViewModel>().ReverseMap();
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}

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
            CreateMap<User, UserViewModel>()
                /*.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Member.FullName))*/
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.AllowNull();
                    opt.MapFrom(src => src.Member != null ? src.Member.Email : "");
                })
                .ReverseMap();
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<Meeting, MeetingViewModel>()
                .ReverseMap();
            CreateMap<Location, LocationViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.AreaNumber = src.LocationName.Split(",")[0];
                    dest.Street = src.LocationName.Split(",")[1];
                    dest.District = src.LocationName.Split(",")[2];
                    dest.City = src.LocationName.Split(",")[3];
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.LocationName = src.AreaNumber + "," + src.Street + "," + src.District + "," + src.City;
                });
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
            CreateMap<FieldTrip, FieldTripViewModel>().ReverseMap();
        }
    }
}

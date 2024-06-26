﻿using AutoMapper;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
using BAL.ViewModels.News;

namespace WebAppMVC.Constants
{
    public class VMAutoMapper : Profile
    {
        public VMAutoMapper()
        {
            CreateMap<NewsViewModel, UpdateNewsDetail>()
                .ReverseMap();
            CreateMap<MeetingViewModel, UpdateMeetingDetailsVM>()
                .ReverseMap();
            CreateMap<MeetingViewModel, UpdateMeetingStatusVM>()
                .ReverseMap();
            CreateMap<ContestViewModel, UpdateContestDetailsVM>()
                .ReverseMap();
            CreateMap<ContestViewModel, UpdateContestStatusVM>()
                .ReverseMap();
            CreateMap<FieldTripViewModel, UpdateFieldtripDetailsVM>()
                .ReverseMap();
            CreateMap<FieldTripViewModel, UpdateFieldtripStatusVM>()
                .ReverseMap();
        }
    }
}

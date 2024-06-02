﻿using AutoMapper;
using BAL.ViewModels;
using BAL.ViewModels.News;

namespace WebAppMVC.Constants
{
    public class VMAutoMapper : Profile
    {
        public VMAutoMapper()
        {
            CreateMap<NewsViewModel, UpdateNewsDetail>()
                .ReverseMap();
        }
    }
}
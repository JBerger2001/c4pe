using AutoMapper;
using Feedback_API.Models;
using Feedback_API.Models.Domain;
using Feedback_API.Models.Requests;
using Feedback_API.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.MappingProfiles
{
    public class DTOToDomainProfile : Profile
    {
        public DTOToDomainProfile()
        {
            CreateMap<PlaceResponse, Place>()
                .ForMember(dest => dest.OpeningTimes, opt =>
                    opt.MapFrom(src => src.OpeningTimes.Select(ot => new OpeningTime()
                    {
                        Day = ot.Day,
                        Open = TimeSpan.Parse(ot.Open),
                        Close = TimeSpan.Parse(ot.Close)
                    }).ToList()))
                .ForMember(dest => dest.PlaceType, opt => opt.Ignore());
            CreateMap<PlaceRequest, Place>();

            CreateMap<OpeningTimeResponse, OpeningTime>();
            CreateMap<OpeningTimeRequest, OpeningTime>();

            CreateMap<UserResponse, User>();
            CreateMap<UserRegisterRequest, User>();

            CreateMap<ReviewResponse, Review>();
            CreateMap<ReviewRequest, Review>();
        }
    }
}

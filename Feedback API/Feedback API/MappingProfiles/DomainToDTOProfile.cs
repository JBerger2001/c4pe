using AutoMapper;
using Feedback_API.Models.Domain;
using Feedback_API.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.MappingProfiles
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Place, PlaceResponse>()
                .ForMember(dest => dest.OpeningTimes, opt =>
                    opt.MapFrom(src => src.OpeningTimes.Select(ot => new OpeningTimeResponse()
                    {
                        ID = ot.ID,
                        Day = ot.Day,
                        Open = ot.Open.ToString(),
                        Close = ot.Close.ToString()
                    }).ToList()))
                .ForSourceMember(src => src.Reviews, opt => opt.DoNotValidate());

            CreateMap<OpeningTime, OpeningTimeResponse>();

            CreateMap<User, UserResponse>();

            CreateMap<Review, ReviewResponse>();

            CreateMap<PlaceType, PlaceTypeResponse>();
        }
    }
}

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
                {
                    opt.MapFrom(src => src.OpeningTimes.Select(ot => new OpeningTimeResponse()
                    {
                        ID = ot.ID,
                        Day = ot.Day,
                        Open = ot.Open.ToString(),
                        Close = ot.Close.ToString()
                    }).ToList());
                })
                .ForMember(dest => dest.Rating, opt =>
                {
                    opt.MapFrom(src => (src.Reviews.Count == 0) ? 0 : src.Reviews.Average(r => r.Rating));
                })
                .ForMember(dest => dest.ReviewCount, opt =>
                {
                    opt.MapFrom(src => src.Reviews.Count);
                })
                .ForSourceMember(src => src.Reviews, opt => opt.DoNotValidate());

            CreateMap<PlaceType, PlaceTypeResponse>();

            CreateMap<PlaceOwner, PlaceOwnerResponse>();
            CreateMap<PlaceOwner, PlaceOwnerFullResponse>();

            CreateMap<OpeningTime, OpeningTimeResponse>();

            CreateMap<User, UserPrivateResponse>()
                .ForMember(dest => dest.ReviewCount, opt =>
                {
                    opt.MapFrom(src => src.Reviews.Count);
                });
            CreateMap<User, UserPublicResponse>()
                .ForMember(dest => dest.ReviewCount, opt =>
                {
                    opt.MapFrom(src => src.Reviews.Count);
                });

            CreateMap<Review, ReviewResponse>()
                .ForMember(dest => dest.PositiveReactions, opt =>
                {
                    opt.MapFrom(src => src.Reactions.Count(r => r.IsHelpful));
                })
                .ForMember(dest => dest.NegativeReactions, opt =>
                {
                    opt.MapFrom(src => src.Reactions.Count(r => !r.IsHelpful));
                });
            CreateMap<Review, ReviewUserResponse>()
                .ForMember(dest => dest.PositiveReactions, opt =>
                {
                    opt.MapFrom(src => src.Reactions.Count(r => r.IsHelpful));
                })
                .ForMember(dest => dest.NegativeReactions, opt =>
                {
                    opt.MapFrom(src => src.Reactions.Count(r => !r.IsHelpful));
                });

            CreateMap<Reaction, ReactionResponse>();

            CreateMap<PlaceType, PlaceTypeResponse>();

            CreateMap<PlaceImage, PlaceImageResponse>();
        }
    }
}

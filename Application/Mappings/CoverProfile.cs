using Application.Dtos;
using Domain.Entities;
using AutoMapper;

namespace SemikolonApi
{
    public class CoverProfile : Profile
    {
        public CoverProfile()
        {
            CreateMap<CoverDto, Cover>()
                .ForMember(dest => dest.Artists, opt => opt.Ignore());
        }
    }
}
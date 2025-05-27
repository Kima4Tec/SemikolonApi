using Application.Dtos;
using Domain.Entities;
using AutoMapper;

namespace SemikolonApi
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => new DateOnly(src.PublishYear, 1, 1)))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasePrice))
                .ForMember(dest => dest.Cover, opt => opt.Ignore());
        }
    }
}
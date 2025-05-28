using Application.Dtos;
using Domain.Entities;
using AutoMapper;

namespace SemikolonApi
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Year));

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(new DateTime(src.PublishDate, 1, 1))));




        }
    }
}
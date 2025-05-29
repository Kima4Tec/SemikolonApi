using Application.Dtos;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace SemikolonApi
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Year))
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dest => dest.DesignIdeas, opt => opt.MapFrom(src => src.Cover != null ? src.Cover.DesignIdeas : null))
                .ForMember(dest => dest.CoverIsDigital, opt => opt.MapFrom(src => src.Cover != null && src.Cover.DigitalOnly))
                .ForMember(dest => dest.CoverArtists, opt => opt.MapFrom(src => src.Cover != null ? src.Cover.Artists : new List<Artist>()));

            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(new DateTime(src.PublishDate, 1, 1))))
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Cover, opt => opt.Ignore());

            CreateMap<Artist, ArtistDto>();

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(new DateTime(src.PublishDate, 1, 1))));
        }
    }
}

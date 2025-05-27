using Application.Dtos;
using Domain.Entities;
using AutoMapper;

namespace Application.Mappings
{
    public class AuthorProfile : Profile // Extend AutoMapper's Profile class
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorDto, Author>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}

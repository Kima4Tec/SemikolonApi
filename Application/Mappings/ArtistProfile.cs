using Application.Dtos;
using AutoMapper;

using Domain.Entities;


namespace Application.Mappings
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<CreateArtistDto, Artist>();
        }
    }
}

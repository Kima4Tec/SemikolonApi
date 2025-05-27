using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IArtists
{
    /// <summary>
    /// Abstraction for artist-service for handling business logic.
    /// </summary>
    public interface IArtistService
    {
        Task<List<Artist>> GetAllArtistsAsync();
        Task<Artist> GetArtistByIdAsync(int artistId);
        Task <Artist> CreateArtistAsync(CreateArtistDto artistDto);
        Task<Artist> UpdateArtistAsync(int artistId, ArtistDto artistDto);
        Task<bool> DeleteArtistAsync(int artistId);
    }
}

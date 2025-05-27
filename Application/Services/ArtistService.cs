using Application.Dtos;
using Application.Interfaces.IArtists;
using AutoMapper;
using Domain.Entities;
using static Infrastructure.Repositories.IRepository;

public class ArtistService : IArtistService
{
    private readonly IRepository<Artist> _repository;
    private readonly IMapper _mapper;

    public ArtistService(IRepository<Artist> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }
/*------------------------------------------------------------------------*/
    public Task<List<Artist>> GetAllArtistsAsync()
    {
        return _repository.GetAllAsync();
    }
    public async Task<Artist> GetArtistByIdAsync(int artistId)
    {
        return await _repository.GetByIdAsync(artistId);
    }

    public Task<Artist> CreateArtistAsync(CreateArtistDto artistDto)
    {
        var artist = _mapper.Map<Artist>(artistDto);
        return _repository.AddAsync(artist);
    }

    public async Task<bool> DeleteArtistAsync(int artistId)
    {
        var artist = await _repository.GetByIdAsync(artistId);
        if (artist == null) return(false);
            await _repository.DeleteAsync(artist);
            return true;
    }


    public async Task<Artist> UpdateArtistAsync(int artistId, ArtistDto artistDto)
    {
        var existingArtist = await _repository.GetByIdAsync(artistId);
        
        if (existingArtist == null)
            throw new KeyNotFoundException($"Artist with ID {artistId} not found.");
        
        existingArtist.FirstName = artistDto.FirstName;
        existingArtist.LastName = artistDto.LastName;

        return await _repository.UpdateAsync(existingArtist);

    }

    
}


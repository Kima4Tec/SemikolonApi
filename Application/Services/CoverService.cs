using Application.Dtos;
using Application.Interfaces.ICovers;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Infrastructure.Repositories.IRepository;

namespace Application.Services
{
    public class CoverService : ICoverService
    {
        private readonly IRepository<Cover> _repository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;

        public CoverService(IRepository<Cover> repository, ICoverRepository coverRepository, IMapper mapper)
        {
            _repository = repository;
            _coverRepository = coverRepository;
            _mapper = mapper;
        }

public async Task<Cover> CreateCoverAsync(CreateCoverDto dto)
{
    // Tjek om cover allerede findes for denne BookId
    var existingCover = await _coverRepository.GetByBookIdAsync(dto.BookId);
    if (existingCover != null)
    {
        throw new InvalidOperationException($"Cover for BookId {dto.BookId} already exists.");
    }

    var cover = _mapper.Map<Cover>(dto);

    if (dto.ArtistIds?.Count > 0)
    {
        var artists = await _coverRepository.GetArtistsByIdsAsync(dto.ArtistIds);
        cover.Artists = artists;
    }

    return await _repository.AddAsync(cover);
}


        public Task<bool> DeleteCoverAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cover>> GetAllCoversAsync()
        {             
            return await _repository.GetAllAsync();
        }

        public async Task<Cover> GetCoverByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Cover> UpdateCoverAsync(int id, CoverDto dto)
        {
            var cover = await _repository.GetByIdAsync(id);
            if (cover == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            _mapper.Map(dto, cover);
            await _repository.UpdateAsync(cover);
            return cover;
        }
    }
}

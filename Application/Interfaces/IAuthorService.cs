using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Abstraction for author-service for handling business logic.
    /// </summary>
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> CreateAuthorAsync(AuthorDto authorDto);
    }
}

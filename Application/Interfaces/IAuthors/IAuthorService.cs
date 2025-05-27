using Application.Dtos;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.IAuthor
{
    /// <summary>
    /// Abstraction for author-service for handling business logic.
    /// </summary>
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(CreateAuthorDto authorDto);
        Task<Author> UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}

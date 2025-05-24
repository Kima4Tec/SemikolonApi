using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for handling author-related data operations.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Asynchronously creates a new author based on the provided data transfer object.
        /// </summary>
        /// <param name="authorDto">The DTO containing author information.</param>
        /// <returns>The created <see cref="Author"/> entity.</returns>
        Task<Author> CreateAuthorAsync(AuthorDto authorDto);
        /// <summary>
        /// Getting all authors
        /// </summary>
        /// <returns></returns>
        Task<List<Author>> GetAllAuthors();
    }
}

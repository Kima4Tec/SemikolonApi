using Domain.Entities;
using static Infrastructure.Repositories.IRepository;

namespace Application.Interfaces.IBook
{
    /// <summary>
    /// Created for specialized book operations that may not fit into a generic repository pattern.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetAllBooksWithoutCoverAsync();
        Task<List<Book>> SearchBookAsync(string query);
    }

}

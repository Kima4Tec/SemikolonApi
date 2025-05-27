using Application.Interfaces.IBook;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing Book entities.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}

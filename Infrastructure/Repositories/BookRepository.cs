using Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

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

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of books.</returns>
        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Book> CreateBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}

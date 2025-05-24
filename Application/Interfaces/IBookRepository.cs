using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Abstraction for data access operations related to books.
    /// This interface defines the contract for a book repository.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves all books from the data source.
        /// </summary>
        /// <returns>A list of all <see cref="Book"/> entities.</returns>
        Task<List<Book>> GetAllBooks();

        /// <summary>
        /// Creates a new book in the data source.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> entity to create.</param>
        /// <returns>The newly created <see cref="Book"/> entity.</returns>
        Task<Book> CreateBook(Book book);
    }

}

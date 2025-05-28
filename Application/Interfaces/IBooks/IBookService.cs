using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IBook
{
    /// <summary>
    /// Interface for BookService / Contract for logic of Book
    /// </summary>
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetBooksWithoutCoverAsync();
        Task<List<BookDto>> SearchBooksAsync(string query);
        Task<Book> CreateBookAsync(CreateBookDto bookDto);
        Task<Book> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}

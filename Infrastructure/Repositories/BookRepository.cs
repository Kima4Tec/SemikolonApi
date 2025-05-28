using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.IBook;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
                             .Include(b => b.Author)
                             .Include(b => b.Cover)
                             .ThenInclude(c => c.Artists)
                             .AsNoTracking()
                             .ToListAsync();
    }


    public async Task<List<Book>> GetAllBooksWithoutCoverAsync()
    {
        // Get all books where the Cover property is null because one book can only have one cover.
        return await _context.Books
            .Where(b => b.Cover == null)
            .ToListAsync();
    }
    public async Task<List<Book>> SearchBookAsync(string query)
    {
        query = query.ToLower();

        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Cover)
                .ThenInclude(c => c.Artists)
            .Where(b => b.Title.ToLower().Contains(query) ||
                        b.Author.FirstName.ToLower().Contains(query) ||
                        b.Author.LastName.ToLower().Contains(query))
            .ToListAsync();
    }
}

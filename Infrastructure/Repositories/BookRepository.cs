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

    public async Task<List<Book>> GetAllBooksWithoutCoverAsync()
    {
        // Get all books where the Cover property is null because one book can only have one cover.
        return await _context.Books
            .Where(b => b.Cover == null)
            .ToListAsync();
    }
}

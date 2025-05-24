using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class AuthorRepository:IAuthorRepository
    {

        private readonly ApplicationDbContext _context;


        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> CreateAuthorAsync(AuthorDto authorDto) 
        {

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

    }
}

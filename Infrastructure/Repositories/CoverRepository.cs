using Application.Interfaces.ICovers;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CoverRepository : ICoverRepository
    {
        private readonly ApplicationDbContext _context;

        public CoverRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Cover> GetByIdAsync(int id)
        {
            return await _context.Covers
                .Include(c => c.Artists)
                .FirstOrDefaultAsync(c => c.CoverId == id);
        }

        public async Task<List<Cover>> GetAllAsync()
        {
            return await _context.Covers
                .Include(c => c.Artists)
                .ToListAsync();
        }



        public async Task<Cover?> GetByBookIdAsync(int bookId)
        {
            return await _context.Covers
                .Include(c => c.Artists)
                .FirstOrDefaultAsync(c => c.BookId == bookId);
        }

        public async Task<List<Artist>> GetArtistsByIdsAsync(List<int> artistIds)
        {
            return await _context.Artists
                .Where(a => artistIds.Contains(a.ArtistId))
                .ToListAsync();
        }
    }
}

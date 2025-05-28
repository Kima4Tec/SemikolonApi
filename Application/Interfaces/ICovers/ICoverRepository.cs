using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.ICovers
{
    public interface ICoverRepository
    {
        Task<Cover> GetByIdAsync(int id);
        Task<List<Cover>> GetAllAsync();
        Task<Cover?> GetByBookIdAsync(int bookId);
        Task<List<Artist>> GetArtistsByIdsAsync(List<int> artistIds);
    }
}

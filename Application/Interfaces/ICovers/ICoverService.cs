using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICovers
{
    public interface ICoverService
    {
        Task<List<Cover>> GetAllCoversAsync();
        Task<Cover> GetCoverByIdAsync(int id);
        Task<Cover> CreateCoverAsync(CreateCoverDto dto);
        Task<Cover> UpdateCoverAsync(int id, CoverDto dto);
        Task<bool> DeleteCoverAsync(int id);
    }
}

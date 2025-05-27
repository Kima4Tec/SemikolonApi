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
        Task<List<Author>> GetAllBooksAsync();
        Task<Author> GetBookByIdAsync(int id);
        Task<Author> CreateBookAsync(CreateCoverDto coverDto);
        Task<Author> UpdateBookAsync(int id, CoverDto coverDto);
        Task<bool> DeleteBookAsync(int id);
    }
}

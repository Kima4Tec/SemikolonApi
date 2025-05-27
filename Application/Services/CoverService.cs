using Application.Dtos;
using Application.Interfaces.ICovers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CoverService : ICoverService
    {
        public Task<Author> CreateBookAsync(CreateCoverDto coverDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateBookAsync(int id, CoverDto coverDto)
        {
            throw new NotImplementedException();
        }
    }
}

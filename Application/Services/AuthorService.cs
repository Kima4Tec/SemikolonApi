using Application.Dtos;
using Application.Interfaces;
using Application.Validation;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            // Add logging and validation here  
            var authors = await _authorRepository.GetAllAuthors();

            return authors;
        }

        public async Task<Author> CreateAuthorAsync(AuthorDto authorDto)
        {
            var validator = new AuthorDtoValidator();
            var result = validator.Validate(authorDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            // Add logging and validation here  
            return await _authorRepository.CreateAuthorAsync(authorDto);
        }
    }
}

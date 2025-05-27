using Application.Dtos;
using Application.Interfaces.IAuthor;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using static Infrastructure.Repositories.IRepository;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _repository;
    private readonly IValidator<CreateAuthorDto> _authorValidator;
    private readonly IMapper _mapper;

    public AuthorService(IRepository<Author> repository,IValidator<CreateAuthorDto> authorValidator,IMapper mapper)
    {
        _repository = repository;
        _authorValidator = authorValidator;
        _mapper = mapper;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<Author> CreateAuthorAsync(CreateAuthorDto authorDto)
    {
        var validationResult = await _authorValidator.ValidateAsync(authorDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var author = _mapper.Map<Author>(authorDto);
        return await _repository.AddAsync(author);
    }
    public async Task<Author> UpdateAuthorAsync(int id, AuthorDto authorDto)
    {
        var existingAuthor = await _repository.GetByIdAsync(id);
        if (existingAuthor == null)
            throw new KeyNotFoundException($"Author with ID {id} not found.");

        existingAuthor.FirstName = authorDto.FirstName;
        existingAuthor.LastName = authorDto.LastName;

        return await _repository.UpdateAsync(existingAuthor);
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null) return false;

        await _repository.DeleteAsync(author);
        return true;
    }
}


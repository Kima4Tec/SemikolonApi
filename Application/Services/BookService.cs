using Application.Dtos;
using Application.Interfaces.IBook;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using static Infrastructure.Repositories.IRepository;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IValidator<CreateAuthorDto> _bookValidator;
        private readonly IMapper _mapper;

        public BookService(IRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Book> CreateBookAsync(CreateBookDto bookDto)
        {
            var validator = new BookDtoValidator();
            if (!validator.ValidateBookDto(bookDto, out var errors))
            {
                throw new ValidationException(string.Join("; ", errors));
            }

            var book = _mapper.Map<Book>(bookDto);

            await _repository.AddAsync(book);
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }

            await _repository.DeleteAsync(book);
            return true;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Book> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            _mapper.Map(bookDto, book);
            await _repository.UpdateAsync(book);
            return book;
        }

    }
}

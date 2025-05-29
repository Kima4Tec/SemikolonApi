using Application.Dtos;
using Application.Interfaces.IBook;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using static Infrastructure.Repositories.IRepository;
using static System.Reflection.Metadata.BlobBuilder;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IRepository<Book> repository, IMapper mapper, IBookRepository bookRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _bookRepository = bookRepository;
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

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<List<BookDto>>(books);
        }
        public async Task<List<Book>> GetBooksWithoutCoverAsync()
        {
            return await _bookRepository.GetAllBooksWithoutCoverAsync();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var books = await _bookRepository.GetBookByIdAsync(id);
            return _mapper.Map<BookDto>(books);
        }

        public async Task<List<BookDto>> SearchBooksAsync(string query)
        {
            var books = await _bookRepository.SearchBookAsync(query);
            return _mapper.Map<List<BookDto>>(books);
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

        public async Task<List<Book>> SearchBookAsync(string query)
        {
            var book = await _bookRepository.SearchBookAsync(query);
            return book ?? new List<Book>(); //hvis null så ny liste eller skal der noget mapper på

        }
    }
}

using Application.Dtos;
using Application.Interfaces.IArtists;
using Application.Interfaces.IBook;
using Application.Validation;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace SemikolonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly BookDtoValidator _bookDtoValidator;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, BookDtoValidator bookDtoValidator, IMapper mapper)
        {
            _bookService = bookService;
            _bookDtoValidator = bookDtoValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Søgning kan ikke være tom.");

            var books = await _bookService.SearchBooksAsync(query);
            return Ok(books);
        }

        [HttpGet("books-without-cover")]
        public async Task<ActionResult<List<BookDto>>> GetBooksWithoutCover()
        {
            var books = await _bookService.GetBooksWithoutCoverAsync();
            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return Ok(bookDtos);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] CreateBookDto bookDto)
        {

            var createdBook = await _bookService.CreateBookAsync(bookDto);
            return Ok(createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBookAsync(int id)
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}

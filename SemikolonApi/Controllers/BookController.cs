using Application.Dtos;
using Application.Interfaces.IBook;
using Application.Validation;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SemikolonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly BookDtoValidator _bookDtoValidator;

        public BookController(IBookService bookService, BookDtoValidator bookDtoValidator)
        {
            _bookService = bookService;
            _bookDtoValidator = bookDtoValidator;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] CreateBookDto bookDto)
        {

            var createdBook = await _bookService.CreateBookAsync(bookDto);
            return Ok(createdBook);
        }

    }
}

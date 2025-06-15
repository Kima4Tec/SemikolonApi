using Application.Dtos;
using Application.Interfaces.IAuthor;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SemikolonApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Retrieves a list of all authors from the repository.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthorsAsync()
        {
            var authorsList = await _authorService.GetAllAuthorsAsync();
            return Ok(authorsList);
        }

        /// <summary>
        /// Gets an author by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            try
            {
                var authorCreated = await _authorService.CreateAuthorAsync(authorDto);
                return Ok(authorCreated);
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );
                return BadRequest(new { errors = errorMessages });
            }
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (id != authorDto.Id)
                return BadRequest("ID in path does not match ID in body.");

            try
            {
                await _authorService.UpdateAuthorAsync(id, authorDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );
                return BadRequest(new { errors = errorMessages });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.DeleteAuthorAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}

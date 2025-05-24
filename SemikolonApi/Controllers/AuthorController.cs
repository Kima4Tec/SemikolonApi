using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SemikolonApi.Controllers
{
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
        /// <returns>
        /// An <see cref="ActionResult"/> containing a list of authors.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthorsAsync()
        {
            var authorsList = await _authorService.GetAllAuthors();
            return Ok(authorsList);
        }

        /// <summary>
        /// Creates a new author based on the provided. Using FluentValidation and errorhandling in frontend to produce output on error.
        /// </summary>
        /// <param name="authorDto">The data transfer object containing author details.</param>
        /// <returns>The newly created <see cref="Author"/> wrapped in an <see cref="ActionResult"/>.</returns>
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorDto authorDto)
        {
            try
            {
                var authorCreated = await _authorService.CreateAuthorAsync(authorDto);
                return Ok(authorCreated);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    errors = ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                });
            }
        }



    }
}

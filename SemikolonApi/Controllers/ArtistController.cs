using Application.Dtos;
using Application.Interfaces.IArtists;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SemikolonApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Artist>>> GetAllArtistsAsync()
        {
            var artistList = await _artistService.GetAllArtistsAsync();
            return Ok(artistList);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtistById(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(id);
                return Ok(artist);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> CreateArtist([FromBody] CreateArtistDto artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .ToDictionary(
                            e => e.Key,
                            e => e.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                });
            }

            var createdArtist = await _artistService.CreateArtistAsync(artistDto);
            return Ok(createdArtist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Artist>> UpdateArtistAsync(int id, [FromBody]ArtistDto artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = ModelState
                        .Where(ms => ms.Value?.Errors?.Count > 0)
                        .ToDictionary(
                            e => e.Key,
                            e => e.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                });
            }


            await _artistService.UpdateArtistAsync(id, artistDto);
                return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var deleted = await _artistService.DeleteArtistAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}

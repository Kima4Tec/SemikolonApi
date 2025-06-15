using Application.Dtos;
using Application.Interfaces.IAuthor;
using Application.Interfaces.ICovers;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SemikolonApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService _coverService;
        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Cover>>> GetAllCoversAsync()
        {
            var coverList = await _coverService.GetAllCoversAsync();
            return Ok(coverList);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cover>> GetCoverById(int id)
        {
            var cover = await _coverService.GetCoverByIdAsync(id);
            if (cover == null)
            {
                return NotFound();
            }
            return Ok(cover);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCover([FromBody] CreateCoverDto coverDto)
        {
            var createdCover = await _coverService.CreateCoverAsync(coverDto);
            return Ok(createdCover);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Cover>> UpdateCover(int id, [FromBody] CoverDto coverDto)
        {
            var updatedCover = await _coverService.UpdateCoverAsync(id, coverDto);
            return Ok(updatedCover);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cover>> DeleteCover(int id)
        {
            var deleted = await _coverService.DeleteCoverAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}

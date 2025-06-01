using Application.Dtos;
using Application.Interfaces.ICovers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SemikolonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService _coverService;
        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cover>>> GetAllCoversAsync()
        {
            var coverList = await _coverService.GetAllCoversAsync();
            return Ok(coverList);
        }

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

    }
}

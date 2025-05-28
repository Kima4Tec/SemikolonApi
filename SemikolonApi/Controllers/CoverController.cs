using Application.Dtos;
using Application.Interfaces.ICovers;
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

        [HttpPost]
        public async Task<ActionResult> CreateCover([FromBody] CreateCoverDto coverDto)
        {
            var createdCover = await _coverService.CreateCoverAsync(coverDto);
            return Ok(createdCover);
        }

    }
}

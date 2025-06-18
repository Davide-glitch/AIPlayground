using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatformsAsync()
        {
            var platforms = await _platformService.GetPlatformsAsync();

            return Ok(platforms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDto>> GetByIdAsync(int id)
        {
            var platform = await _platformService.GetByIdAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }
    }
}

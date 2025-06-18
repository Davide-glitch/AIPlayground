using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunsController : ControllerBase
    {
        private readonly IRunService _runService;

        public RunsController(IRunService runService)
        {
            _runService = runService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRuns([FromBody] RunCreateDto runCreateDto)
        {
            if (runCreateDto.ModelsToRun.Count == 0)
            {
                return BadRequest("Invalid run data.");
            }

            var runs = await _runService.CreateRunsAsync(runCreateDto);

            return Ok(runs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRuns()
        {
            Console.WriteLine("GetAllRuns endpoint hit.");
            var runs = await _runService.GetAllRunsAsync();
            return Ok(runs);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserRating(int id, [FromBody] RunUserRatingUpdateDto dto)
        {
            dto.RunId = id;
            var updatedRun = await _runService.UpdateUserRatingAsync(dto);
            return Ok(updatedRun);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRun(int id)
        {
            var result = await _runService.DeleteRunAsync(id);

            if (!result)
            {
                return NotFound($"Run with id {id} not found");
            }

            return Ok(new { message = "Run deleted successfully" });
        }
    }
}

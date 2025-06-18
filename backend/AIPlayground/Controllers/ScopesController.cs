using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly IScopeService _scopeService;

        public ScopesController(IScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetScopesAsync()
        {
            var scopes = await _scopeService.GetAllScopesAsync();

            return Ok(scopes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScopeDto>> GetByIdAsync(int id)
        {
            var scope = await _scopeService.GetScopeByIdAsync(id);

            if (scope == null)
            {
                return NotFound();
            }

            return Ok(scope);
        }

        [HttpGet("{scopeId}/prompts")]
        public async Task<IActionResult> GetPromptsByScopeIdAsync(int scopeId)
        {
            var prompts = await _scopeService.GetPromptsByScopeIdAsync(scopeId);

            return Ok(prompts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScopeAsync([FromBody] ScopeCreateDto scopeCreateDto)
        {
            var scope = await _scopeService.CreateScopeAsync(scopeCreateDto);

            return Ok(scope);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ScopeDto>> UpdateScopeAsync(int id, ScopeCreateDto scopeUpdateDto)
        {
            var scope = await _scopeService.UpdateScopeAsync(id, scopeUpdateDto);
            if (scope == null)
            {
                return NotFound();
            }

            return Ok(scope);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                await _scopeService.DeleteScopeAsync(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}

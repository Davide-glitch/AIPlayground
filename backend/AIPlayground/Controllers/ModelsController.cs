using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModelsAsync()
        {
            var models = await _modelService.GetModelsAsync();

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDto>> GetByIdAsync(int id)
        {
            var model = await _modelService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}

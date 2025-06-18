using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;

namespace AIPlayground.BusinessLogic.Services
{
    public class PromptService : IPromptService
    {
        private readonly IRepository<Prompt> _promptRepository;
        private readonly IRepository<Scope> _scopeRepository;

        public PromptService(IRepository<Prompt> promptRepository, IRepository<Scope> scopeRepository)
        {
            _promptRepository = promptRepository;
            _scopeRepository = scopeRepository;
        }

        public async Task<IEnumerable<PromptDto>> GetPromptsAsync()
        {
            var prompts = await _promptRepository.GetAllAsync();

            return prompts.Select(p => new PromptDto
            {
                Id = p.Id,
                Name = p.Name,
                SystemMessage = p.SystemMessage,
                UserMessage = p.UserMessage,
                ExpectedResult = p.ExpectedResult
            });
        }

        public async Task<PromptDto?> GetByIdAsync(int id)
        {
            var prompt = await _promptRepository.GetByIdAsync(id);

            if (prompt == null)
            {
                return null;
            }

            return new PromptDto
            {
                Id = prompt.Id,
                Name = prompt.Name,
                SystemMessage = prompt.SystemMessage,
                UserMessage = prompt.UserMessage,
                ExpectedResult = prompt.ExpectedResult
            };
        }

        public async Task<PromptDto> CreatePromptAsync(PromptCreateDto promptCreateDto)
        {
            // Validate that the referenced Scope exists before creating the Prompt
            var scope = await _scopeRepository.GetByIdAsync(promptCreateDto.ScopeId);
            if (scope == null)
            {
                throw new ArgumentException($"Scope with ID {promptCreateDto.ScopeId} does not exist. Cannot create prompt with non-existent scope.");
            }

            var prompt = new Prompt
            {
                ScopeId = promptCreateDto.ScopeId,
                Name = promptCreateDto.Name,
                SystemMessage = promptCreateDto.SystemMessage, // Make sure we set the SystemMessage too
                UserMessage = promptCreateDto.UserMessage,
                ExpectedResult = promptCreateDto.ExpectedResult
            };

            var createdPrompt = await _promptRepository.AddAsync(prompt);

            return new PromptDto
            {
                Id = createdPrompt.Id,
                Name = createdPrompt.Name,
                SystemMessage = createdPrompt.SystemMessage,
                UserMessage = createdPrompt.UserMessage,
                ExpectedResult = createdPrompt.ExpectedResult
            };
        }

        public async Task DeleteByIdAsync(int id)
        {
            var prompt = await _promptRepository.GetByIdAsync(id);

            if (prompt == null)
            {
                throw new Exception($"Prompt with id {id} not found");
            }

            await _promptRepository.DeleteAsync(id);
        }
    }
}

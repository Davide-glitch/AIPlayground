using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;

namespace AIPlayground.BusinessLogic.Services
{
    public class ScopeService : IScopeService
    {
        private readonly IRepository<Scope> _scopeRepository;

        public ScopeService(IRepository<Scope> scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        public async Task<IEnumerable<ScopeDto>> GetAllScopesAsync()
        {
            var scopes = await _scopeRepository.GetAllAsync();

            return scopes.Select(scope => new ScopeDto
            {
                Id = scope.Id,
                Name = scope.Name,
            });
        }

        public async Task<ScopeDto?> GetScopeByIdAsync(int id)
        {
            var scope = await _scopeRepository.GetByIdAsync(id);

            if (scope == null)
            {
                return null;
            }

            return new ScopeDto
            {
                Id = scope.Id,
                Name = scope.Name
            };
        }

        public async Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto)
        {
            var scope = new Scope
            {
                Name = scopeDto.Name,
            };

            var createdScope = await _scopeRepository.AddAsync(scope);

            return new ScopeDto
            {
                Id = createdScope.Id,
                Name = createdScope.Name,
            };
        }

        public async Task<ScopeDto?> UpdateScopeAsync(int id, ScopeCreateDto scopeUpdateDto)
        {
            var scope = await _scopeRepository.GetByIdAsync(id);

            if (scope == null)
            {
                return null;
            }

            scope.Name = scopeUpdateDto.Name;

            var updatedScope = await _scopeRepository.UpdateAsync(scope);

            return new ScopeDto
            {
                Id = updatedScope.Id,
                Name = updatedScope.Name
            };
        }

        public async Task DeleteScopeAsync(int id)
        {
            var scope = await _scopeRepository.GetByIdAsync(id);

            if (scope == null)
            {
                throw new Exception($"Scope with id {id} not found");
            }

            await _scopeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId)
        {
            var scope = await _scopeRepository.GetByIdAsync(scopeId);

            if (scope == null)
            {
                return new List<PromptDto>();
            }

            var promptsByScope = scope.Prompts;

            return promptsByScope.Select(p => new PromptDto
            {
                Id = p.Id,
                Name = p.Name,
                SystemMessage = p.SystemMessage,
                UserMessage = p.UserMessage,
                ExpectedResult = p.ExpectedResult
            });
        }
    }
}

using AIPlayground.BusinessLogic.DTOs;

namespace AIPlayground.BusinessLogic.Interfaces
{
    public interface IPromptService
    {
        Task<IEnumerable<PromptDto>> GetPromptsAsync();
        Task<PromptDto?> GetByIdAsync(int id);
        Task<PromptDto> CreatePromptAsync(PromptCreateDto promptCreateDto);
        Task DeleteByIdAsync(int id);
    }
}

using AIPlayground.BusinessLogic.DTOs;

namespace AIPlayground.BusinessLogic.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelDto>> GetModelsAsync();
        Task<ModelDto?> GetByIdAsync(int id);
    }
}

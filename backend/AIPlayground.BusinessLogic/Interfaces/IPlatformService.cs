using AIPlayground.BusinessLogic.DTOs;

namespace AIPlayground.BusinessLogic.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDto>> GetPlatformsAsync();
        Task<PlatformDto?> GetByIdAsync(int id);
    }
}

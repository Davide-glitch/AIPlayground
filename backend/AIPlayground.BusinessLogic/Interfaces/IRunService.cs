using AIPlayground.BusinessLogic.DTOs;

namespace AIPlayground.BusinessLogic.Interfaces
{
    public interface IRunService
    {
        Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto);
        Task<RunDto> UpdateUserRatingAsync(RunUserRatingUpdateDto dto);
        Task<List<RunGetDto>> GetAllRunsAsync();
        Task<bool> DeleteRunAsync(int id);
    }
}

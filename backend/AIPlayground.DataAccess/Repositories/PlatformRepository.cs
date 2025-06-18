using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories
{
    public class PlatformRepository : BaseRepository<Platform>
    {
        public PlatformRepository(AiPlaygroundContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Platform>> GetAllAsync()
        {
            try
            {
                return await _context.Platforms
                    .Include(p => p.Models)
                        .ThenInclude(m => m.Runs)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all platforms with models: {ex.Message}", ex);
            }
        }
        public override async Task<Platform?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Platforms
                    .Include(p => p.Models)
                        .ThenInclude(m => m.Runs)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving platform with ID {id}: {ex.Message}", ex);
            }
        }
    }
}

using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories
{
    public class ModelRepository : BaseRepository<Model>
    {
        public ModelRepository(AiPlaygroundContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Model>> GetAllAsync()
        {
            try
            {
                return await _context.Models
                    .Include(m => m.Runs)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all models with runs: {ex.Message}", ex);
            }
        }

        public override async Task<Model?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Models
                    .Include(m => m.Runs)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving model with ID {id}: {ex.Message}", ex);
            }
        }
    }
}

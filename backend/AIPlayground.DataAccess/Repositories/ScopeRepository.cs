using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories
{
    public class ScopeRepository : BaseRepository<Scope>
    {
        public ScopeRepository(AiPlaygroundContext context) : base(context) { }

        public override async Task<Scope?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Scopes.Include(s => s.Prompts).FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}: {ex.Message}", ex);
            }
        }
    }
}

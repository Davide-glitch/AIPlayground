using AiPlayground.DataAccess.Configurations;
using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess
{
    public class AiPlaygroundContext : DbContext
    {
        public AiPlaygroundContext(DbContextOptions<AiPlaygroundContext> options)
            : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<Run> Runs { get; set; } = null!;
        public DbSet<Prompt> Prompts { get; set; } = null!;
        public DbSet<Scope> Scopes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new PlatformConfiguration().Configure(modelBuilder.Entity<Platform>());
            new ModelConfiguration().Configure(modelBuilder.Entity<Model>());
            new ScopeConfiguration().Configure(modelBuilder.Entity<Scope>());
            new PromptConfiguration().Configure(modelBuilder.Entity<Prompt>());
            new RunConfiguration().Configure(modelBuilder.Entity<Run>());
        }
    }
}

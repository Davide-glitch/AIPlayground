using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.ToTable("Scopes")
                .HasKey(s => s.Id);

            builder.HasData(new List<Scope>
            {
                new Scope { Id = 1, Name = "General" },
                new Scope { Id = 2, Name = "Coding" },
                new Scope { Id = 3, Name = "Creative Writing" }
            });
        }
    }
}

using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.ToTable("Prompts")
                .HasKey(p => p.Id);

            builder.HasOne(p => p.Scope)
                .WithMany(s => s.Prompts)
                .HasForeignKey(p => p.ScopeId)
                .HasConstraintName("FK_Prompt_Scope");

            builder.HasData(new List<Prompt>
            {
                new Prompt
                {
                    Id = 1,
                    ScopeId = 1,
                    Name = "Basic Greeting",
                    SystemMessage = "You are a helpful assistant.",
                    UserMessage = "Say hello to the user in a friendly way.",
                    ExpectedResult = "A friendly greeting message."
                },
                new Prompt
                {
                    Id = 2,
                    ScopeId = 2,
                    Name = "Code Review",
                    SystemMessage = "You are an expert code reviewer.",
                    UserMessage = "Review this Python function for best practices.",
                    ExpectedResult = "Code review with suggestions for improvement."
                },
                new Prompt
                {
                    Id = 3,
                    ScopeId = 3,
                    Name = "Story Beginning",
                    SystemMessage = "You are a creative writer.",
                    UserMessage = "Write the opening paragraph of a mystery novel.",
                    ExpectedResult = "An engaging opening paragraph for a mystery story."
                }
            });
        }
    }
}

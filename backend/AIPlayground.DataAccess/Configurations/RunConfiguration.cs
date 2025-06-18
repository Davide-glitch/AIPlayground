using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class RunConfiguration : IEntityTypeConfiguration<Run>
    {
        public void Configure(EntityTypeBuilder<Run> builder)
        {
            builder.ToTable("Runs")
                .HasKey(r => r.Id); builder.Property(r => r.Rating).HasDefaultValue(0); builder.Property(r => r.UserRating).HasDefaultValue(0);

            // ResponseTimeMs will be calculated and set by processors, no default value needed

            builder.HasOne(r => r.Model)
                .WithMany(m => m.Runs)
                .HasForeignKey(r => r.ModelId)
                .HasConstraintName("FK_Run_Model");

            builder.HasOne(r => r.Prompt)
                .WithMany(p => p.Runs)
                .HasForeignKey(r => r.PromptId)
                .HasConstraintName("FK_Run_Prompt");            // Sample data to demonstrate time-based ratings
            builder.HasData(new List<Run>
            {
                // GPT-4o runs (Model ID 1) - Fast response times
                new Run { Id = 1, ModelId = 1, PromptId = 1, ActualResponse = "Hello! I'm delighted to meet you! How can I assist you today?", Temperature = 0.7, Rating = 4.5, UserRating = 4.5, ResponseTimeMs = 800 },
                new Run { Id = 2, ModelId = 1, PromptId = 2, ActualResponse = "The code looks good but could benefit from type hints and docstrings.", Temperature = 0.3, Rating = 4.2, UserRating = 4.0, ResponseTimeMs = 1200 },
                new Run { Id = 3, ModelId = 1, PromptId = 3, ActualResponse = "The fog rolled in like a silent predator, consuming the streetlights one by one.", Temperature = 0.8, Rating = 4.8, UserRating = 4.5, ResponseTimeMs = 900 },
                
                // GPT-4o-mini runs (Model ID 2) - Very fast response times
                new Run { Id = 4, ModelId = 2, PromptId = 1, ActualResponse = "Hi there! Nice to meet you!", Temperature = 0.7, Rating = 3.8, UserRating = 3.5, ResponseTimeMs = 600 },
                new Run { Id = 5, ModelId = 2, PromptId = 2, ActualResponse = "Your code is functional. Consider adding error handling.", Temperature = 0.3, Rating = 3.5, UserRating = 3.8, ResponseTimeMs = 750 },
                
                // Gemini runs (Model ID 3) - Medium response times
                new Run { Id = 6, ModelId = 3, PromptId = 1, ActualResponse = "Greetings! I'm here to help with whatever you need.", Temperature = 0.7, Rating = 4.1, UserRating = 4.2, ResponseTimeMs = 2500 },
                new Run { Id = 7, ModelId = 3, PromptId = 3, ActualResponse = "Detective Sarah Chen had seen enough crime scenes to know when something was off.", Temperature = 0.8, Rating = 4.3, UserRating = 4.1, ResponseTimeMs = 3200 },
                
                // DeepSeek-reasoner runs (Model ID 4) - Slow response times
                new Run { Id = 8, ModelId = 4, PromptId = 2, ActualResponse = "This function demonstrates good separation of concerns. I recommend adding unit tests.", Temperature = 0.2, Rating = 4.6, UserRating = 4.8, ResponseTimeMs = 8500 },
                new Run { Id = 9, ModelId = 4, PromptId = 1, ActualResponse = "Hello! I'm ready to assist you with any questions you might have.", Temperature = 0.5, Rating = 3.9, UserRating = 4.0, ResponseTimeMs = 12000 },
                
                // DeepSeek-chat runs (Model ID 5) - Medium-slow response times
                new Run { Id = 10, ModelId = 5, PromptId = 1, ActualResponse = "Hey! Great to meet you. What can I help you with today?", Temperature = 0.6, Rating = 3.7, UserRating = 3.9, ResponseTimeMs = 4200 },
                new Run { Id = 11, ModelId = 5, PromptId = 3, ActualResponse = "The old mansion stood silent against the stormy night, hiding secrets within its walls.", Temperature = 0.9, Rating = 4.0, UserRating = 3.8, ResponseTimeMs = 5800 }
            });
        }
    }
}

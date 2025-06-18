using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.ToTable("Platforms")
                .HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ImageUrl)
                .IsRequired();

            builder.HasData(new List<Platform>
            {
                new Platform
                {
                    Id = 1,
                    Name = "OpenAI",
                    ImageUrl = "https://yt3.googleusercontent.com/MopgmVAFV9BqlzOJ-UINtmutvEPcNe5IbKMmP_4vZZo3vnJXcZGtybUBsXaEVxkmxKyGqX9R=s900-c-k-c0x00ffffff-no-rj"
                },
                new Platform
                {
                    Id = 2,
                    Name = "DeepSeek",
                    ImageUrl = "https://www.futuroprossimo.it/wp-content/uploads/2025/01/deepseek.jpg"
                },
                new Platform
                {
                    Id = 3,
                    Name = "Gemini",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/Google_Gemini_logo.svg/1200px-Google_Gemini_logo.svg.png"
                }
            });
        }
    }
}

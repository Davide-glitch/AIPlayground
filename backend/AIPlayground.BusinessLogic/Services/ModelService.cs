using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Interfaces;

namespace AIPlayground.BusinessLogic.Services
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> _modelRepository;

        public ModelService(IRepository<Model> modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public async Task<IEnumerable<ModelDto>> GetModelsAsync()
        {
            var models = await _modelRepository.GetAllAsync();

            return models.Select(m => new ModelDto
            {
                Id = m.Id,
                Name = m.Name,
                PlatformId = m.PlatformId,
                AverageRating = CalculateAverageRating(m.Runs)
            });
        }

        public async Task<ModelDto?> GetByIdAsync(int id)
        {
            var model = await _modelRepository.GetByIdAsync(id);

            if (model == null)
            {
                return null;
            }
            return new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                PlatformId = model.PlatformId,
                AverageRating = CalculateAverageRating(model.Runs)
            };
        }

        private double CalculateAverageRating(ICollection<Run> runs)
        {
            if (!runs.Any()) return 0;

            // Calculate average user rating and time rating
            var userRating = runs.Average(r => r.UserRating);
            var timeRating = runs.Average(r => CalculateTimeRating(r.ResponseTimeMs));

            // Combine ratings: 70% user rating + 30% time rating
            var combinedRating = (userRating * 0.7) + (timeRating * 0.3);

            // Round to 2 decimal places
            return Math.Round(combinedRating, 2);
        }

        private double CalculateTimeRating(int responseTimeMs)
        {
            // Time-based rating: 3 categories (1-3 stars for time component)
            // Fast: 0-1000ms = 3 stars
            // Medium: 1001-5000ms = 2 stars  
            // Slow: 5001+ms = 1 star
            if (responseTimeMs <= 1000) return 3.0;
            if (responseTimeMs <= 5000) return 2.0;
            return 1.0;
        }
    }
}

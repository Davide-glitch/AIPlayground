using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AIPlayground.BusinessLogic.AIProcessing.Factories;
using AIPlayground.BusinessLogic.DTOs;
using AIPlayground.BusinessLogic.Enums;
using AIPlayground.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AIPlayground.BusinessLogic.Services
{
    public class RunService : IRunService
    {
        private readonly AiPlaygroundContext _context;
        private readonly IConfiguration _configuration;

        public RunService(AiPlaygroundContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto)
        {
            var runs = new List<RunDto>();

            var AIProcessorFactory = new AIProcessorFactory(_configuration);

            var prompt = await _context.Prompts.FindAsync(runCreateDto.PromptId);

            if (prompt == null)
            {
                throw new Exception($"Prompt with id {runCreateDto.PromptId} not found");
            }

            foreach (var modelToRun in runCreateDto.ModelsToRun)
            {
                var model = await _context.Models.FindAsync(modelToRun.ModelId);

                if (model == null)
                {
                    throw new Exception($"Model with id {modelToRun.ModelId} not found");
                }

                var platformType = (PlatformType)model.PlatformId;

                var processor = AIProcessorFactory.CreateAIProcessor(platformType);

                var run = await processor.ProcessAsync(prompt, model, (float)modelToRun.Temperature);

                await _context.Runs.AddAsync(run); runs.Add(new RunDto
                {
                    Id = run.Id,
                    ModelId = run.ModelId,
                    PromptId = run.PromptId,
                    ActualResponse = run.ActualResponse,
                    Temperature = run.Temperature,
                    Rating = run.Rating,
                    UserRating = run.UserRating,
                    ResponseTimeMs = run.ResponseTimeMs
                });
            }

            await _context.SaveChangesAsync();

            return runs;
        }

        public async Task<RunDto> UpdateUserRatingAsync(RunUserRatingUpdateDto dto)
        {
            var run = await _context.Runs.FindAsync(dto.RunId);

            if (run == null)
            {
                throw new Exception($"Run with id {dto.RunId} not found");
            }

            run.UserRating = dto.UserRating;
            await _context.SaveChangesAsync(); return new RunDto
            {
                Id = run.Id,
                ModelId = run.ModelId,
                PromptId = run.PromptId,
                ActualResponse = run.ActualResponse,
                Temperature = run.Temperature,
                Rating = run.Rating,
                UserRating = run.UserRating,
                ResponseTimeMs = run.ResponseTimeMs
            };
        }

        public async Task<List<RunGetDto>> GetAllRunsAsync()
        {
            return await _context.Runs
                .Include(r => r.Model)
                .Include(r => r.Prompt).Select(run => new RunGetDto
                {
                    Id = run.Id,
                    Model = new ModelDto
                    {
                        Id = run.Model.Id,
                        Name = run.Model.Name,
                        PlatformId = run.Model.PlatformId
                    },
                    Prompt = new PromptDto
                    {
                        Id = run.Prompt.Id,
                        Name = run.Prompt.Name,
                        SystemMessage = run.Prompt.SystemMessage,
                        UserMessage = run.Prompt.UserMessage,
                        ExpectedResult = run.Prompt.ExpectedResult
                    },
                    ActualResponse = run.ActualResponse,
                    Temperature = run.Temperature,
                    Rating = run.Rating,
                    UserRating = run.UserRating,
                    ResponseTimeMs = run.ResponseTimeMs
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteRunAsync(int id)
        {
            var run = await _context.Runs.FindAsync(id);

            if (run == null)
            {
                return false;
            }

            _context.Runs.Remove(run);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

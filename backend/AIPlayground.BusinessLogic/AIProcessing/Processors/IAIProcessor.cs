using AiPlayground.DataAccess.Entities;

namespace AIPlayground.BusinessLogic.AIProcessing.Processors;

public interface IAIProcessor
{
    Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature);
}

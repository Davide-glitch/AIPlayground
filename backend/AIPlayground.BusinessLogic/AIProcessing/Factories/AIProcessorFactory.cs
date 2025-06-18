using AIPlayground.BusinessLogic.AIProcessing.Processors;
using AIPlayground.BusinessLogic.Enums;
using Microsoft.Extensions.Configuration;

namespace AIPlayground.BusinessLogic.AIProcessing.Factories;

public class AIProcessorFactory
{
    private readonly IConfiguration configuration;

    public AIProcessorFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IAIProcessor CreateAIProcessor(PlatformType platformType)
    {
        switch (platformType)
        {
            case PlatformType.OpenAI:
                return new OpenAIProcessor();
            case PlatformType.DeepSeek:
                return new DeepSeekProcessor();
            case PlatformType.Gemini:
                return new GeminiProcessor(configuration);
            default:
                throw new ArgumentException($"No AI processor found for platform type: {platformType}.");
        }
    }
}
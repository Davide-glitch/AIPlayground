using AIPlayground.BusinessLogic.AIProcessing.Processors;
using AIPlayground.BusinessLogic.Enums;

namespace AIPlayground.BusinessLogic.AIProcessing.Factories;

public interface IAIProcessorFactory
{
    IAIProcessor CreateAIProcessor(PlatformType platformType);
}

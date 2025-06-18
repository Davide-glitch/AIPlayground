using AiPlayground.DataAccess.Entities;
using OpenAI.Chat;
using System.Diagnostics;

namespace AIPlayground.BusinessLogic.AIProcessing.Processors;

public class OpenAIProcessor : IAIProcessor
{
    public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
    {
        var stopwatch = Stopwatch.StartNew();

        ChatClient client = new(model: model.Name, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        var systemMessage = new SystemChatMessage(prompt.SystemMessage);
        var userMessage = new UserChatMessage(prompt.UserMessage);

        var messages = new List<ChatMessage>
        {
            systemMessage,
            userMessage,
        };

        var options = new ChatCompletionOptions
        {
            Temperature = temperature,
        };

        ChatCompletion completion = await client.CompleteChatAsync(messages, options);

        stopwatch.Stop();
        var responseTimeMs = (int)stopwatch.ElapsedMilliseconds;
        var actualResponse = completion.Content.First().Text; var ratingSystemMessage = new SystemChatMessage(@"You are an expert evaluator rating AI responses. Your task is to provide a decimal rating from 1-10.

IMPORTANT: First analyze if the 'Expected Result' is relevant to the task:
- If the Expected Result is clear, specific, and directly related to the task, rate how well the response matches it (10 = perfect match)
- If the Expected Result is vague, generic, empty, or not related to the actual task, ignore it and instead rate the response based on:
  * Accuracy and correctness
  * Completeness and thoroughness  
  * Clarity and helpfulness
  * Appropriateness for the given prompt

Always give only a decimal number between 1-10 as your response.");

        var expectedResultMessage = new UserChatMessage($@"TASK: {prompt.SystemMessage}

USER PROMPT: {prompt.UserMessage}

EXPECTED RESULT: {prompt.ExpectedResult}

AI RESPONSE: {actualResponse}

Rate this AI response (1-10):");

        var ratingMessages = new List<ChatMessage>
        {
            ratingSystemMessage,
            expectedResultMessage
        };

        ChatCompletion ratingCompletion = await client.CompleteChatAsync(ratingMessages);
        double.TryParse(ratingCompletion.Content.First().Text, out double ratingResponse); return new Run
        {
            ModelId = model.Id,
            PromptId = prompt.Id,
            ActualResponse = actualResponse,
            Temperature = temperature,
            Rating = ratingResponse,
            ResponseTimeMs = responseTimeMs
        };
    }
}

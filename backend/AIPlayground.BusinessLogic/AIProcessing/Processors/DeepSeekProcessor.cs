using System.Net.Http.Headers;
using System.Text.Json;
using AiPlayground.DataAccess.Entities;
using AIPlayground.BusinessLogic.AIProcessing.Models;
using System.Diagnostics;

namespace AIPlayground.BusinessLogic.AIProcessing.Processors;

public class DeepSeekProcessor : IAIProcessor
{
    public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
    {
        var stopwatch = Stopwatch.StartNew();

        var apiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY");

        var requestUri = "https://api.deepseek.com/chat/completions";

        var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var content = new DeepSeekRequest
        {
            Model = model.Name,
            Messages =
            [
                new DeepSeekMessage
                {
                    Role = "system",
                    Content = prompt.SystemMessage
                },

                new DeepSeekMessage
                {
                    Role = "user",
                    Content = prompt.UserMessage
                }
            ],
            Stream = false
        };

        var jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"); try
        {
            var response = await client.PostAsync(requestUri, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            stopwatch.Stop();
            var responseTimeMs = (int)stopwatch.ElapsedMilliseconds;

            var deepSeekResponse = JsonSerializer.Deserialize<DeepSeekResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }); if (deepSeekResponse != null)
            {
                var actualResponse = deepSeekResponse.Choices.First().Message.Content;

                // Generate rating using DeepSeek API
                var ratingContent = new DeepSeekRequest
                {
                    Model = model.Name,
                    Messages =
                    [                        new DeepSeekMessage
                        {
                            Role = "system",
                            Content = @"You are an expert evaluator rating AI responses. Your task is to provide a decimal rating from 1-10.

IMPORTANT: First analyze if the 'Expected Result' is relevant to the task:
- If the Expected Result is clear, specific, and directly related to the task, rate how well the response matches it (10 = perfect match)
- If the Expected Result is vague, generic, empty, or not related to the actual task, ignore it and instead rate the response based on:
  * Accuracy and correctness
  * Completeness and thoroughness  
  * Clarity and helpfulness
  * Appropriateness for the given prompt

Always give only a decimal number between 1-10 as your response."
                        },
                        new DeepSeekMessage
                        {
                            Role = "user",
                            Content = $@"TASK: {prompt.SystemMessage}

USER PROMPT: {prompt.UserMessage}

EXPECTED RESULT: {prompt.ExpectedResult}

AI RESPONSE: {actualResponse}

Rate this AI response (1-10):"
                        }
                    ],
                    Stream = false
                };

                var ratingJsonContent = JsonSerializer.Serialize(ratingContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                var ratingHttpContent = new StringContent(ratingJsonContent, System.Text.Encoding.UTF8, "application/json");

                double ratingResponse = 0;
                try
                {
                    var ratingApiResponse = await client.PostAsync(requestUri, ratingHttpContent);
                    var ratingResponseContent = await ratingApiResponse.Content.ReadAsStringAsync();

                    var ratingDeepSeekResponse = JsonSerializer.Deserialize<DeepSeekResponse>(ratingResponseContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (ratingDeepSeekResponse != null && ratingDeepSeekResponse.Choices.Any())
                    {
                        double.TryParse(ratingDeepSeekResponse.Choices.First().Message.Content, out ratingResponse);
                    }
                }
                catch
                {
                    // If rating fails, default to 0
                    ratingResponse = 0;
                }

                return new Run
                {
                    ModelId = model.Id,
                    PromptId = prompt.Id,
                    ActualResponse = actualResponse,
                    Temperature = temperature,
                    Rating = ratingResponse,
                    UserRating = 0,
                    ResponseTimeMs = responseTimeMs
                };
            }
            else
            {
                stopwatch.Stop();
                throw new Exception("DeepSeek response is null");
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            throw new Exception($"Error sending DeepSeek request: {ex.Message}");
        }
    }
}

using System.Net.Http.Headers;
using System.Text.Json;
using AiPlayground.DataAccess.Entities;
using AIPlayground.BusinessLogic.AIProcessing.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AIPlayground.BusinessLogic.AIProcessing.Processors;

public class GeminiProcessor : IAIProcessor
{
    private readonly IConfiguration _configuration;

    public GeminiProcessor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
    {
        var stopwatch = Stopwatch.StartNew();

        var apiKey = _configuration["GEMINI_API_KEY"] ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY");

        var requestUri = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        var client = new HttpClient();

        // The API key is already in the URL, so we don't need to set an Authorization header
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var content = new GeminiRequest
        {
            Contents = [
                new GeminiContent
                {
                    Role = "user",
                    Parts = [
                        new GeminiPart
                        {
                            Text = $"{prompt.SystemMessage}\n\n{prompt.UserMessage}"
                        }
                    ]
                }
            ],
            GenerationConfig = new GeminiGenerationConfig
            {
                Temperature = temperature
            }
        };

        var jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(requestUri, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            stopwatch.Stop();
            var responseTimeMs = (int)stopwatch.ElapsedMilliseconds;

            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (geminiResponse != null && geminiResponse.Candidates.Any() && geminiResponse.Candidates.First().Content.Parts.Any())
            {
                var actualResponse = geminiResponse.Candidates.First().Content.Parts.First().Text;

                // Generate rating using Gemini API
                var ratingContent = new GeminiRequest
                {
                    Contents = [
                        new GeminiContent
                        {
                            Role = "user",
                            Parts = [
                                new GeminiPart
                                {
                                    Text = $@"You are an expert evaluator rating AI responses. Your task is to provide a decimal rating from 1-10.

IMPORTANT: First analyze if the 'Expected Result' is relevant to the task:
- If the Expected Result is clear, specific, and directly related to the task, rate how well the response matches it (10 = perfect match)
- If the Expected Result is vague, generic, empty, or not related to the actual task, ignore it and instead rate the response based on:
  * Accuracy and correctness
  * Completeness and thoroughness  
  * Clarity and helpfulness
  * Appropriateness for the given prompt

Always give only a decimal number between 1-10 as your response.

TASK: {prompt.SystemMessage}

USER PROMPT: {prompt.UserMessage}

EXPECTED RESULT: {prompt.ExpectedResult}

AI RESPONSE: {actualResponse}

Rate this AI response (1-10):"
                                }
                            ]
                        }
                    ],
                    GenerationConfig = new GeminiGenerationConfig
                    {
                        Temperature = 0.1f // Use low temperature for consistent rating
                    }
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

                    var ratingGeminiResponse = JsonSerializer.Deserialize<GeminiResponse>(ratingResponseContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (ratingGeminiResponse != null && ratingGeminiResponse.Candidates.Any() && ratingGeminiResponse.Candidates.First().Content.Parts.Any())
                    {
                        double.TryParse(ratingGeminiResponse.Candidates.First().Content.Parts.First().Text, out ratingResponse);
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
                throw new Exception("Gemini response is null or invalid");
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            throw new Exception($"Error sending Gemini request: {ex.Message}");
        }
    }
}

// Adding Gemini request/response models
public class GeminiRequest
{
    public List<GeminiContent> Contents { get; set; } = [];
    public GeminiGenerationConfig GenerationConfig { get; set; } = new();
}

public class GeminiContent
{
    public string Role { get; set; } = "";
    public List<GeminiPart> Parts { get; set; } = [];
}

public class GeminiPart
{
    public string Text { get; set; } = "";
}

public class GeminiGenerationConfig
{
    public float Temperature { get; set; }
}

public class GeminiResponse
{
    public List<GeminiCandidate> Candidates { get; set; } = [];
}

public class GeminiCandidate
{
    public GeminiContent Content { get; set; } = new();
}
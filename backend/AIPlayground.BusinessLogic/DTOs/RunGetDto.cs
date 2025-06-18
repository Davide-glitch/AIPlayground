using AIPlayground.BusinessLogic.DTOs;

namespace AIPlayground.BusinessLogic.DTOs
{
    public class RunGetDto
    {
        public int Id { get; set; }
        public ModelDto? Model { get; set; }
        public PromptDto? Prompt { get; set; }
        public string? ActualResponse { get; set; }
        public double Temperature { get; set; }
        public double Rating { get; set; }
        public double UserRating { get; set; }
        public int ResponseTimeMs { get; set; }
    }
}

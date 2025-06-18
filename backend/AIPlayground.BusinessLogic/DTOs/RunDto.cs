namespace AIPlayground.BusinessLogic.DTOs
{
    public class RunDto
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public int PromptId { get; set; }

        public string ActualResponse { get; set; } = string.Empty;

        public double Temperature { get; set; }

        public double Rating { get; set; }

        public double UserRating { get; set; }

        public int ResponseTimeMs { get; set; }
    }
}

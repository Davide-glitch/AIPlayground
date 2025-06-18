namespace AiPlayground.DataAccess.Entities
{
    public class Run
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public int PromptId { get; set; }

        public string ActualResponse { get; set; } = string.Empty;

        public double Temperature { get; set; }

        public double Rating { get; set; }

        public double UserRating { get; set; }

        public int ResponseTimeMs { get; set; }

        public virtual Model Model { get; set; } = null!;

        public virtual Prompt Prompt { get; set; } = null!;
    }
}

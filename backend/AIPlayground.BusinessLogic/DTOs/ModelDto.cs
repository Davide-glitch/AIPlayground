namespace AIPlayground.BusinessLogic.DTOs
{
    public class ModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int PlatformId { get; set; }

        public double AverageRating { get; set; }
    }
}

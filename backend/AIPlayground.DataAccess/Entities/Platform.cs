namespace AiPlayground.DataAccess.Entities
{
    public class Platform
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Model> Models { get; set; } = new HashSet<Model>();
    }
}

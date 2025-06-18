namespace AiPlayground.DataAccess.Entities
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int PlatformId { get; set; }

        public virtual Platform Platform { get; set; } = null!;

        public virtual ICollection<Run> Runs { get; set; } = new HashSet<Run>();
    }
}

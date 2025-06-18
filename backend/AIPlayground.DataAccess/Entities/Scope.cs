namespace AiPlayground.DataAccess.Entities
{
    public class Scope
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Prompt> Prompts { get; set; } = new HashSet<Prompt>();
    }
}

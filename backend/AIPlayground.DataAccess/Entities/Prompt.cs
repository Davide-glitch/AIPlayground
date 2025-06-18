namespace AiPlayground.DataAccess.Entities
{
    public class Prompt
    {
        public int Id { get; set; }

        public int ScopeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SystemMessage { get; set; } = string.Empty;

        public string UserMessage { get; set; } = string.Empty;

        public string ExpectedResult { get; set; } = string.Empty;

        public virtual Scope Scope { get; set; } = null!;

        public virtual ICollection<Run> Runs { get; set; } = new HashSet<Run>();
    }
}

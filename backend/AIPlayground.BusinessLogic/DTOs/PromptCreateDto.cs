namespace AIPlayground.BusinessLogic.DTOs
{
    public class PromptCreateDto
    {
        public int ScopeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SystemMessage { get; set; } = string.Empty;

        public string UserMessage { get; set; } = string.Empty;

        public string ExpectedResult { get; set; } = string.Empty;
    }
}

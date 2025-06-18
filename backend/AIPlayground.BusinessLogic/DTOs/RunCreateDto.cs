namespace AIPlayground.BusinessLogic.DTOs
{
    public class RunCreateDto
    {
        public int PromptId { get; set; }

        public List<ModelRunDto> ModelsToRun { get; set; } = new List<ModelRunDto>();
    }
}

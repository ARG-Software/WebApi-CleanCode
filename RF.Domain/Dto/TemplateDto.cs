namespace RF.Domain.Dto
{
    public class TemplateDto
    {
        public int Id { get; set; }

        public int? SourceId { get; set; }

        public string Name { get; set; }

        public string Definition { get; set; }
    }
}
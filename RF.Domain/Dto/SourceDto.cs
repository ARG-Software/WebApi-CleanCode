using System.Collections.Generic;

namespace RF.Domain.Dto
{
    public class SourceDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TemplateDto> Templates { get; set; }
    }
}
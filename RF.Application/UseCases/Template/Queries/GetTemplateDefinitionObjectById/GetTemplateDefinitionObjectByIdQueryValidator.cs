using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Queries.GetTemplateDefinitionObjectById
{
    public class GetTemplateDefinitionObjectByIdQueryValidator : AbstractValidator<GetTemplateDefinitionObjectByIdQuery>
    {
        public GetTemplateDefinitionObjectByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
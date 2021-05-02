using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Queries.GetSourceIdByTemplateId
{
    public class GetSourceIdByTemplateIdQueryValidator : AbstractValidator<GetSourceIdByTemplateIdQuery>
    {
        public GetSourceIdByTemplateIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
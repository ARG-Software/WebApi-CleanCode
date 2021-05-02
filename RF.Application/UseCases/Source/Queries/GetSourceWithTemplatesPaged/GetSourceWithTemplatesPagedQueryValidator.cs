using FluentValidation;

namespace RF.Application.Core.UseCases.Source.Queries.GetSourceWithTemplatesPaged
{
    public class GetSourceWithTemplatesPagedQueryValidator : AbstractValidator<GetSourceWithTemplatesPagedQuery>
    {
        public GetSourceWithTemplatesPagedQueryValidator()
        {
            RuleFor(x => x.SourceId).NotEmpty();
        }
    }
}
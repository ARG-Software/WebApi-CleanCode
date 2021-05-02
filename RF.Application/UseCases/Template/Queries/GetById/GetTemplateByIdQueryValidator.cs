using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Queries.GetById
{
    public class GetTemplateByIdQueryValidator : AbstractValidator<GetTemplateByIdQuery>
    {
        public GetTemplateByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
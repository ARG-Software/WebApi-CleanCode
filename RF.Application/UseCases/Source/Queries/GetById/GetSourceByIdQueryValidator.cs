using FluentValidation;

namespace RF.Application.Core.UseCases.Source.Queries.GetById
{
    public class GetSourceByIdQueryValidator : AbstractValidator<GetSourceByIdQuery>
    {
        public GetSourceByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
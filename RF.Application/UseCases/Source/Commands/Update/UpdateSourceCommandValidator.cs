using FluentValidation;

namespace RF.Application.Core.UseCases.Source.Commands.Update
{
    public class UpdateSourceCommandValidator : AbstractValidator<UpdateSourceCommand>
    {
        public UpdateSourceCommandValidator()
        {
            RuleFor(x => x.Source.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Source.Name).NotEmpty();
        }
    }
}
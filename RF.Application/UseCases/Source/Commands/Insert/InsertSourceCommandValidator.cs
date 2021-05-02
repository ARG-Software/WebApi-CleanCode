using FluentValidation;

namespace RF.Application.Core.UseCases.Source.Commands.Insert
{
    public class InsertSourceCommandValidator : AbstractValidator<InsertSourceCommand>
    {
        public InsertSourceCommandValidator()
        {
            RuleFor(x => x.Source.Name).NotEmpty();
        }
    }
}
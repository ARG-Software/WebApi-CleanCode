using FluentValidation;

namespace RF.Application.Core.UseCases.Source.Commands.Delete
{
    public class DeleteSourceCommandValidator : AbstractValidator<DeleteSourceCommand>
    {
        public DeleteSourceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Commands.Delete
{
    public class DeleteTemplateCommandValidator : AbstractValidator<DeleteTemplateCommand>
    {
        public DeleteTemplateCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Commands.Update
{
    public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateCommandValidator()
        {
            RuleFor(x => x.Template.Definition).NotEmpty().NotNull();
            RuleFor(x => x.Template.Name).NotEmpty().NotNull();
            RuleFor(x => x.Template.SourceId).NotNull().GreaterThan(0);
            RuleFor(x => x.Template.Id).NotNull().GreaterThan(0);
        }
    }
}
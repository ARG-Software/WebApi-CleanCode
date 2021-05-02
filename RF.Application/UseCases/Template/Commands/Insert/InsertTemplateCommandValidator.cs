using FluentValidation;

namespace RF.Application.Core.UseCases.Template.Commands.Insert
{
    public class InsertTemplateCommandValidator : AbstractValidator<InsertTemplateCommand>
    {
        public InsertTemplateCommandValidator()
        {
            RuleFor(x => x.Template.Definition).NotEmpty().NotNull();
            RuleFor(x => x.Template.Name).NotEmpty().NotNull();
            RuleFor(x => x.Template.SourceId).NotNull();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Template.Commands.Insert
{
    public class InsertTemplateCommand : IRequest
    {
        public TemplateDto Template { get; set; }
    }

    public class InsertCommandHandler : IRequestHandler<InsertTemplateCommand>
    {
        private readonly IReadRepository<Domain.Entities.Source> _sourceRepository;
        private readonly IWriteRepository<Domain.Entities.Template> _templateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InsertCommandHandler(IWriteRepository<Domain.Entities.Template> templateRepository,
            IReadRepository<Domain.Entities.Source> sourceRepository,
            IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(InsertTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = request.Template;

            var sourceForTemplate = await _sourceRepository.SingleAsync(x => x.Id == template.SourceId);

            var templateToInsert = new Domain.Entities.Template
            {
                Source = sourceForTemplate,
                Name = template.Name
            };

            templateToInsert.SetTemplateDefinition(template.Definition);

            _templateRepository.InsertRFEntity(templateToInsert);

            var wasInserted = await _unitOfWork.CommitAsync();

            if (!wasInserted)
            {
                throw new UpsertException("Couldn't insert" + nameof(Domain.Entities.Template) + " entity ");
            }

            return Unit.Value;
        }
    }
}
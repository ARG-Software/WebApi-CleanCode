using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Template.Commands.Update
{
    public class UpdateTemplateCommand : IRequest
    {
        public TemplateDto Template { get; set; }
    }

    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand>
    {
        private readonly IReadRepository<Domain.Entities.Source> _sourceRepository;
        private readonly IReadWriteRepository<Domain.Entities.Template> _templateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTemplateCommandHandler(IReadWriteRepository<Domain.Entities.Template> templateRepository,
            IReadRepository<Domain.Entities.Source> sourceRepository,
            IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var templateDto = request.Template;

            var sourceForTemplate = await _sourceRepository.SingleAsync(x => x.Id == templateDto.SourceId);

            if (sourceForTemplate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Source), request.Template.SourceId);
            }

            var templateToUpdate = await _templateRepository.SingleAsync(x => x.Id == templateDto.Id);

            if (templateToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Template), request.Template.Id);
            }

            if (!string.IsNullOrEmpty(templateDto.Definition))
            {
                try
                {
                    templateToUpdate.SetTemplateDefinition(templateDto.Definition);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            _templateRepository.UpdateRFEntity(templateToUpdate);

            var wasUpdated = await _unitOfWork.CommitAsync();

            if (!wasUpdated)
            {
                throw new UpsertException("Couldn't update" + nameof(Domain.Entities.Template) + " entity ");
            }

            return Unit.Value;
        }
    }
}
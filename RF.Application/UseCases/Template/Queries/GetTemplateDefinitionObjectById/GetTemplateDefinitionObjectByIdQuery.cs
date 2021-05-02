using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Interfaces.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Application.Core.UseCases.Template.Queries.GetTemplateDefinitionObjectById
{
    public class GetTemplateDefinitionObjectByIdQuery : IRequest<ITemplateDefinition>
    {
        public int Id { get; set; }
    }

    public class GetTemplateDefinitionObjectByIdQueryHandler : IRequestHandler<GetTemplateDefinitionObjectByIdQuery, ITemplateDefinition>
    {
        private readonly IReadRepository<Domain.Entities.Template> _templateRepository;

        public GetTemplateDefinitionObjectByIdQueryHandler(IReadRepository<Domain.Entities.Template> templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<ITemplateDefinition> Handle(GetTemplateDefinitionObjectByIdQuery request, CancellationToken cancellationToken)
        {
            var templateId = request.Id;

            ITemplate templateEntity = await _templateRepository.SingleAsync(x => x.Id == templateId);

            if (templateEntity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Template), request.Id);
            }

            return templateEntity.GetTemplateDefinitionObject();
        }
    }
}
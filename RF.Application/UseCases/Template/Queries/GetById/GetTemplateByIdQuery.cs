using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Template.Queries.GetById
{
    public class GetTemplateByIdQuery : IRequest<TemplateDto>
    {
        public int Id { get; set; }
    }

    public class GetTemplateByIdQueryHandler : IRequestHandler<GetTemplateByIdQuery, TemplateDto>
    {
        private readonly IReadRepository<Domain.Entities.Template> _templateRepository;

        public GetTemplateByIdQueryHandler(IReadRepository<Domain.Entities.Template> templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<TemplateDto> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var templateId = request.Id;

            var templateEntity = await _templateRepository.SingleAsync(x => x.Id == templateId);
            if (templateEntity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Template), request.Id);
            }
            var templateToReturn = new TemplateDto()
            {
                Id = templateEntity.Id,
                Name = templateEntity.Name,
                Definition = templateEntity.Definition
            };
            return templateToReturn;
        }
    }
}
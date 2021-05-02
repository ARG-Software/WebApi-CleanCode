using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Template.Queries.GetSourceIdByTemplateId
{
    public class GetSourceIdByTemplateIdQuery : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class GetSourceIdByTemplateIdQueryHandler : IRequestHandler<GetSourceIdByTemplateIdQuery, int>
    {
        private readonly IReadRepository<Domain.Entities.Template> _templateRepository;

        public GetSourceIdByTemplateIdQueryHandler(IReadRepository<Domain.Entities.Template> templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<int> Handle(GetSourceIdByTemplateIdQuery request, CancellationToken cancellationToken)
        {
            var templateId = request.Id;
            var sourceForTemplate = (await _templateRepository.SingleAsync(x => x.Id == templateId, null, true, x => x.Source))?.Source;
            if (sourceForTemplate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Source), request.Id);
            }

            return sourceForTemplate.Id;
        }
    }
}
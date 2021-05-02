using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Source.Queries.GetSourceWithTemplatesPaged
{
    public class GetSourceWithTemplatesPagedQuery : IRequest<SourceDto>
    {
        public int SourceId { get; set; }
        public PagingOptionsDto Options { get; set; }
    }

    public class GetSourceWithTemplatesPagedQueryHandler : IRequestHandler<GetSourceWithTemplatesPagedQuery, SourceDto>
    {
        private readonly IReadRepository<Domain.Entities.Source> _sourceRepository;

        public GetSourceWithTemplatesPagedQueryHandler(IReadRepository<Domain.Entities.Source> sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDto> Handle(GetSourceWithTemplatesPagedQuery request, CancellationToken cancellationToken)
        {
            var sourceId = request.SourceId;
            var paginationOptions = request.Options ?? new PagingOptionsDto();

            var sourceEntity = await _sourceRepository.SingleAsync(x => x.Id == sourceId, null, true, x => x.Templates);

            if (sourceEntity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Source), request.SourceId);
            }

            var templates = sourceEntity.Templates
                .Skip(paginationOptions.CurrentIndex * paginationOptions.HowManyPerPage)
                .Take(paginationOptions.HowManyPerPage).ToList();

            var sourceToReturn = new SourceDto
            {
                Id = sourceEntity.Id,
                Name = sourceEntity.Name,
                Templates = templates.ConvertAll(x => new TemplateDto()
                {
                    Name = x.Name,
                    Definition = x.Definition
                })
            };

            return sourceToReturn;
        }
    }
}
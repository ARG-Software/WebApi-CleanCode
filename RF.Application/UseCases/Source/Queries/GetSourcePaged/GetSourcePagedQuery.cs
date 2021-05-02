using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Source.Queries.GetSourcePaged
{
    public class GetSourcePagedQuery : IRequest<PagedSetDto<SourceDto>>
    {
        public PagingOptionsDto Options { get; set; }
    }

    public class GetSourcePagedQueryHandler : IRequestHandler<GetSourcePagedQuery, PagedSetDto<SourceDto>>
    {
        private readonly IReadRepository<Domain.Entities.Source> _sourceRepository;

        public GetSourcePagedQueryHandler(IReadRepository<Domain.Entities.Source> sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<PagedSetDto<SourceDto>> Handle(GetSourcePagedQuery request, CancellationToken cancellationToken)
        {
            var pagingOptions = request.Options ?? new PagingOptionsDto();

            var pagedDtos = (await _sourceRepository.GetListAsync(x => new SourceDto
            {
                Id = x.Id,
                Name = x.Name
            }, null, null, pagingOptions.CurrentIndex, pagingOptions.HowManyPerPage)).ToList();

            if (pagedDtos.Any())
            {
                return new PagedSetDto<SourceDto>
                {
                    Result = pagedDtos,
                    Total = _sourceRepository.CountTableSize()
                };
            }

            return new PagedSetDto<SourceDto>
            {
                Result = pagedDtos,
                Total = 0
            };
        }
    }
}
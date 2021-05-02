using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Source.Queries.GetById
{
    public class GetSourceByIdQuery : IRequest<SourceDto>
    {
        public int Id { get; set; }
    }

    public class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDto>
    {
        private readonly IReadRepository<Domain.Entities.Source> _sourceRepository;

        public GetSourceByIdQueryHandler(IReadRepository<Domain.Entities.Source> sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDto> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var sourceEntity = await _sourceRepository.SingleAsync(x => x.Id == request.Id);

            if (sourceEntity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Source), request.Id);
            }

            var sourceToReturn = new SourceDto
            {
                Id = sourceEntity.Id,
                Name = sourceEntity.Name
            };
            return sourceToReturn;
        }
    }
}
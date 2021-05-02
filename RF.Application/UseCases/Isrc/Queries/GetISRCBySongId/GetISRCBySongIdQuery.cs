using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Isrc.Queries.GetISRCBySongId
{
    public class GetIsrcBySongIdQuery : IRequest<int?>
    {
        public int SongId { get; set; }
    }

    public class GetIsrcBySongIdQueryHandler : IRequestHandler<GetIsrcBySongIdQuery, int?>
    {
        private readonly IReadRepository<ISRC> _isrcRepository;

        public GetIsrcBySongIdQueryHandler(IReadRepository<Domain.Entities.SongCodeIdentifier.ISRC> isrcRepository)
        {
            _isrcRepository = isrcRepository;
        }

        public async Task<int?> Handle(GetIsrcBySongIdQuery request, CancellationToken cancellationToken)
        {
            var songId = request.SongId;

            var isrcId = (await _isrcRepository.SingleAsync(x => x.SongId == songId && x.Default))?.Id;

            return isrcId;
        }
    }
}
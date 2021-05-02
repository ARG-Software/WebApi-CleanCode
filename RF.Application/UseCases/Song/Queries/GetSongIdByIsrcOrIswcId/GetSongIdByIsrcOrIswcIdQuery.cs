using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Song.Queries.GetSongIdByIsrcOrIswcId
{
    public class GetSongIdByIsrcOrIswcIdQuery : IRequest<int>
    {
        public int? IsrcId { get; set; }
        public int? IswcId { get; set; }

        public string Song { get; set; }

        public string SourceSongCode { get; set; }
    }

    public class GetSongIdByIsrcOrIswcIdQueryHandler : IRequestHandler<GetSongIdByIsrcOrIswcIdQuery, int>
    {
        private readonly IReadRepository<ISWC> _iswcRepository;
        private readonly IReadRepository<ISRC> _isrcRepository;

        public GetSongIdByIsrcOrIswcIdQueryHandler(IReadRepository<ISWC> iswcRepository, IReadRepository<Domain.Entities.SongCodeIdentifier.ISRC> isrcRepository)
        {
            _iswcRepository = iswcRepository;
            _isrcRepository = isrcRepository;
        }

        public async Task<int> Handle(GetSongIdByIsrcOrIswcIdQuery request, CancellationToken cancellationToken)
        {
            var isrcId = request.IsrcId;
            var iswcId = request.IswcId;

            int? songId = null;
            if (isrcId.HasValue)
            {
                songId = (await _isrcRepository.SingleAsync(x => x.Id == isrcId.Value))?.SongId;
            }

            if (iswcId.HasValue && songId == null)
            {
                songId = (await _iswcRepository.SingleAsync(x => x.Id == iswcId.Value))?.SongId;
            }

            return songId ?? 0;
        }
    }
}
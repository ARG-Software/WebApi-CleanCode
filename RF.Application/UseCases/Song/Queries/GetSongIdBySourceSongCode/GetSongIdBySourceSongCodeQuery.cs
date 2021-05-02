using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Song.Queries.GetSongIdBySourceSongCode
{
    public class GetSongIdBySourceSongCodeOrAliasQuery : IRequest<int>
    {
        public string SourceSongCode { get; set; }
        public string Song { get; set; }
    }

    public class GetSongIdBySourceSongCodeOrAliasQueryHandler : IRequestHandler<GetSongIdBySourceSongCodeOrAliasQuery, int>
    {
        private readonly IReadRepository<SourceSongCode> _sourceSongCodeRepository;
        private readonly IReadRepository<SongAlias> _songAliasRepository;

        public GetSongIdBySourceSongCodeOrAliasQueryHandler(IReadRepository<SourceSongCode> sourceSongCodeRepository,
            IReadRepository<SongAlias> songAliasRepository
            )
        {
            _sourceSongCodeRepository = sourceSongCodeRepository;
            _songAliasRepository = songAliasRepository;
        }

        public async Task<int> Handle(GetSongIdBySourceSongCodeOrAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.SourceSongCode))
            {
                if (!string.IsNullOrEmpty(request?.Song))
                {
                    return await GetSongIdByAlias(request.Song);
                }

                return 0;
            }

            var song = request.Song;
            var sourceSongCode = request.SourceSongCode.ToLower();

            var songId = (await _sourceSongCodeRepository.SingleAsync(x =>
                string.Equals(x.Code.ToLower(), sourceSongCode)))?.SongId
                         ?? await GetSongIdByAlias(song);

            return songId;
        }

        private async Task<int> GetSongIdByAlias(string song)
        {
            var songLower = song.ToLower();
            var songId = (await _songAliasRepository
                .SingleAsync(x => string.Equals(x.Name.ToLower(), songLower)))?.SongId;
            return songId ?? 0;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Episode.Queries.GetEpisodeIdByAlias
{
    public class GetEpisodeIdByAliasQuery : IRequest<int?>
    {
        public string EpisodeAlias { get; set; }
    }

    public class GetEpisodeIdByAliasQueryHandler : IRequestHandler<GetEpisodeIdByAliasQuery, int?>
    {
        private readonly IReadRepository<EpisodeAlias> _episodeAliasRepository;

        public GetEpisodeIdByAliasQueryHandler(IReadRepository<EpisodeAlias> episodeAliasRepository)
        {
            _episodeAliasRepository = episodeAliasRepository;
        }

        public async Task<int?> Handle(GetEpisodeIdByAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.EpisodeAlias))
            {
                return null;
            }

            var episodeAlias = request.EpisodeAlias.ToLower();

            var episodeId = (await _episodeAliasRepository.SingleAsync(x => string.Equals(x.Name.ToLower(), episodeAlias)))?.EpisodeId;

            return episodeId;
        }
    }
}
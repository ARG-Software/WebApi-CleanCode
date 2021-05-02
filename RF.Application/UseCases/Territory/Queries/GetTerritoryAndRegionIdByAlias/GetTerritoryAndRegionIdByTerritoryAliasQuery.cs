using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Territory.Queries.GetTerritoryAndRegionIdByAlias
{
    public class GetTerritoryAndRegionIdByTerritoryAliasQuery : IRequest<TerritoryIdAndRegionIdDto>
    {
        public string TerritoryAlias { get; set; }
    }

    public class GetTerritoryAndRegionIdByTerritoryAliasQueryHandler : IRequestHandler<
        GetTerritoryAndRegionIdByTerritoryAliasQuery, TerritoryIdAndRegionIdDto>
    {
        private readonly IReadRepository<TerritoryAlias> _territoryAliasRepository;

        public GetTerritoryAndRegionIdByTerritoryAliasQueryHandler(IReadRepository<TerritoryAlias> territoryAliasRepository)
        {
            _territoryAliasRepository = territoryAliasRepository;
        }

        public async Task<TerritoryIdAndRegionIdDto> Handle(GetTerritoryAndRegionIdByTerritoryAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.TerritoryAlias))
            {
                return new TerritoryIdAndRegionIdDto()
                {
                    TerritoryId = null,
                    RegionId = null
                };
            }

            var territoryAlias = request.TerritoryAlias.ToLower();

            var territoryAliasEntity = await _territoryAliasRepository.SingleAsync(
                x => string.Equals(x.Name.ToLower(), territoryAlias), null, true,
                x => x.Territory);

            var territoryId = territoryAliasEntity?.TerritoryId;
            var regionId = territoryAliasEntity?.Territory?.RegionId;

            return new TerritoryIdAndRegionIdDto()
            {
                TerritoryId = territoryId,
                RegionId = regionId
            };
        }
    }
}
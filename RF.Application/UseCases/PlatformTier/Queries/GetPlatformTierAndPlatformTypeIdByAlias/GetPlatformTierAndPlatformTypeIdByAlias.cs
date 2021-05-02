using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.PlatformTier.Queries.GetPlatformTierAndPlatformTypeIdByAlias
{
    public class GetPlatformTierAndPlatformTypeIdByAliasQuery : IRequest<PlatformTierIdAndPlatformTypeIdDto>
    {
        public string PlatformTierAlias { get; set; }
    }

    public class GetPlatformTierAndPlatformTypeIdByAliasQueryHandler : IRequestHandler<
        GetPlatformTierAndPlatformTypeIdByAliasQuery, PlatformTierIdAndPlatformTypeIdDto>
    {
        private readonly IReadRepository<PlatformTierAlias> _platformTierAliasRepository;

        public GetPlatformTierAndPlatformTypeIdByAliasQueryHandler(IReadRepository<PlatformTierAlias> platformTierAliasRepository)
        {
            _platformTierAliasRepository = platformTierAliasRepository;
        }

        public async Task<PlatformTierIdAndPlatformTypeIdDto> Handle(GetPlatformTierAndPlatformTypeIdByAliasQuery request, CancellationToken cancellationToken)
        {
            var platformTierAlias = request.PlatformTierAlias;

            if (string.IsNullOrEmpty(platformTierAlias))
            {
                return new PlatformTierIdAndPlatformTypeIdDto()
                {
                    PlatformTypeId = null,
                    PlatformTierId = null
                };
            }

            var platformTierAliasEntity = await _platformTierAliasRepository.SingleAsync(
                x => string.Equals(x.Name.ToLower(), platformTierAlias.ToLower()), null, true,
                x => x.PlatformTier, x => x.PlatformTier.Platform);

            var platformTierId = platformTierAliasEntity?.PlatformTierId;
            var platformTypeId = platformTierAliasEntity?.PlatformTier?.Platform.PlatformTypeId;

            return new PlatformTierIdAndPlatformTypeIdDto()
            {
                PlatformTypeId = platformTypeId,
                PlatformTierId = platformTierId
            };
        }
    }
}
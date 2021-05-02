using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.RoyaltyType.Queries.GetRoyaltyTypeIdAndRoyaltyTypeGroupIdByAliasAndSourceId
{
    public class GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery : IRequest<RoyaltyTypeIdAndRoyaltyTypeGroupIdDto>
    {
        public string RoyaltyTypeAlias { get; set; }
        public int SourceId { get; set; }
    }

    public class GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryHandler : IRequestHandler<GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery, RoyaltyTypeIdAndRoyaltyTypeGroupIdDto>
    {
        private readonly IReadRepository<RoyaltyTypeAlias> _royaltyTypeAliasRepository;

        public GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryHandler(IReadRepository<RoyaltyTypeAlias> royaltyTypeAliasRepository)
        {
            _royaltyTypeAliasRepository = royaltyTypeAliasRepository;
        }

        public async Task<RoyaltyTypeIdAndRoyaltyTypeGroupIdDto> Handle(GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.RoyaltyTypeAlias))
            {
                return new RoyaltyTypeIdAndRoyaltyTypeGroupIdDto()
                {
                    RoyaltyTypeId = null,
                    RoyaltyTypeGroupId = null
                };
            }

            var royaltyTypeAlias = request.RoyaltyTypeAlias.ToLower();
            var sourceId = request.SourceId;

            var royaltyTypeAliasEntity = await _royaltyTypeAliasRepository.SingleAsync(
                x => string.Equals(x.Name.ToLower(), royaltyTypeAlias) && x.SourceId == sourceId, null, true,
                x => x.RoyaltyType);

            var royaltyTypeId = royaltyTypeAliasEntity?.RoyaltyTypeId;
            var royaltyTypeGroupId = royaltyTypeAliasEntity?.RoyaltyType?.RoyaltyTypeGroupId;

            return new RoyaltyTypeIdAndRoyaltyTypeGroupIdDto()
            {
                RoyaltyTypeId = royaltyTypeId,
                RoyaltyTypeGroupId = royaltyTypeGroupId
            };
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.ProductionTitle.Queries.GetProductionTitleAndTypeIdByAlias
{
    public class GetProductionTitleAndTypeIdByAliasQuery : IRequest<ProductionTitleIdAndProductionTitleTypeIdDto>
    {
        public string ProductionTitleAlias { get; set; }
    }

    public class GetProductionTitleAndTypeIdByAliasQueryHandler : IRequestHandler<
        GetProductionTitleAndTypeIdByAliasQuery, ProductionTitleIdAndProductionTitleTypeIdDto>
    {
        private readonly IReadRepository<ProductionTitleAlias> _productionTitleAliasRepository;

        public GetProductionTitleAndTypeIdByAliasQueryHandler(IReadRepository<ProductionTitleAlias> productionTitleAliasRepository)
        {
            _productionTitleAliasRepository = productionTitleAliasRepository;
        }

        public async Task<ProductionTitleIdAndProductionTitleTypeIdDto> Handle(GetProductionTitleAndTypeIdByAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.ProductionTitleAlias))
            {
                return new ProductionTitleIdAndProductionTitleTypeIdDto()
                {
                    ProductionTitleTypeId = null,
                    ProductionTitleId = null
                };
            }

            var productionTitleAlias = request.ProductionTitleAlias.ToLower();

            var productionTitleAliasEntity = await _productionTitleAliasRepository.SingleAsync(
                x => string.Equals(x.Name.ToLower(), productionTitleAlias), null, true,
                x => x.ProductionTitle);

            var productionTitleId = productionTitleAliasEntity?.ProductionTitleId;
            var productionTypeId = productionTitleAliasEntity?.ProductionTitle?.ProductionTypeId;

            return new ProductionTitleIdAndProductionTitleTypeIdDto()
            {
                ProductionTitleId = productionTitleId,
                ProductionTitleTypeId = productionTypeId
            };
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Label.Queries.GetLabelIdByAlias
{
    public class GetLabelIdByAliasQuery : IRequest<int?>
    {
        public string LabelAlias { get; set; }
    }

    public class GetLabelIdByAliasQueryHandler : IRequestHandler<GetLabelIdByAliasQuery, int?>
    {
        private readonly IReadRepository<LabelAlias> _labelAliasRepository;

        public GetLabelIdByAliasQueryHandler(IReadRepository<LabelAlias> labelAliasRepository)
        {
            _labelAliasRepository = labelAliasRepository;
        }

        public async Task<int?> Handle(GetLabelIdByAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.LabelAlias))
            {
                return null;
            }

            var labelAlias = request.LabelAlias.ToLower();

            var labelId = (await _labelAliasRepository.SingleAsync(x => string.Equals(x.Name.ToLower(), labelAlias)))?.LabelId;

            return labelId;
        }
    }
}
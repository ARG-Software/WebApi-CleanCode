using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Society.Queries.GetSocietyIdByAlias
{
    public class GetSocietyIdByAliasQuery : IRequest<int?>
    {
        public string SocietyAlias { get; set; }
    }

    public class GetSocietyIdByAliasQueryHandler : IRequestHandler<GetSocietyIdByAliasQuery, int?>
    {
        private readonly IReadRepository<SocietyAlias> _societyAliasRepository;

        public GetSocietyIdByAliasQueryHandler(IReadRepository<SocietyAlias> societyAliasRepository)
        {
            _societyAliasRepository = societyAliasRepository;
        }

        public async Task<int?> Handle(GetSocietyIdByAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.SocietyAlias))
            {
                return null;
            }

            var societyAlias = request.SocietyAlias.ToLower();

            var societyId = (await _societyAliasRepository.SingleAsync(x => string.Equals(x.Name.ToLower(), societyAlias)))?.SocietyId;

            return societyId;
        }
    }
}
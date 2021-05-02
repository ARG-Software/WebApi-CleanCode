using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;

namespace RF.Application.Core.UseCases.Publisher.Queries.GetPublisherIdByAlias
{
    public class GetPublisherIdByAliasQuery : IRequest<int?>
    {
        public string PublisherAlias { get; set; }
    }

    public class GetPublisherIdByAliasQueryHandler : IRequestHandler<GetPublisherIdByAliasQuery, int?>
    {
        private readonly IReadRepository<PublisherAlias> _publisherAliasRepository;

        public GetPublisherIdByAliasQueryHandler(IReadRepository<PublisherAlias> publisherAliasRepository)
        {
            _publisherAliasRepository = publisherAliasRepository;
        }

        public async Task<int?> Handle(GetPublisherIdByAliasQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.PublisherAlias))
            {
                return null;
            }

            var publisherAlias = request.PublisherAlias.ToLower();

            var publisherId = (await _publisherAliasRepository.SingleAsync(x => string.Equals(x.Name.ToLower(), publisherAlias)))?.PublisherId;

            return publisherId;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Interfaces.Bus;

namespace RF.Infrastructure.Bus
{
    public class MediatrBus:IMemoryBus
    {

        private readonly IMediator _mediator;

        public MediatrBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
           return _mediator.Publish(notification, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }
        }
}

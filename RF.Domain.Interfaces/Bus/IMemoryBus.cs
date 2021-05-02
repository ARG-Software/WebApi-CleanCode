using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RF.Domain.Interfaces.Bus
{
    public interface IMemoryBus
    {

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default (CancellationToken));

        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default (CancellationToken)) where TNotification : INotification;
       
    }
}
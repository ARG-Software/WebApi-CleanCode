using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Domain.Interfaces.Logger;

namespace RF.Application.Core.Common.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRoyaltyManagerLogger _logger;

        public LoggingBehavior(IRoyaltyManagerLogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.JobDebug("", $"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.JobDebug("", $"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}
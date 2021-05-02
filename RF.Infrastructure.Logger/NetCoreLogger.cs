using System;
using Microsoft.Extensions.Logging;
using RF.Domain.Interfaces.Logger;

namespace RF.Infrastructure.Logger
{
    public class NetCoreLogger : IRoyaltyManagerLogger
    {
        private readonly ILogger _logger;

        public NetCoreLogger(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger("RoyaltyManager");
        }

        public void JobDebug(string jobName, string message)
        {
            _logger.LogDebug(jobName, message);
        }

        public void JobDebug(string jobName, string message, Exception exception)
        {
            _logger.LogDebug(exception, jobName, message);
        }

        public void JobWarning(string jobName, string message)
        {
            _logger.LogWarning(jobName, message);
        }

        public void JobWarning(string jobName, string message, Exception exception)
        {
            _logger.LogWarning(exception, jobName, message);
        }

        public void JobError(string jobName, string message)
        {
            _logger.LogError(jobName, message);
        }

        public void JobError(string jobName, string message, Exception exception)
        {
            _logger.LogError(exception, jobName, message);
        }
    }
}
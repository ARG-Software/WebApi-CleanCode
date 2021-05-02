//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using RF.Application.Core.Notifications.Logging.Message;
//using RF.Domain.Enum;

//namespace RF.Application.Core.Notifications.Logging
//{
//    public class LogNotificationHandler : INotificationHandler<LogMessage>, INotification
//    {
//        private readonly ILogger<LogMessage> _logger;

//        public LogNotificationHandler(ILogger<LogMessage> logger)
//        {
//            _logger = logger;
//        }

//        public async Task Handle(LogMessage notification, CancellationToken cancellationToken)
//        {
//            var logType = notification.LogMessageType;
//            var message = notification.Message;

//            if (_logger == null)
//            {
//                return;
//            }

//            switch (logType)
//            {
//                case LogTypeEnum.Debug:
//                    {
//                        _logger.LogDebug(message);
//                        break;
//                    }
//                case LogTypeEnum.Critical:
//                    {
//                        _logger.LogCritical(message);
//                        break;
//                    }
//                case LogTypeEnum.Error:
//                    {
//                        _logger.LogError(message);
//                        break;
//                    }
//                case LogTypeEnum.Information:
//                    {
//                        _logger.LogInformation(message);
//                        break;
//                    }
//                case LogTypeEnum.Warning:
//                    {
//                        _logger.LogWarning(message);
//                        break;
//                    }
//                default:
//                    {
//                        throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
//                    }
//            }
//        }
//    }
//}
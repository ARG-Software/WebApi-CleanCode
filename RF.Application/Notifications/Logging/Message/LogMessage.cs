using MediatR;
using RF.Domain.Enum;

namespace RF.Application.Core.Notifications.Logging.Message
{
    public class LogMessage : INotification
    {
        public LogTypeEnum LogMessageType { get; set; }
        public string Message { get; set; }
    }
}
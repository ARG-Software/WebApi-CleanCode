using Hangfire.Server;
using MediatR;

namespace RF.Application.Core.Notifications.Logging.Message
{
    public class LogBackgroundJobMessage : INotification
    {
        public PerformContext BackgroudJobContext { get; set; }
    }
}
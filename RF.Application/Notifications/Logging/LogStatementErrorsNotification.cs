using System.Threading;
using System.Threading.Tasks;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Application.Core.Notifications.Logging
{
    public class LogStatementErrorsNotification : INotification
    {
        public PerformContext BackgroundJobContext { get; set; }
    }

    public class LogStatementErrorsNotificationHandler : INotificationHandler<LogStatementErrorsNotification>, INotification
    {
        private readonly IStatementErrors _errors;

        public LogStatementErrorsNotificationHandler(IStatementErrors errors)
        {
            _errors = errors;
        }

        public async Task Handle(LogStatementErrorsNotification notification, CancellationToken cancellationToken)
        {
            var jobContext = notification.BackgroundJobContext;

            var errors = _errors.GetStatementErrors();

            foreach (var (key, value) in errors)
            {
                jobContext.SetTextColor(ConsoleTextColor.Green);
                jobContext.WriteLine($"{key}");
                jobContext.SetTextColor(ConsoleTextColor.Red);
                foreach (var message in value)
                {
                    jobContext.WriteLine(message);
                }
            }
        }
    }
}
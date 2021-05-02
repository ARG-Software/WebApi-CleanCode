using System;
using Serilog;

namespace RF.Infrastructure.Logger
{
    public static class LogExtensions
    {
        public static ILogger AddSerilogFileExtension()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/Logs/Log-{Date}.txt")
                .CreateLogger();
        }
    }
}
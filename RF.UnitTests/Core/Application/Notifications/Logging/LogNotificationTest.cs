using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using RF.Application.Core.Notifications.Logging.Message;
using RF.Domain.Enum;
using Xunit;
using Times = Moq.Times;

namespace RF.UnitTests.Core.Application.Notifications.Logging
{
    public class LogNotificationTest
    {
        //    private readonly Mock<ILogger<LogMessage>> _fakeLogger;
        //    private LogNotificationHandler _handler;

        //    public LogNotificationTest()
        //    {
        //        _fakeLogger = new Mock<ILogger<LogMessage>>();
        //        _handler = new LogNotificationHandler(_fakeLogger.Object);
        //    }

        //    [Fact]
        //    public async void LogNotification_MessageLogForDebug_Success()
        //    {
        //        //Arrange
        //        var fakeMessage = new LogMessage()
        //        {
        //            LogMessageType = LogTypeEnum.Debug,
        //            Message = "test"
        //        };

        //        //Act
        //        await _handler.Handle(fakeMessage, CancellationToken.None);

        //        //Assert
        //        _fakeLogger.Verify(
        //            x => x.Log(
        //                LogLevel.Debug,
        //                It.IsAny<EventId>(),
        //                It.Is<It.IsAnyType>((o, t) => string.Equals(fakeMessage.Message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
        //                It.IsAny<Exception>(),
        //                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
        //            Times.Once);
        //    }

        //    [Fact]
        //    public async void LogNotification_MessageLogForDebugIfLoggerIsNull_DoesntThrowException()
        //    {
        //        //Arrange
        //        _handler = new LogNotificationHandler(null);
        //        var fakeMessage = new LogMessage()
        //        {
        //            LogMessageType = LogTypeEnum.Debug,
        //            Message = "test"
        //        };

        //        //Act && Assert
        //        try
        //        {
        //            await _handler.Handle(fakeMessage, CancellationToken.None);
        //        }
        //        catch (Exception ex)
        //        {
        //            Assert.True(false, "Expected no exception, but got: " + ex.Message);
        //        }
        //    }
    }
}
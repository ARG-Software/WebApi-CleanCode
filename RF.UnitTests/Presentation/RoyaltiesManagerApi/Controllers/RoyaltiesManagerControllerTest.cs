using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RF.Domain.Dto;
using RF.RoyaltiesManagerApi.Controllers;
using RF.Services.Interfaces.ETL;
using Xunit;

namespace RF.UnitTests.Presentation.RoyaltiesManagerApi.Controllers
{
    public class StatementControllerTest
    {
        private readonly IETLService _fakeService;
        private readonly IBackgroundJobClient _fakeBackgroundJobClient;
        private readonly RoyaltiesManagerController _controller;
        private readonly Fixture _fixture;
        private const int FakeTemplateId = 2;
        private const int FakePaymentId = 1;
        private string[] FakeTags;

        public StatementControllerTest()
        {
            _fakeService = A.Fake<IETLService>();
            _fakeBackgroundJobClient = A.Fake<IBackgroundJobClient>();
            _controller = new RoyaltiesManagerController(_fakeService, _fakeBackgroundJobClient);
            _fixture = new Fixture();
            FakeTags = new string[] { };
        }

        [Fact]
        public async Task ProcessStatement_IfRequestIsValid_ReturnsStatementEnqueueMessage()
        {
            //Arrange
            var fakeFile = CreateFakeStreamFile(true);

            //Act
            var result = await _controller.ProcessStatement(fakeFile, FakeTemplateId, FakePaymentId, FakeTags);
            var messageOfSuccess = result as ObjectResult;

            //Assert

            Assert.NotNull(result);
            Assert.Equal("Statement added to queue to be processed", (messageOfSuccess?.Value));
        }

        [Fact]
        public async Task ProcessStatement_IfParseStatementFileReturnsException_ReturnBadRequest()
        {
            //Arrange
            var fakeFile = CreateFakeStreamFile(true);
            const string fakeMessageException = "Error Parsing File!";

            A.CallTo(() => _fakeService.ParseStatementFile(A<int>.Ignored, A<FileStream>.Ignored))
                .Throws(new Exception(fakeMessageException));

            //Act
            var result = await _controller.ProcessStatement(fakeFile, FakeTemplateId, FakePaymentId, FakeTags);
            var messageOfBadRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(fakeMessageException, (messageOfBadRequestResult?.Value));
        }

        [Fact]
        public async Task ProcessStatement_IfStreamFileEmpty_ReturnsBadRequest()
        {
            //Arrange
            var fakeFile = CreateFakeStreamFile(false);

            //Act
            var result = await _controller.ProcessStatement(fakeFile, FakeTemplateId, FakePaymentId, FakeTags);
            var messageOfBadRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("File Is Empty Or Null", messageOfBadRequestResult?.Value);
        }

        [Fact]
        public async Task ProcessStatement_IfStreamFileNull_ReturnsBadRequest()
        {
            //Act
            var result = await _controller.ProcessStatement(null, FakeTemplateId, FakePaymentId, FakeTags);
            var messageOfBadRequestResult = result as BadRequestObjectResult;

            //Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("File Is Empty Or Null", messageOfBadRequestResult?.Value);
        }

        [Fact]
        public async Task ProcessStatement_IfSTemplateIdIs0_ReturnsBadRequest()
        {
            //Act
            var result = await _controller.ProcessStatement(null, 0, FakePaymentId, FakeTags);
            var messageOfBadRequestResult = result as BadRequestObjectResult;

            //Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Template Id is not valid, equal to 0", messageOfBadRequestResult?.Value);
        }

        [Fact]
        public async Task ProcessStatement_IfPaymentIdIs0_ReturnsBadRequest()
        {
            //Act
            var result = await _controller.ProcessStatement(null, FakeTemplateId, 0, FakeTags);
            var messageOfBadRequestResult = result as BadRequestObjectResult;

            //Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Payment Id is not valid, equal to 0", messageOfBadRequestResult?.Value);
        }

        private static IFormFile CreateFakeStreamFile(bool hasContent)
        {
            var fakeFile = A.Fake<IFormFile>();
            var content = (hasContent) ? "Hello World from a Fake File" : "";
            var fakeFileName = "test.xlsx";
            var fakeMemoryStream = new MemoryStream();
            var writer = new StreamWriter(fakeMemoryStream);
            writer.Write(content);
            writer.Flush();

            A.CallTo(() => fakeFile.CopyToAsync(A<FileStream>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.CompletedTask);
            A.CallTo(() => fakeFile.FileName).Returns(fakeFileName);
            A.CallTo(() => fakeFile.Length).Returns(fakeMemoryStream.Length);

            return fakeFile;
        }
    }
}
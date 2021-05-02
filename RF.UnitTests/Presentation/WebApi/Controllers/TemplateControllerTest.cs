using Xunit;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using RF.Application.Core.UseCases.Template.Commands.Delete;
using RF.Application.Core.UseCases.Template.Commands.Insert;
using RF.Application.Core.UseCases.Template.Commands.Update;
using RF.Application.Core.UseCases.Template.Queries.GetById;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Bus;

namespace RF.UnitTests.Presentation.WebApi.Controllers
{
    public class TemplateControllerTest
    {
        private readonly IMemoryBus _fakeBus;
        private readonly TemplateController _controller;
        private readonly Fixture _fixture;
        private readonly int _fakeTemplateId;

        public TemplateControllerTest()
        {
            _fakeBus = A.Fake<IMemoryBus>();
            _controller = new TemplateController(_fakeBus);
            _fixture = new Fixture();
            _fakeTemplateId = 1;
        }

        [Fact]
        public async void GetTemplate_ReturnsTemplateDto()
        {
            //Arrange
            var fakeTemplateDto = new TemplateDto()
            {
                Id = _fakeTemplateId,
                Name = "fake name"
            };

            A.CallTo(() => _fakeBus.Send(A<GetTemplateByIdQuery>.Ignored, CancellationToken.None))
                .Returns(fakeTemplateDto);

            //Act
            var result = await _controller.GetTemplateById(1, CancellationToken.None) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<TemplateDto>(result.Value);
            Assert.Equal(fakeTemplateDto, result.Value);
        }

        [Fact]
        public async void InsertTemplate_WasInserted_ReturnsNoContent()
        {
            //Arrange
            var fakeTemplateDto = new TemplateDto
            {
                Id = _fakeTemplateId,
                Name = "fake name"
            };

            //Act
            var result = await _controller.InsertTemplate(fakeTemplateDto, CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<InsertTemplateCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }

        [Fact]
        public async void UpdateTemplate_WasUpdated_ReturnsTrue()
        {
            //Arrange
            var fakeTemplateDto = new TemplateDto
            {
                Id = _fakeTemplateId,
                Name = "fake name"
            };
            //Act
            var result = await _controller.UpdateTemplate(fakeTemplateDto, CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<UpdateTemplateCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }

        [Fact]
        public async void DeleteTemplate_WasDeleted_ReturnsTrue()
        {
            //Act
            var result = await _controller.DeleteTemplate(1, CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<DeleteTemplateCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }
    }
}
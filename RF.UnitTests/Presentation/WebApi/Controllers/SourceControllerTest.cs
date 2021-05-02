using Xunit;
using System.Linq;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using RF.Application.Core.UseCases.Source.Commands.Delete;
using RF.Application.Core.UseCases.Source.Commands.Insert;
using RF.Application.Core.UseCases.Source.Commands.Update;
using RF.Application.Core.UseCases.Source.Queries.GetById;
using RF.Application.Core.UseCases.Source.Queries.GetSourcePaged;
using RF.Application.Core.UseCases.Source.Queries.GetSourceWithTemplatesPaged;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Bus;
using RF.WebApi.Controllers;

namespace RF.UnitTests.Presentation.WebApi.Controllers
{
    public class SourceControllerTest
    {
        private readonly IMemoryBus _fakeBus;
        private readonly SourceController _controller;
        private readonly Fixture _fixture;
        private readonly int _fakeSourceId;

        public SourceControllerTest()
        {
            _fakeBus = A.Fake<IMemoryBus>();
            _controller = new SourceController(_fakeBus);
            _fixture = new Fixture();
            _fakeSourceId = 1;
        }

        [Fact]
        public async void GetSource_ReturnsSourceDto()
        {
            //Arrange
            var fakeSourceDto = new SourceDto
            {
                Id = _fakeSourceId,
                Name = "fake name"
            };

            A.CallTo(() => _fakeBus.Send(A<GetSourceByIdQuery>.Ignored, CancellationToken.None))
                .Returns(fakeSourceDto);

            //Act
            var result = await _controller.GetSource(1, CancellationToken.None) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<SourceDto>(result.Value);
            Assert.Equal(fakeSourceDto, result.Value);
        }

        [Fact]
        public async void GetSourceWithTemplatesPaged_ReturnsSourceDto()
        {
            //Arrange
            var fakeSourceDto = new SourceDto
            {
                Id = _fakeSourceId,
                Name = "fake name",
                Templates = _fixture.CreateMany<TemplateDto>(2)
            };

            A.CallTo(() => _fakeBus.Send(A<GetSourceWithTemplatesPagedQuery>.Ignored, CancellationToken.None))
                .Returns(fakeSourceDto);

            //Act
            var result = await _controller.GetSourceWithTemplates(1, new PagingOptionsDto(), CancellationToken.None) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<SourceDto>(result.Value);
            Assert.Equal(fakeSourceDto, result.Value);
        }

        [Fact]
        public async void GetSourcesListPaged_ReturnsPagedSetDtoAsync()
        {
            //Arrange
            const int fakeListSize = 5;
            const int fakePagedTotal = 100;
            var fakePagedSources = _fixture.CreateMany<SourceDto>(fakeListSize).ToList();
            var fakeResult = new PagedSetDto<SourceDto>
            {
                Result = fakePagedSources,
                Total = fakePagedTotal
            };

            A.CallTo(() => _fakeBus.Send(A<GetSourcePagedQuery>.Ignored, CancellationToken.None))
                .Returns(fakeResult);

            //Act
            var result = await _controller.GetSourceListPaged(new PagingOptionsDto(), CancellationToken.None) as ObjectResult;
            var castedResult = result?.Value as PagedSetDto<SourceDto>;

            //Assert
            Assert.NotNull(castedResult);
            Assert.Equal(castedResult.Result, fakePagedSources);
            Assert.Equal(castedResult.Total, fakePagedTotal);
            Assert.Equal(castedResult.Result.Count(), fakeListSize);
            Assert.Equal(castedResult, fakeResult);
        }

        [Fact]
        public async void InsertSource_WasInserted_ReturnsNoContent()
        {
            //Arrange
            var fakeSourceDto = new SourceDto
            {
                Id = _fakeSourceId,
                Name = "fake name"
            };

            //Act
            var result = await _controller.InsertSource(fakeSourceDto, CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<InsertSourceCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }

        [Fact]
        public async void UpdateSource_WasUpdated_ReturnsNoContent()
        {
            //Arrange
            var fakeSourceDto = new SourceDto
            {
                Id = _fakeSourceId,
                Name = "fake name"
            };

            //Act
            var result = await _controller.UpdateSource(fakeSourceDto, CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<UpdateSourceCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }

        [Fact]
        public async void DeleteSource_WasDeleted_ReturnsNoContent()
        {
            //Act
            var result = await _controller.DeleteSource(1);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => _fakeBus.Send(A<DeleteSourceCommand>.Ignored, CancellationToken.None))
                .MustHaveHappened();
        }
    }
}
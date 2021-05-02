using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Source.Commands.Insert;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Command
{
    public class InsertSourceCommandTest
    {
        private readonly IWriteRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly InsertSourceCommandHandler _handler;
        private readonly InsertSourceCommand _request;
        private readonly InsertSourceCommandValidator _validator;

        private readonly Fixture _fixture;

        public InsertSourceCommandTest()
        {
            _fakeSourceRepository = A.Fake<IWriteRepository<RF.Domain.Entities.Source>>();
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _handler = new InsertSourceCommandHandler(_fakeSourceRepository, _fakeUnitOfWork);
            _request = new InsertSourceCommand();
            _validator = new InsertSourceCommandValidator();

            _fixture = new Fixture();
            _request.Source = _fixture.Create<SourceDto>();
        }

        [Fact]
        public async void InsertSourceCommandHandler_Success()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(true);

            //Act
            await _handler.Handle(_request, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeSourceRepository.InsertRFEntityAsync(A<RF.Domain.Entities.Source>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void InsertSourceCommandHandler_CouldntSaveToDatabase_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(false);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<UpsertException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal("Couldn't insert" + nameof(RF.Domain.Entities.Source) + " entity ", ex.Message);

            A.CallTo(() => _fakeSourceRepository.InsertRFEntityAsync(A<RF.Domain.Entities.Source>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void CreateInsertSourceCommand_WithNoName_Fail()
        {
            //Arrange
            _request.Source.Name = null;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        [Fact]
        public async void CreateInsertSourceCommand_Success()
        {
            //Arrange
            _request.Source.Name = "teste";

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }
    }
}
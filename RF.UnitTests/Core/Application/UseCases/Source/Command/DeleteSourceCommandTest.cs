using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Source.Commands.Delete;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Command
{
    public class DeleteSourceCommandTest
    {
        private readonly IWriteRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly DeleteSourceCommandHandler _handler;
        private readonly DeleteSourceCommandValidator _validator;
        private DeleteSourceCommand _request;

        public DeleteSourceCommandTest()
        {
            _fakeSourceRepository = A.Fake<IWriteRepository<RF.Domain.Entities.Source>>();
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _handler = new DeleteSourceCommandHandler(_fakeSourceRepository, _fakeUnitOfWork);
            _validator = new DeleteSourceCommandValidator();
            _request = new DeleteSourceCommand();
        }

        [Fact]
        public async void DeleteCommandHandler_Success()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(true);
            _request.Id = 1;

            //Act
            await _handler.Handle(_request, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeSourceRepository.DeleteRFEntityById(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteCommandHandler_SourceNotDeleted_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(false);
            _request.Id = 1;

            //Act && Assert
            var ex = await Assert.ThrowsAsync<DeleteFailureException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal("Deletion of entity \"Source\" (1) failed.", ex.Message);

            A.CallTo(() => _fakeSourceRepository.DeleteRFEntityById(A<int>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void CreateDeleteSourceCommand_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void CreateDeleteSourceCommand_WithIdEqual0_Fail()
        {
            //Arrange
            _request.Id = 0;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        [Fact]
        public async void CreateDeleteSourceCommand_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}
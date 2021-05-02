using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Template.Commands.Delete;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Template.Command
{
    public class DeleteTemplateCommandTest
    {
        private readonly IWriteRepository<RF.Domain.Entities.Template> _fakeTemplateRepository;
        private readonly IUnitOfWork _fakeUnitOfWork;

        private readonly DeleteTemplateCommandHandler _handler;
        private readonly DeleteTemplateCommand _request;
        private readonly DeleteTemplateCommandValidator _validator;

        public DeleteTemplateCommandTest()
        {
            _fakeTemplateRepository = A.Fake<IWriteRepository<RF.Domain.Entities.Template>>();
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _handler = new DeleteTemplateCommandHandler(_fakeTemplateRepository, _fakeUnitOfWork);
            _request = new DeleteTemplateCommand();
            _validator = new DeleteTemplateCommandValidator();
        }

        [Fact]
        public async void DeleteTemplateCommandHandler_Success()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(true);
            _request.Id = 1;

            //Act
            await _handler.Handle(_request, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeTemplateRepository.DeleteRFEntityById(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteTemplateCommandHandler_SourceNotDeleted_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).Returns(false);
            _request.Id = 1;

            //Act && Assert
            var ex = await Assert.ThrowsAsync<DeleteFailureException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal("Deletion of entity \"Template\" (1) failed.", ex.Message);

            A.CallTo(() => _fakeTemplateRepository.DeleteRFEntityById(A<int>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void CreateDeleteTemplateCommand_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void CreateDeleteTemplateCommand_WithIdEqual0_Fail()
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
        public async void CreateDeleteTemplateCommand_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}
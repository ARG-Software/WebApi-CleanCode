using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Source.Commands.Update;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Command
{
    public class UpdateSourceCommandTest
    {
        private readonly IReadWriteRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly UpdateSourceCommandHandler _handler;
        private readonly UpdateSourceCommandValidator _validator;
        private readonly UpdateSourceCommand _request;

        public UpdateSourceCommandTest()
        {
            _fakeSourceRepository = A.Fake<IReadWriteRepository<RF.Domain.Entities.Source>>();
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _handler = new UpdateSourceCommandHandler(_fakeSourceRepository, _fakeUnitOfWork);
            _validator = new UpdateSourceCommandValidator();
            _request = new UpdateSourceCommand();
            var fixture = new Fixture();
            _request.Source = fixture.Create<SourceDto>();
        }

        [Fact]
        public async void UpdateSourceCommandHandler_Success()
        {
            //Arrange
            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    false
                ))
                .Returns(A.Dummy<RF.Domain.Entities.Source>());
            A.CallTo(() => _fakeUnitOfWork.CommitAsync())
                .Returns(true);

            //Act
            await _handler.Handle(_request, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeSourceRepository.UpdateRFEntity(A<RF.Domain.Entities.Source>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUnitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateSourceCommandHandler_CouldntFindSourceToUpdate_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored, A<bool>.Ignored
                ))
                .Returns((RF.Domain.Entities.Source)null);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(RF.Domain.Entities.Source)}\" ({_request.Source.Id}) was not found.", ex.Message);
        }

        [Fact]
        public async void UpdateSourceCommandHandler_CouldntUpdateSource_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    false
                ))
                .Returns(A.Dummy<RF.Domain.Entities.Source>());
            A.CallTo(() => _fakeUnitOfWork.Commit())
                .Returns(false);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<UpsertException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal("Couldn't update" + nameof(RF.Domain.Entities.Source) + " entity ", ex.Message);
        }

        [Fact]
        public async void CreateUpdateSourceCommand_WithNoName_Fail()
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
        public async void CreateUpdateSourceCommand_WithNoId_Fail()
        {
            //Arrange
            _request.Source.Id = 0;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        [Fact]
        public async void CreateUpdateSourceCommand_Success()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }
    }
}
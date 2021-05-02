using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Source.Queries.GetById;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Queries
{
    public class GetSourceByIdQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly GetSourceByIdQueryHandler _handler;
        private readonly GetSourceByIdQueryValidator _validator;
        private readonly GetSourceByIdQuery _request;

        public GetSourceByIdQueryTest()
        {
            _fakeSourceRepository = A.Fake<IReadRepository<RF.Domain.Entities.Source>>();
            _handler = new GetSourceByIdQueryHandler(_fakeSourceRepository);
            _validator = new GetSourceByIdQueryValidator();
            _request = new GetSourceByIdQuery();
        }

        [Fact]
        public async void GetSourceByIdHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;
            var fakeResultSource = new RF.Domain.Entities.Source
            {
                Id = fakeInputId,
                Name = "fakeName"
            };

            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<bool>.Ignored
                    ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<SourceDto>(result);
            Assert.Equal(fakeInputId, result.Id);
            Assert.Equal("fakeName", result.Name);
        }

        [Fact]
        public async void GetSourceByIdHandler_SourceDoesntExist_ThrowException()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;

            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((RF.Domain.Entities.Source)null);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(RF.Domain.Entities.Source)}\" ({_request.Id}) was not found.", ex.Message);
        }

        [Fact]
        public async void CreateGetSourceByIdQuery_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void CreateGetSourceByIdQuery_WithIdEqual0_Fail()
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
        public async void CreateGetSourceByIdQuery_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}
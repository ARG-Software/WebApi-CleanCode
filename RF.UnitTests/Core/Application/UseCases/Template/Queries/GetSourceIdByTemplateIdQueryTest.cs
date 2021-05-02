using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Template.Queries.GetSourceIdByTemplateId;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Template.Queries
{
    public class GetSourceIdByTemplateIdQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Template> _fakeTemplateRepository;
        private readonly GetSourceIdByTemplateIdQueryHandler _handler;
        private readonly GetSourceIdByTemplateIdQuery _request;
        private readonly GetSourceIdByTemplateIdQueryValidator _validator;

        public GetSourceIdByTemplateIdQueryTest()
        {
            _fakeTemplateRepository = A.Fake<IReadRepository<RF.Domain.Entities.Template>>();
            _handler = new GetSourceIdByTemplateIdQueryHandler(_fakeTemplateRepository);
            _request = new GetSourceIdByTemplateIdQuery();
            _validator = new GetSourceIdByTemplateIdQueryValidator();
        }

        [Fact]
        public async void GetSourceIdByTemplateIdHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            const int fakeSourceId = 2;
            _request.Id = fakeInputId;

            var fakeResultTemplate = new RF.Domain.Entities.Template()
            {
                Id = fakeInputId,
                Name = "fakeName",
                Source = new RF.Domain.Entities.Source()
                {
                    Name = "FakeSource",
                    Id = fakeSourceId
                }
            };

            A.CallTo(() => _fakeTemplateRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Template, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Template>, IOrderedQueryable<RF.Domain.Entities.Template>>>.Ignored,
                    A<bool>.Ignored, A<Expression<Func<RF.Domain.Entities.Template, object>>[]>.Ignored
                ))
                .Returns(fakeResultTemplate);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(fakeSourceId, result);
        }

        [Fact]
        public async void GetSourceIdByTemplateIdHandler_SourceDoesntExist_ThrowException()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;

            var fakeResultTemplate = new RF.Domain.Entities.Template()
            {
                Id = fakeInputId,
                Name = "fakeName",
                Source = null
            };

            A.CallTo(() => _fakeTemplateRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Template, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Template>, IOrderedQueryable<RF.Domain.Entities.Template>>>.Ignored,
                    A<bool>.Ignored, A<Expression<Func<RF.Domain.Entities.Template, object>>[]>.Ignored
                ))
                .Returns(fakeResultTemplate);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(RF.Domain.Entities.Source)}\" ({_request.Id}) was not found.", ex.Message);
        }

        [Fact]
        public async void GetSourceIdByTemplateIdCommand_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void GetSourceIdByTemplateIdCommand_WithIdEqual0_Fail()
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
        public async void GetSourceIdByTemplateIdCommand_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}
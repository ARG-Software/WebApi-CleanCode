using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Source.Queries.GetSourceWithTemplatesPaged;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Queries
{
    public class GetSourceWithTemplatesPagedQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly GetSourceWithTemplatesPagedQueryHandler _handler;
        private readonly GetSourceWithTemplatesPagedQuery _request;
        private readonly GetSourceWithTemplatesPagedQueryValidator _validator;

        public GetSourceWithTemplatesPagedQueryTest()
        {
            _fakeSourceRepository = A.Fake<IReadRepository<RF.Domain.Entities.Source>>();
            _handler = new GetSourceWithTemplatesPagedQueryHandler(_fakeSourceRepository);
            _request = new GetSourceWithTemplatesPagedQuery();
            _validator = new GetSourceWithTemplatesPagedQueryValidator();
        }

        [Fact]
        public async void GetSourceWithTemplatesPagedHandler_Success()
        {
            //Arrange
            const int templateListSize = 2;
            var fakeTemplates = A.CollectionOfDummy<RF.Domain.Entities.Template>(templateListSize);
            var fakeSource = A.Dummy<RF.Domain.Entities.Source>();
            fakeSource.Templates = fakeTemplates.ToList();

            _request.SourceId = 1;

            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<RF.Domain.Entities.Source, object>>>.Ignored
                ))
                .Returns(fakeSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Templates);
            Assert.True(result.Templates.Any());
            Assert.Equal(result.Templates.Count(), templateListSize);
        }

        [Fact]
        public async void GetSourceWithTemplatesPagedHandler_IfSourceNotFound_ThrowException()
        {
            //Arrange
            _request.SourceId = 1;

            A.CallTo(() => _fakeSourceRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<RF.Domain.Entities.Source, object>>>.Ignored
                ))
                .Returns((RF.Domain.Entities.Source)null);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(RF.Domain.Entities.Source)}\" ({_request.SourceId}) was not found.", ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async void CreateGetSourceWithTemplatesPagedCommand_WithNoValidId_Fail(int? idValue)
        {
            //Arrange
            _request.SourceId = idValue.GetValueOrDefault();

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        [Fact]
        public async void CreateGetSourceWithTemplatesPagedCommand_Success()
        {
            //Arrange
            _request.SourceId = 1;
            _request.Options = new PagingOptionsDto();

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.Application.Core.UseCases.Source.Queries.GetSourcePaged;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Source.Queries
{
    public class GetSourcePagedQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Source> _fakeSourceRepository;
        private readonly GetSourcePagedQueryHandler _handler;
        private readonly GetSourcePagedQuery _request;
        private readonly Fixture _fixture;

        public GetSourcePagedQueryTest()
        {
            _fakeSourceRepository = A.Fake<IReadRepository<RF.Domain.Entities.Source>>();
            _handler = new GetSourcePagedQueryHandler(_fakeSourceRepository);
            _request = new GetSourcePagedQuery();
            _fixture = new Fixture();
            _request.Options = _fixture.Create<PagingOptionsDto>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void GetSourcePagedHandler_IfPagingOptionsPassed_Success(bool dtoIsNull)
        {
            //Arrange
            PagingOptionsDto fakePagingOptionsDto = null;
            var fakePagedSetSize = 10;
            const int fakeTableSize = 30;

            if (!dtoIsNull)
            {
                fakePagingOptionsDto = new PagingOptionsDto
                {
                    CurrentIndex = 0,
                    HowManyPerPage = fakePagedSetSize
                };
            }
            else
            {
                fakePagedSetSize = 10;
            }

            _request.Options = fakePagingOptionsDto;

            var fakeResultList = A.CollectionOfDummy<SourceDto>(fakePagedSetSize).ToList();

            A.CallTo(() => _fakeSourceRepository.GetListAsync(A<Expression<Func<RF.Domain.Entities.Source, SourceDto>>>.Ignored,
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<int>.Ignored, A<int>.Ignored, A<bool>.Ignored
                    ))
                .Returns(fakeResultList);
            A.CallTo(() => _fakeSourceRepository.CountTableSize(A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored))
                 .Returns(fakeTableSize);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Total, fakeTableSize);
            Assert.Equal(result.Result, fakeResultList);
            Assert.Equal(result.Result.Count(), fakePagedSetSize);
            Assert.IsAssignableFrom<IEnumerable<SourceDto>>(result.Result);
        }

        [Fact]
        public async void GetSourcePageHandler_IfNoSourcesFound_ReturnsEmptyPagedSet()
        {
            //Arrange
            A.CallTo(() => _fakeSourceRepository.GetListAsync(A<Expression<Func<RF.Domain.Entities.Source, SourceDto>>>.Ignored,
                    A<Expression<Func<RF.Domain.Entities.Source, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Source>, IOrderedQueryable<RF.Domain.Entities.Source>>>.Ignored,
                    A<int>.Ignored, A<int>.Ignored, A<bool>.Ignored
                ))
                .Returns(new List<SourceDto>());

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Total);
            Assert.Empty(result.Result);
            Assert.IsAssignableFrom<IEnumerable<SourceDto>>(result.Result);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Publisher.Queries.GetPublisherIdByAlias;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Publisher.Queries
{
    public class GetPublisherIdByAliasQueryTest
    {
        private readonly IReadRepository<PublisherAlias> _fakePublisherAliasRepository;
        private readonly GetPublisherIdByAliasQueryHandler _handler;
        private readonly GetPublisherIdByAliasQuery _request;

        public GetPublisherIdByAliasQueryTest()
        {
            _fakePublisherAliasRepository = A.Fake<IReadRepository<PublisherAlias>>();
            _handler = new GetPublisherIdByAliasQueryHandler(_fakePublisherAliasRepository);
            _request = new GetPublisherIdByAliasQuery();
        }

        [Fact]
        public async void GetPublisherIdByAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.PublisherAlias = "fakePublisherAlias";
            var fakeResultSource = new PublisherAlias
            {
                PublisherId = fakeInputId,
            };

            A.CallTo(() => _fakePublisherAliasRepository.SingleAsync(
                    A<Expression<Func<PublisherAlias, bool>>>.Ignored,
                    A<Func<IQueryable<PublisherAlias>, IOrderedQueryable<PublisherAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeInputId, result);
        }

        [Fact]
        public async void GetPublisherIdByAliasHandler_IfNotFound_ReturnsNull()
        {
            //Arrange
            _request.PublisherAlias = "fakePublisherAlias";

            A.CallTo(() => _fakePublisherAliasRepository.SingleAsync(
                    A<Expression<Func<PublisherAlias, bool>>>.Ignored,
                    A<Func<IQueryable<PublisherAlias>, IOrderedQueryable<PublisherAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((PublisherAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetPublisherIdByAliasHandler_WithEmptyRequest_ReturnNull()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }
    }
}
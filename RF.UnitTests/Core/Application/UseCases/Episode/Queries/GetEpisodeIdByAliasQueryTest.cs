using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Episode.Queries.GetEpisodeIdByAlias;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Episode.Queries
{
    public class GetEpisodeIdByAliasQueryTest
    {
        private readonly IReadRepository<EpisodeAlias> _fakeEpisodeAliasRepository;
        private readonly GetEpisodeIdByAliasQueryHandler _handler;
        private readonly GetEpisodeIdByAliasQuery _request;

        public GetEpisodeIdByAliasQueryTest()
        {
            _fakeEpisodeAliasRepository = A.Fake<IReadRepository<EpisodeAlias>>();
            _handler = new GetEpisodeIdByAliasQueryHandler(_fakeEpisodeAliasRepository);
            _request = new GetEpisodeIdByAliasQuery();
        }

        [Fact]
        public async void GetEpisodeIdByAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.EpisodeAlias = "fakeEpisodeAlias";
            var fakeResultSource = new EpisodeAlias
            {
                EpisodeId = fakeInputId,
            };

            A.CallTo(() => _fakeEpisodeAliasRepository.SingleAsync(
                    A<Expression<Func<EpisodeAlias, bool>>>.Ignored,
                    A<Func<IQueryable<EpisodeAlias>, IOrderedQueryable<EpisodeAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeInputId, result);
        }

        [Fact]
        public async void GetEpisodeIdByAliasHandler_IfNotFound_ReturnsNull()
        {
            //Arrange
            _request.EpisodeAlias = "fakeEpisodeAlias";

            A.CallTo(() => _fakeEpisodeAliasRepository.SingleAsync(
                    A<Expression<Func<EpisodeAlias, bool>>>.Ignored,
                    A<Func<IQueryable<EpisodeAlias>, IOrderedQueryable<EpisodeAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((EpisodeAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetEpisodeIdByAliasHandler_WithEmptyRequest_ReturnNull()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }
    }
}
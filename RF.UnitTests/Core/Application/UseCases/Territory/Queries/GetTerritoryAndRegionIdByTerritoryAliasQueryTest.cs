using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Territory.Queries.GetTerritoryAndRegionIdByAlias;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Territory.Queries
{
    public class GetTerritoryAndRegionIdByTerritoryAliasQueryTest
    {
        private readonly IReadRepository<TerritoryAlias> _fakeTerritoryAliasRepository;
        private readonly GetTerritoryAndRegionIdByTerritoryAliasQueryHandler _handler;
        private readonly GetTerritoryAndRegionIdByTerritoryAliasQuery _request;

        public GetTerritoryAndRegionIdByTerritoryAliasQueryTest()
        {
            _fakeTerritoryAliasRepository = A.Fake<IReadRepository<TerritoryAlias>>();
            _handler = new GetTerritoryAndRegionIdByTerritoryAliasQueryHandler(_fakeTerritoryAliasRepository);
            _request = new GetTerritoryAndRegionIdByTerritoryAliasQuery();
        }

        [Fact]
        public async void GetTerritoryAndRegionIdByTerritoryAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.TerritoryAlias = "fakeTerritory";

            var fakeTerritory = new RF.Domain.Entities.Territory
            {
                RegionId = fakeInputId
            };

            var fakeResultSource = new TerritoryAlias
            {
                TerritoryId = fakeInputId,
                Territory = fakeTerritory
            };

            A.CallTo(() => _fakeTerritoryAliasRepository.SingleAsync(
                    A<Expression<Func<TerritoryAlias, bool>>>.Ignored,
                    A<Func<IQueryable<TerritoryAlias>, IOrderedQueryable<TerritoryAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<TerritoryAlias, object>>[]>.Ignored
                ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<TerritoryIdAndRegionIdDto>(result);
            Assert.Equal(fakeInputId, result.TerritoryId);
            Assert.Equal(fakeInputId, result.RegionId);
        }

        [Fact]
        public async void GetTerritoryAndRegionIdByTerritoryAliasHandler_IfNoAliasFound_ReturnDtoWithNullValues()
        {
            //Arrange
            const string fakeTerritory = "fakeTerritory";
            _request.TerritoryAlias = fakeTerritory;

            A.CallTo(() => _fakeTerritoryAliasRepository.SingleAsync(
                    A<Expression<Func<TerritoryAlias, bool>>>.Ignored,
                    A<Func<IQueryable<TerritoryAlias>, IOrderedQueryable<TerritoryAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<TerritoryAlias, object>>[]>.Ignored
                ))
                .Returns((TerritoryAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.TerritoryId);
            Assert.Null(result.RegionId);
        }

        [Fact]
        public async void GetTerritoryAndRegionIdByTerritoryAliasHandler_WithEmptyRequest_ReturnDtoWithNullValues()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.TerritoryId);
            Assert.Null(result.RegionId);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.ProductionTitle.Queries.GetProductionTitleAndTypeIdByAlias;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.ProductionTitle.Queries
{
    public class GetProductionTitleAndTypeIdByAliasQueryTest
    {
        private readonly IReadRepository<ProductionTitleAlias> _fakeProductionTitleAliasRepository;
        private readonly GetProductionTitleAndTypeIdByAliasQueryHandler _handler;
        private readonly GetProductionTitleAndTypeIdByAliasQuery _request;
        private const string FakeProductionTitleAlias = "fakeProductionTitle";

        public GetProductionTitleAndTypeIdByAliasQueryTest()
        {
            _fakeProductionTitleAliasRepository = A.Fake<IReadRepository<ProductionTitleAlias>>();
            _handler = new GetProductionTitleAndTypeIdByAliasQueryHandler(_fakeProductionTitleAliasRepository);
            _request = new GetProductionTitleAndTypeIdByAliasQuery();
        }

        [Fact]
        public async void GetProductionTitleAndTypeIdByAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.ProductionTitleAlias = FakeProductionTitleAlias;

            var fakeResult = new ProductionTitleAlias
            {
                ProductionTitleId = fakeInputId,
                ProductionTitle = new RF.Domain.Entities.ProductionTitle()
                {
                    ProductionTypeId = fakeInputId
                }
            };

            A.CallTo(() => _fakeProductionTitleAliasRepository.SingleAsync(
                    A<Expression<Func<ProductionTitleAlias, bool>>>.Ignored,
                    A<Func<IQueryable<ProductionTitleAlias>, IOrderedQueryable<ProductionTitleAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<ProductionTitleAlias, object>>[]>.Ignored
                ))
                .Returns(fakeResult);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<ProductionTitleIdAndProductionTitleTypeIdDto>(result);
            Assert.Equal(fakeInputId, result.ProductionTitleId);
        }

        [Fact]
        public async void GetProductionTitleAndTypeIdByAliasHandler_IfNotFound_ReturnsDtoWithNullValues()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.ProductionTitleAlias = FakeProductionTitleAlias;

            A.CallTo(() => _fakeProductionTitleAliasRepository.SingleAsync(
                    A<Expression<Func<ProductionTitleAlias, bool>>>.Ignored,
                    A<Func<IQueryable<ProductionTitleAlias>, IOrderedQueryable<ProductionTitleAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<ProductionTitleAlias, object>>[]>.Ignored
                ))
                .Returns((ProductionTitleAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.ProductionTitleTypeId);
            Assert.Null(result.ProductionTitleId);
        }

        [Fact]
        public async void GetProductionTitleAndTypeIdByAliasHandler_WithEmptyRequest_ReturnNull()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.ProductionTitleTypeId);
            Assert.Null(result.ProductionTitleId);
        }
    }
}
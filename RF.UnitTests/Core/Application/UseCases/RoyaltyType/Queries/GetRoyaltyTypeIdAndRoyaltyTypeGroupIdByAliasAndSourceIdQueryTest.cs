using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.RoyaltyType.Queries.GetRoyaltyTypeIdAndRoyaltyTypeGroupIdByAliasAndSourceId;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.RoyaltyType.Queries
{
    public class GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryTest
    {
        private readonly IReadRepository<RoyaltyTypeAlias> _fakeRoyaltyTypeAliasRepository;
        private readonly GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryHandler _handler;
        private readonly GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery _request;

        public GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryTest()
        {
            _fakeRoyaltyTypeAliasRepository = A.Fake<IReadRepository<RoyaltyTypeAlias>>();
            _handler = new GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQueryHandler(_fakeRoyaltyTypeAliasRepository);
            _request = new GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery();
        }

        [Fact]
        public async void GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.RoyaltyTypeAlias = "fakeRoyaltyType";
            var fakeResultSource = new RoyaltyTypeAlias
            {
                RoyaltyTypeId = fakeInputId,
            };

            A.CallTo(() => _fakeRoyaltyTypeAliasRepository.SingleAsync(
                    A<Expression<Func<RoyaltyTypeAlias, bool>>>.Ignored,
                    A<Func<IQueryable<RoyaltyTypeAlias>, IOrderedQueryable<RoyaltyTypeAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<RoyaltyTypeAlias, object>>[]>.Ignored
                ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<RoyaltyTypeIdAndRoyaltyTypeGroupIdDto>(result);
            Assert.Equal(fakeInputId, result.RoyaltyTypeId);
        }

        [Fact]
        public async void GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdHandler_IfRoyaltyTypeNotFound_ReturnsDtoWithNullValues()
        {
            //Arrange
            _request.RoyaltyTypeAlias = "fakeRoyaltyType";

            A.CallTo(() => _fakeRoyaltyTypeAliasRepository.SingleAsync(
                    A<Expression<Func<RoyaltyTypeAlias, bool>>>.Ignored,
                    A<Func<IQueryable<RoyaltyTypeAlias>, IOrderedQueryable<RoyaltyTypeAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<RoyaltyTypeAlias, object>>[]>.Ignored
                ))
                .Returns((RoyaltyTypeAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<RoyaltyTypeIdAndRoyaltyTypeGroupIdDto>(result);
            Assert.Null(result.RoyaltyTypeId);
            Assert.Null(result.RoyaltyTypeGroupId);
        }

        [Fact]
        public async void GetRoyaltyTypeIdAndRoyaltyTypeGroupIdByAliasAndSourceIdHandler_WithEmptyRequest_ReturnsDtoWithNullValues()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.RoyaltyTypeId);
            Assert.Null(result.RoyaltyTypeGroupId);
        }
    }
}
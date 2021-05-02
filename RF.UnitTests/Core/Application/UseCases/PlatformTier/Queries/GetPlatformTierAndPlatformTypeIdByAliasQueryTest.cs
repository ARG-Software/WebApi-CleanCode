using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.Application.Core.UseCases.PlatformTier.Queries.GetPlatformTierAndPlatformTypeIdByAlias;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.PlatformTier.Queries
{
    public class GetPlatformTierAndPlatformTypeIdByAliasQueryTest
    {
        private readonly IReadRepository<PlatformTierAlias> _fakePlatformTierAliasRepository;
        private readonly GetPlatformTierAndPlatformTypeIdByAliasQueryHandler _handler;
        private GetPlatformTierAndPlatformTypeIdByAliasQuery _request;
        private readonly Fixture _fixture;

        public GetPlatformTierAndPlatformTypeIdByAliasQueryTest()
        {
            _fakePlatformTierAliasRepository = A.Fake<IReadRepository<PlatformTierAlias>>();
            _handler = new GetPlatformTierAndPlatformTypeIdByAliasQueryHandler(_fakePlatformTierAliasRepository);
            _request = new GetPlatformTierAndPlatformTypeIdByAliasQuery();
            _fixture = new Fixture();
        }

        [Fact]
        public async void GetPlatformTierAndPlatformTypeIdByAliasHandler_Success()
        {
            //Arrange
            const int fakePlatformTierId = 1;
            const int fakePlatformTypeId = 2;

            _request = _fixture.Create<GetPlatformTierAndPlatformTypeIdByAliasQuery>();

            var fakeResultSource = new PlatformTierAlias
            {
                PlatformTierId = fakePlatformTierId,
                PlatformTier = new RF.Domain.Entities.PlatformTier()
                {
                    Platform = new Platform()
                    {
                        PlatformTypeId = fakePlatformTypeId
                    }
                }
            };

            A.CallTo(() => _fakePlatformTierAliasRepository.SingleAsync(
                    A<Expression<Func<PlatformTierAlias, bool>>>.Ignored,
                    A<Func<IQueryable<PlatformTierAlias>, IOrderedQueryable<PlatformTierAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<PlatformTierAlias, object>>[]>.Ignored
                ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<PlatformTierIdAndPlatformTypeIdDto>(result);
            Assert.Equal(fakePlatformTierId, result.PlatformTierId);
            Assert.Equal(fakePlatformTypeId, result.PlatformTypeId);
        }

        [Fact]
        public async void GetPlatformTierAndPlatformTypeIdByAliasHandler_IfNotFound_ReturnsDtoWithNullValues()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.PlatformTierAlias = fakeInputId.ToString();
            var fakeResultSource = new PlatformTierAlias
            {
                PlatformTierId = fakeInputId,
            };

            A.CallTo(() => _fakePlatformTierAliasRepository.SingleAsync(
                    A<Expression<Func<PlatformTierAlias, bool>>>.Ignored,
                    A<Func<IQueryable<PlatformTierAlias>, IOrderedQueryable<PlatformTierAlias>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<PlatformTierAlias, object>>[]>.Ignored
                ))
                .Returns((PlatformTierAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.PlatformTypeId);
            Assert.Null(result.PlatformTierId);
        }

        [Fact]
        public async void GetPlatformTierAndPlatformTypeIdByAliasHandler_WithEmptyRequest_ReturnDtoWithNullValues()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.PlatformTypeId);
            Assert.Null(result.PlatformTierId);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Society.Queries.GetSocietyIdByAlias;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Society.Queries
{
    public class GetSocietyIdByAliasQueryTest
    {
        private readonly IReadRepository<SocietyAlias> _fakeSocietyAliasRepository;
        private readonly GetSocietyIdByAliasQueryHandler _handler;
        private readonly GetSocietyIdByAliasQuery _request;

        public GetSocietyIdByAliasQueryTest()
        {
            _fakeSocietyAliasRepository = A.Fake<IReadRepository<SocietyAlias>>();
            _handler = new GetSocietyIdByAliasQueryHandler(_fakeSocietyAliasRepository);
            _request = new GetSocietyIdByAliasQuery();
        }

        [Fact]
        public async void GetSocietyIdByAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.SocietyAlias = "fakeSociety";
            var fakeResultSource = new SocietyAlias
            {
                SocietyId = fakeInputId,
            };

            A.CallTo(() => _fakeSocietyAliasRepository.SingleAsync(
                    A<Expression<Func<SocietyAlias, bool>>>.Ignored,
                    A<Func<IQueryable<SocietyAlias>, IOrderedQueryable<SocietyAlias>>>.Ignored,
                    A<bool>.Ignored
                    ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeInputId, result);
        }

        [Fact]
        public async void GetSocietyIdByAliasHandler_IfNotFound_ReturnsNull()
        {
            //Arrange
            _request.SocietyAlias = "fakeSociety";

            A.CallTo(() => _fakeSocietyAliasRepository.SingleAsync(
                    A<Expression<Func<SocietyAlias, bool>>>.Ignored,
                    A<Func<IQueryable<SocietyAlias>, IOrderedQueryable<SocietyAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((SocietyAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetSocietyIdByAliasHandler_WithEmptyRequest_ReturnNull()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }
    }
}
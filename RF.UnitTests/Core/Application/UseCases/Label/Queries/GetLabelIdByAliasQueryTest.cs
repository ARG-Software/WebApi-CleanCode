using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Label.Queries.GetLabelIdByAlias;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Label.Queries
{
    public class GetLabelIdByAliasQueryTest
    {
        private readonly IReadRepository<LabelAlias> _fakeLabelAliasRepository;
        private readonly GetLabelIdByAliasQueryHandler _handler;
        private readonly GetLabelIdByAliasQuery _request;

        public GetLabelIdByAliasQueryTest()
        {
            _fakeLabelAliasRepository = A.Fake<IReadRepository<LabelAlias>>();
            _handler = new GetLabelIdByAliasQueryHandler(_fakeLabelAliasRepository);
            _request = new GetLabelIdByAliasQuery();
        }

        [Fact]
        public async void GetLabelIdByAliasHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.LabelAlias = "fakeLabelAlias";
            var fakeResultSource = new LabelAlias
            {
                LabelId = fakeInputId
            };

            A.CallTo(() => _fakeLabelAliasRepository.SingleAsync(
                    A<Expression<Func<LabelAlias, bool>>>.Ignored,
                    A<Func<IQueryable<LabelAlias>, IOrderedQueryable<LabelAlias>>>.Ignored,
                    A<bool>.Ignored
                    ))
                .Returns(fakeResultSource);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeInputId, result);
        }

        [Fact]
        public async void GetLabelIdByAliasHandler_IfNotFound_ReturnsNull()
        {
            //Arrange
            _request.LabelAlias = "fakeLabelAlias";

            A.CallTo(() => _fakeLabelAliasRepository.SingleAsync(
                    A<Expression<Func<LabelAlias, bool>>>.Ignored,
                    A<Func<IQueryable<LabelAlias>, IOrderedQueryable<LabelAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((LabelAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetLabelIdByAliasHandler_WithEmptyRequest_ReturnNull()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }
    }
}
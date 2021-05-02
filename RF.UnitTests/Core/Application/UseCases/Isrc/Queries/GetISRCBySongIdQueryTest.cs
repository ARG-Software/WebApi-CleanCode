using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Isrc.Queries.GetISRCBySongId;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Isrc.Queries
{
    public class GetIsrcBySongIdQueryTest
    {
        private readonly IReadRepository<ISRC> _fakeIsrcRepository;
        private readonly GetIsrcBySongIdQueryHandler _handler;
        private readonly GetIsrcBySongIdQuery _request;

        public GetIsrcBySongIdQueryTest()
        {
            _fakeIsrcRepository = A.Fake<IReadRepository<ISRC>>();
            _handler = new GetIsrcBySongIdQueryHandler(_fakeIsrcRepository);
            _request = new GetIsrcBySongIdQuery();
        }

        [Fact]
        public async void GetIsrcBySongIdQueryHandler_Success()
        {
            //Arrange
            const int fakeIsrcId = 1;
            _request.SongId = 2;
            var fakeIsrcResult = new ISRC
            {
                Id = fakeIsrcId
            };

            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeIsrcResult);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeIsrcId, result);
        }

        [Fact]
        public async void GetIsrcBySongIdQuery_IfNotFound_ReturnsNull()
        {
            //Arrange
            _request.SongId = 2;

            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((ISRC)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Null(result);
        }
    }
}
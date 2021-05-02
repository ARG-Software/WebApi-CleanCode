using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdByIsrcOrIswcId;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Song.Queries
{
    public class GetSongIdByIsrcOrIswcIdQueryTest
    {
        private readonly IReadRepository<ISRC> _fakeIsrcRepository;
        private readonly IReadRepository<ISWC> _fakeIswcRepository;
        private readonly GetSongIdByIsrcOrIswcIdQueryHandler _handler;
        private readonly GetSongIdByIsrcOrIswcIdQuery _request;

        public GetSongIdByIsrcOrIswcIdQueryTest()
        {
            _fakeIsrcRepository = A.Fake<IReadRepository<ISRC>>();
            _fakeIswcRepository = A.Fake<IReadRepository<ISWC>>();
            _handler = new GetSongIdByIsrcOrIswcIdQueryHandler(_fakeIswcRepository, _fakeIsrcRepository);
            _request = new GetSongIdByIsrcOrIswcIdQuery();
        }

        [Fact]
        public async void GetSongIdByIsrcOrIswcIdQuery_IfIsrcHasValueAndHasSongIdOnDatabaseForThatIsrcId_ReturnSongIdForIsrc()
        {
            //Arrange
            const int fakeIsrcId = 1;
            const int fakeSongId = 1;
            _request.IsrcId = fakeIsrcId;
            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(new ISRC
                {
                    SongId = fakeSongId
                });
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(result, fakeSongId);
        }

        [Fact]
        public async void GetSongIdByIsrcOrIswcIdQuery_IfIsrcHasValueAndHasNoSongIdOnDatabaseForThatIsrcId_Returns0()
        {
            //Arrange
            const int fakeIsrcId = 1;
            _request.IsrcId = fakeIsrcId;
            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                 .Returns((ISRC)null);
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetSongIdByIsrcOrIswcIdQuery_IfIsrcHasValueAndHasNoSongIdOnDatabaseForThatIsrcIdAndAIswcIsPassedAndHasMatchInDatabaseForASongId_ReturnsSongIdForIswc()
        {
            //Arrange
            const int fakeIsrcId = 1;
            const int fakeIswcId = 1;
            const int fakeSongIdForIswc = 1;
            _request.IsrcId = fakeIsrcId;
            _request.IswcId = fakeIswcId;

            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                 .Returns((ISRC)null);
            A.CallTo(() => _fakeIswcRepository.SingleAsync(
                    A<Expression<Func<ISWC, bool>>>.Ignored,
                    A<Func<IQueryable<ISWC>, IOrderedQueryable<ISWC>>>.Ignored,
                    A<bool>.Ignored))
                 .Returns(new ISWC()
                 {
                     SongId = fakeSongIdForIswc
                 });

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeSongIdForIswc, result);
        }

        [Fact]
        public async void GetSongIdByIsrcOrIswcIdQuery_IfIsrcHasNoValueAndIswcHasValueAndHasMatchOnDatabaseForASongId_ReturnsSongIdForIswc()
        {
            //Arrange
            const int fakeIswcId = 1;
            const int fakeSongIdForIswc = 1;
            _request.IswcId = fakeIswcId;

            A.CallTo(() => _fakeIswcRepository.SingleAsync(
                    A<Expression<Func<ISWC, bool>>>.Ignored,
                    A<Func<IQueryable<ISWC>, IOrderedQueryable<ISWC>>>.Ignored,
                    A<bool>.Ignored))
               .Returns(new ISWC()
               {
                   SongId = fakeSongIdForIswc
               });

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeSongIdForIswc, result);
        }

        [Fact]
        public async void FindSongIdByAlternativeWay_IfIsrcHasValueAndIswcHasValueAndBothDontHaveMatchOnDatabaseForASongId_Returns0AndCallsContextServiceToAddParsingError()
        {
            //Arrange
            const int fakeIswcId = 1;
            const int fakeIsrcId = 1;
            _request.IsrcId = fakeIsrcId;
            _request.IswcId = fakeIswcId;
            A.CallTo(() => _fakeIsrcRepository.SingleAsync(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns((ISRC)null);
            A.CallTo(() => _fakeIswcRepository.SingleAsync(
                    A<Expression<Func<ISWC, bool>>>.Ignored,
                    A<Func<IQueryable<ISWC>, IOrderedQueryable<ISWC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns((ISWC)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(0, result);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdBySourceSongCode;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Song.Queries
{
    public class GetSongIdBySourceSongCodeQueryTest
    {
        private readonly IReadRepository<SourceSongCode> _fakeSongCodeRepository;
        private readonly IReadRepository<SongAlias> _fakeSongAliasRepository;
        private readonly GetSongIdBySourceSongCodeOrAliasQueryHandler _handler;
        private readonly GetSongIdBySourceSongCodeOrAliasQuery _request;
        private const string FakeSourceSongCode = "fakesourcesongCode";
        private const string FakeSongAlias = "fakeSongAlias";

        public GetSongIdBySourceSongCodeQueryTest()
        {
            _fakeSongCodeRepository = A.Fake<IReadRepository<SourceSongCode>>();
            _fakeSongAliasRepository = A.Fake<IReadRepository<SongAlias>>();
            _handler = new GetSongIdBySourceSongCodeOrAliasQueryHandler(_fakeSongCodeRepository, _fakeSongAliasRepository);
            _request = new GetSongIdBySourceSongCodeOrAliasQuery();
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_IfSourceSongCodeIsPassedAndItsFound_ReturnsSongId()
        {
            //Arrange
            const int fakeSongId = 1;
            _request.SourceSongCode = FakeSourceSongCode;
            _request.Song = FakeSongAlias;

            var fakeResult = new SourceSongCode()
            {
                SongId = fakeSongId
            };

            A.CallTo(() => _fakeSongCodeRepository.SingleAsync(
                    A<Expression<Func<SourceSongCode, bool>>>.Ignored,
                    A<Func<IQueryable<SourceSongCode>, IOrderedQueryable<SourceSongCode>>>.Ignored,
                    A<bool>.Ignored
                    ))
                .Returns(fakeResult);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeSongId, result);
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_IfSourceSongCodeIsNotPassedAndSongAliasIsPassedAndItsFound_ReturnsSongId()
        {
            //Arrange
            const int fakeSongId = 1;
            _request.SourceSongCode = "";
            _request.Song = FakeSongAlias;

            var fakeResult = new SongAlias()
            {
                SongId = fakeSongId
            };

            A.CallTo(() => _fakeSongAliasRepository.SingleAsync(
                    A<Expression<Func<SongAlias, bool>>>.Ignored,
                    A<Func<IQueryable<SongAlias>, IOrderedQueryable<SongAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeResult);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeSongId, result);
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_IfSourceSongCodeIsNotPassedAndSongAliasNotPassed_Returns0()
        {
            //Arrange
            _request.SourceSongCode = "";
            _request.Song = "";

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_IfSourceSongCodeNotFoundTryToGoByAlias_ReturnsSongId()
        {
            //Arrange
            const int fakeSongId = 1;
            _request.SourceSongCode = FakeSourceSongCode;
            _request.Song = FakeSongAlias;

            var fakeResult = new SongAlias()
            {
                SongId = fakeSongId
            };

            A.CallTo(() => _fakeSongCodeRepository.SingleAsync(
                    A<Expression<Func<SourceSongCode, bool>>>.Ignored,
                    A<Func<IQueryable<SourceSongCode>, IOrderedQueryable<SourceSongCode>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((SourceSongCode)null);

            A.CallTo(() => _fakeSongAliasRepository.SingleAsync(
                    A<Expression<Func<SongAlias, bool>>>.Ignored,
                    A<Func<IQueryable<SongAlias>, IOrderedQueryable<SongAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeResult);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(fakeSongId, result);
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_IfSourceSongCodeNotFoundAndAliasNotFound_Returns0()
        {
            //Arrange
            _request.SourceSongCode = FakeSourceSongCode;
            _request.Song = FakeSongAlias;

            A.CallTo(() => _fakeSongCodeRepository.SingleAsync(
                    A<Expression<Func<SourceSongCode, bool>>>.Ignored,
                    A<Func<IQueryable<SourceSongCode>, IOrderedQueryable<SourceSongCode>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((SourceSongCode)null);

            A.CallTo(() => _fakeSongAliasRepository.SingleAsync(
                    A<Expression<Func<SongAlias, bool>>>.Ignored,
                    A<Func<IQueryable<SongAlias>, IOrderedQueryable<SongAlias>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((SongAlias)null);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetSongIdBySourceSongCodeHandler_WithEmptyRequest_Returns0()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(0, result);
        }
    }
}
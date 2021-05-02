using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using FakeItEasy;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Services.ETL;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;
using RF.UnitTests.Services.Fixtures;
using Xunit;

namespace RF.UnitTests.Services.ETL
{
    public class StatementServiceTest : CommonConfigurationAndMethodsForServicesTest
    {
        private readonly StatementService _service;
        private readonly IETLContextService _contextService;

        public StatementServiceTest()
        {
            _contextService = A.Fake<IETLContextService>();
            _service = A.Fake<StatementService>((options) => options.CallsBaseMethods());

            _service.StatementDetailRepository = FakeStatementDetailRepository;
            _service.StatementHeaderRepository = FakeStatementHeaderRepository;
            _service.IsrcRepository = FakeIsrcRepository;
            _service.IswcRepository = FakeIswcRepository;
            _service.SongAliasRepository = FakeSongAliasRepository;
            _service.SourceSongCodeRepository = FakeSongSourceCode;
            _service.ContextService = _contextService;
        }

        [Fact]
        public void InsertStatementHeader_IfEntityIsCorrectlyInserted_ReturnsTrue()

        {
            //Arrange
            var mockStatementHeader = Fixture.Create<StatementHeader>();

            //Act
            var result = _service.InsertStatementHeader(mockStatementHeader);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void InsertStatementHeader_IfWhenInsertingEntityAnExceptionIsThrown_ReturnsFalseAndAddsErrorToList()
        {
            //Arrange
            var mockStatementHeader = Fixture.Create<StatementHeader>();
            A.CallTo(() => FakeStatementHeaderRepository.InsertRFEntity(mockStatementHeader))
                .Throws<Exception>();

            //Act && assert
            var ex = Assert.Throws<ETLException>(() => _service.InsertStatementHeader(mockStatementHeader));
            Assert.Equal("Couldn't insert statement header.", ex.Message);
        }

        [Fact]
        public void InsertStatementDetails_IfEntitiesAreCorrectlyInserted_UpdatesStatementDetailListWithStatementHeaderIdAndReturnsTrue()

        {
            //Arrange
            const int mockedStatementHeaderId = 12;
            var mockedStatementHeader = Fixture.Build<StatementHeader>().With(x => x.Id, mockedStatementHeaderId).Create();
            var mockedStatementDetailsToInsert = Fixture.Build<StatementDetail>().With(x => x.StatementHeaderId, 20).CreateMany(2).ToList();

            A.CallTo(() => _contextService.GetStatementDetailsToBeInserted())
                .Returns(mockedStatementDetailsToInsert);

            //Act
            var result = _service.InsertStatementDetails(mockedStatementHeader);

            //Assert
            Assert.True(result);

            A.CallTo(() => FakeStatementDetailRepository
                    .InsertRFEntity(A<List<StatementDetail>>.That.Matches(x => x.TrueForAll(s => s.StatementHeaderId == mockedStatementHeaderId))))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void InsertStatementDetails_IfWhenInsertingEntitiesAnExceptionIsThrown_ThrowException()
        {
            //Arrange
            A.CallTo(() => FakeStatementDetailRepository.InsertRFEntity(A<IEnumerable<StatementDetail>>.Ignored))
                .Throws<Exception>();
            var mockedStatementHeader = Fixture.Create<StatementHeader>();

            //Act && assert
            var ex = Assert.Throws<ETLException>(() => _service.InsertStatementDetails(mockedStatementHeader));
            Assert.Equal("Couldn't insert statements details.", ex.Message);
        }

        [Fact]
        public void CreateStatementHeader_ReturnsStatementHeaderCreated()
        {
            //Arrange
            const int mockedPaymentId = 1;
            const int mockedTemplateId = 2;
            const double royaltyNetLocal = 22.34;
            const double royaltyNetForeign = 23.34;
            const int listSize = 2;

            A.CallTo(() => _contextService.GetStatementDetailsToBeInserted())
                .Returns(Fixture.Build<StatementDetail>()
                    .With(x => x.RoyaltyNetUsd, royaltyNetLocal)
                    .With(x => x.RoyaltyNetForeign, royaltyNetForeign)
                    .CreateMany(listSize));

            A.CallTo(() => _contextService.GetPaymentForETLProcessment())
                .Returns(Fixture.Build<PaymentReceived>().With(x => x.Id, mockedPaymentId).Create());

            //Act
            var result = _service.CreateStatementHeader(mockedTemplateId);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<StatementHeader>(result);
            Assert.Equal(mockedPaymentId, result.PaymentReceivedId);
            Assert.Equal(royaltyNetLocal * listSize, result.TotalLocal);
            Assert.Equal(royaltyNetForeign * listSize, result.TotalForeign);
            Assert.Equal(mockedTemplateId, result.TemplateId);
        }

        [Fact]
        public void FindCode_IfCodeIsNullAndSongId0_ReturnNull()
        {
            //Act
            var result = _service.FindCode<ISRC>(null, 0);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindCode_IfCodeIsNotNullAndHasMatchInDatabase_ReturnCodeId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockCodeId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(Fixture.Build<ISRC>()
                    .With(x => x.Id, mockCodeId)
                    .Create()
                );

            //Act
            var result = _service.FindCode<ISRC>(mockCode, 0);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result.Value);
        }

        [Fact]
        public void FindCode_IfCodeIsNotNullAndHasNoMatchInDatabaseAndSongIdIsNotNullAndHasMatchInDatabaseAndCodeIsEmptyOrNull_UpdateCodeEntityAndReturnCodeId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockCodeId = 1;
            const int mockSongId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .ReturnsNextFromSequence(
                    null,
                    Fixture.Build<ISRC>()
                        .With(x => x.Code, String.Empty)
                        .With(x => x.Id, mockCodeId)
                        .Create());

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.FindCode<ISRC>(mockCode, mockSongId);

            //Assert

            A.CallTo(() => FakeIsrcRepository.UpdateRFEntity(A<ISRC>.Ignored))
                .MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result.Value);
        }

        [Fact]
        public void FindCode_IfCodeIsNotNullAndHasNoMatchInDatabaseAndSongIdIsNotNullAndHasMatchInDatabaseAndCodeIsNotEmptyOrNull_UpdateCodeEntityAndReturnCodeId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockCodeId = 0;
            const int mockSongId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .ReturnsNextFromSequence(
                null,
                    Fixture.Build<ISRC>()
                        .With(x => x.Code, String.Empty)
                        .With(x => x.Id, mockCodeId)
                        .Create());

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.FindCode<ISRC>(mockCode, mockSongId);

            //Assert
            A.CallTo(() => FakeIsrcRepository.UpdateRFEntity(A<ISRC>.Ignored))
                .MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result.Value);
        }

        [Fact]
        public void FindCode_IfCodeIsNotNullAndHasNoMatchInDatabaseAndSongIdIsNotNullAndHasNoMatchInDatabase_CreateCodeEntityAndReturnCodeId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockCodeId = 0;
            const int mockSongId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .ReturnsNextFromSequence(
                null,
                null);

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.FindCode<ISRC>(mockCode, mockSongId);

            //Assert
            A.CallTo(() => FakeIsrcRepository.InsertRFEntity(A<ISRC>.Ignored))
                .MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result.Value);
        }

        [Fact]
        public void FindCode_IfCodeIsNullAndSongIdNotNullAndHasMatchInDatabase_ReturnCodeId()
        {
            //Arrange
            const int mockCodeId = 1;
            const int mockSongId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(Fixture.Build<ISRC>()
                    .With(x => x.Id, mockCodeId)
                    .Create());

            //Act
            var result = _service.FindCode<ISRC>(string.Empty, mockSongId);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result.Value);
        }

        [Fact]
        public void FindCode_IfCodeIsNullAndSongIdNotNullAndHasNoMatchInDatabase_ReturnNull()
        {
            //Arrange

            const int mockSongId = 1;
            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                 .Returns(null);

            //Act
            var result = _service.FindCode<ISRC>(string.Empty, mockSongId);

            //Assert

            Assert.Null(result);
        }

        [Fact]
        public void InsertCodeEntity_IfInsertedIntoDatabase_ReturnEntityId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockSongId = 1;
            const int mockCodeId = 0;

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.InsertCodeEntity<ISRC>(mockCode, mockSongId);

            //Assert
            A.CallTo(() => FakeIsrcRepository.InsertRFEntity(A<ISRC>.Ignored))
                .MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result);
        }

        [Fact]
        public void InsertCodeEntity_IfNotInsertedIntoDatabase_ThrowException()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockSongId = 1;

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(false);

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.InsertCodeEntity<ISRC>(mockCode, mockSongId));

            //Assert
            Assert.Equal("Couldn't Add new Entry for with  code value " + mockCode + " to Database", ex.Message);
        }

        [Fact]
        public void UpdateCodeEntity_IfUpdatedIntoDatabase_ReturnEntityId()
        {
            //Arrange
            const string mockCode = "anyCode";
            const int mockCodeId = 1;
            var mockEntity = Fixture.Build<ISRC>()
                .With(x => x.Id, 1)
                .Create();

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.UpdateCodeEntity(mockCode, mockEntity);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(mockCodeId, result);
        }

        [Fact]
        public void UpdateCodeEntity_IfNotUpdatedIntoDatabase_ThrowException()
        {
            //Arrange
            const string mockCode = "anyCode";
            var mockEntity = Fixture.Build<ISRC>()
                .With(x => x.Id, 1)
                .Create();

            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(false);

            //Act && assert
            var ex = Assert.Throws<ETLException>(() => _service.UpdateCodeEntity(mockCode, mockEntity));
            Assert.Equal("Couldn't update code value " + mockCode + " to Database", ex.Message);
        }
    }
}
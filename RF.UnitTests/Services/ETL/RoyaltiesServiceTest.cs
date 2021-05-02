using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using FakeItEasy;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Filters;
using RF.Domain.Interfaces.Builders;
using RF.Domain.Interfaces.Filters;
using RF.Domain.Interfaces.ValueObjects;
using RF.Services.ETL;
using RF.Services.ETL.Common.Exceptions;
using RF.UnitTests.Services.Fixtures;
using Xunit;

namespace RF.UnitTests.Services.ETL
{
    public class RoyaltiesServiceTest : CommonConfigurationAndMethodsForServicesTest
    {
        private readonly RoyaltiesService _service;
        private readonly List<StatementDetail> _fakeStatementDetailList;

        private readonly IDistributionAgreementFilterBuilder<DistributionAgreementDetail>
            _fakeDistributionAgreementFilterBuilder;

        private readonly IDistributionAgreementRankScore<StatementDetail, DistributionAgreementDetail>
            _fakeDistributionAgreementRankScore;

        public RoyaltiesServiceTest()
        {
            _fakeDistributionAgreementFilterBuilder =
                A.Fake<IDistributionAgreementFilterBuilder<DistributionAgreementDetail>>();
            _fakeDistributionAgreementRankScore =
                A.Fake<IDistributionAgreementRankScore<StatementDetail, DistributionAgreementDetail>>();

            _fakeStatementDetailList = new List<StatementDetail>();

            _service = new RoyaltiesService()
            {
                QueryBuilderForDistributionAgreementDetail = _fakeDistributionAgreementFilterBuilder,
                DistributionAgreementRankScore = _fakeDistributionAgreementRankScore,
                RoyaltyDistributionRepository = FakeRoyaltyDistributionRepository,
                IsrcRepository = FakeIsrcRepository,
                WriterShareRepository = FakeWriterShareRepository,
                DistributionAgreementDetailRepository = FakeDistributionAgreementDetailRepository,
                DistributionAgreementRepository = FakeDistributionAgreementRepository
            };
            this.MockRoyaltyServiceRepositoriesDefaultBehaviour();
            this.MockQueryBuilderDefaultBehaviour();
        }

        [Fact]
        public void InsertRoyaltiesDistributions_IfCouldNotSaveToDatabase_ThrowException()
        {
            //Arrange
            var mockedDistributionsList = Fixture.CreateMany<RoyaltyDistribution>(2);
            A.CallTo(() => FakeRoyaltyDistributionRepository.InsertRFEntity
                    (A<IEnumerable<RoyaltyDistribution>>.Ignored))
                .Throws<Exception>();

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.InsertRoyaltiesDistributions(mockedDistributionsList));
            //Assert
            Assert.Equal("Couldn't insert royalty distributions on database", ex.Message);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_ReturnListWithRoyaltiesCalculatedForEachPublisher()
        {
            //Arrange
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                Id = 1,
                AlbumId = 3,
                LabelId = 2,
                ISRCId = 1,
                RoyaltyNetUsd = 120.45,
                PublisherId = 3,
                TerritoryId = 2,
                RoyaltyTypeId = 4
            });

            //Act
            var result = _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList).ToList();
            var firstListElement = result.First();
            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<RoyaltyDistribution>>(result);
            Assert.Equal(4, result.Count);
            Assert.Equal(30.1125, firstListElement.Amount);
            Assert.Equal(0.5, firstListElement.Rate);
            Assert.Equal(1, firstListElement.StatementDetailId);
            Assert.Equal(6.0225000000000009, result.Last().Amount);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfStatementIsrcIdIsNull_ThrowException()
        {
            //Arrange
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                ISRCId = null,
                RoyaltyNetUsd = 120.45,
                TerritoryId = 3,
                RoyaltyTypeId = 4,
                Id = 1,
                SongId = 2
            });

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal("The statement with the Song Id 2 doesn't have a ISRC associated", ex.Message);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfSSongIdDoesntHaveISRCMatch_ThrowException()
        {
            //Arrange
            var mockedIsrcId = 1;
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                ISRCId = mockedIsrcId,
                RoyaltyNetUsd = 120.45,
                TerritoryId = 3,
                RoyaltyTypeId = 4
            });

            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(null);

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal("The song for the ISRC " + mockedIsrcId + " doesn't exist", ex.Message);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfSSongIdDoesntHaveWriterShareAssociated_ThrowException()
        {
            //Arrange
            const int mockSongId = 1;
            var mockISRC = Fixture.Build<ISRC>().With(x => x.SongId, mockSongId).Create();
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                ISRCId = 1,
                RoyaltyNetUsd = 120.45,
                TerritoryId = 3,
                RoyaltyTypeId = 4
            });

            A.CallTo(() => FakeWriterShareRepository.GetAll(
                    A<Expression<Func<WriterShare, bool>>>.Ignored,
                    A<Func<IQueryable<WriterShare>, IOrderedQueryable<WriterShare>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<WriterShare, object>>[]>.Ignored
                ))
                .Returns(new List<WriterShare>());

            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(mockISRC);

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal(ex.Message, "The song with the Id " + mockSongId + " doesn't have a writer share associated");
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfTotalWriterSharePercentageIsBelow1_ThrowException()
        {
            //Arrange
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                AlbumId = 3,
                LabelId = 2,
                ISRCId = 1,
                RoyaltyNetUsd = 120.45,
                PublisherId = 3,
                TerritoryId = 2,
                RoyaltyTypeId = 4
            });

            A.CallTo(() => FakeIsrcRepository.Single(
                    A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(new ISRC()
                {
                    SongId = 1
                });

            A.CallTo(() => FakeDistributionAgreementDetailRepository.GetAll(
                    A<Expression<Func<DistributionAgreementDetail, bool>>>.Ignored,
                    A<Func<IQueryable<DistributionAgreementDetail>,
                        IOrderedQueryable<DistributionAgreementDetail>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(new List<DistributionAgreementDetail>() {
                    Fixture.Build<DistributionAgreementDetail>()
                        .With(x => x.AgreementGroup, 1)
                        .With(x => x.LabelId, 2)
                        .With(x => x.Share, 0.3)
                        .Create(),
                    Fixture.Build<DistributionAgreementDetail>()
                        .With(x => x.AgreementGroup, 1)
                        .With(x => x.LabelId, 2)
                        .With(x => x.Share, 0.5)
                        .Create()
                        }
                );

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal("The agreement id 1 doesn't have the Shares correct, the sum doesn't match to 100%", ex.Message);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfNoResultsWereFoundForDistributionAgreement_ThrowException()
        {
            //Arrange
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                AlbumId = 3,
                LabelId = 2,
                ISRCId = 1,
                RoyaltyNetUsd = 120.45,
                PublisherId = 3,
                TerritoryId = 2,
                RoyaltyTypeId = 4
            });

            A.CallTo(() => FakeDistributionAgreementDetailRepository.GetAll(
                    A<Expression<Func<DistributionAgreementDetail, bool>>>.Ignored,
                    A<Func<IQueryable<DistributionAgreementDetail>,
                        IOrderedQueryable<DistributionAgreementDetail>>>.Ignored,
                    A<bool>.Ignored))
                .Returns(new List<DistributionAgreementDetail>());

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal("No Agreement Details found for the Agreement Id 1", ex.Message);
        }

        [Fact]
        public void RoyaltiesDistributionCalculation_IfNoFilterFoundForDistributionAgreementId_ThrowException()
        {
            //Arrange
            _fakeStatementDetailList.Add(new StatementDetail()
            {
                AlbumId = 3,
                LabelId = 2,
                ISRCId = 1,
                RoyaltyNetUsd = 120.45,
                PublisherId = 3,
                TerritoryId = 2,
                RoyaltyTypeId = 4
            });
            A.CallTo(() => FakeDistributionAgreementRepository
                .Single(A<Expression<Func<DistributionAgreement, bool>>>.Ignored,
                        A<Func<IQueryable<DistributionAgreement>, IOrderedQueryable<DistributionAgreement>>>.Ignored,
                        A<bool>.Ignored,
                        A<Expression<Func<DistributionAgreement, object>>>.Ignored
                    )
                )
                .Returns(Fixture.Build<DistributionAgreement>().With(x => x.DistributionAgreementFilter,
                        (DistributionAgreementFilter)null)
                      .Create());

            //Act
            var ex = Assert.Throws<ETLException>(() => _service.RoyaltiesDistributionCalculation(_fakeStatementDetailList));

            //Assert
            Assert.Equal("Couldn't find distribution filter for the distribution agreement with the Id 1", ex.Message);
        }

        #region Mocks

        //TODO: transpose this to a fixture

        private void MockRoyaltyServiceRepositoriesDefaultBehaviour()
        {
            A.CallTo(() => FakeIsrcRepository.Single(
                        A<Expression<Func<ISRC, bool>>>.Ignored,
                    A<Func<IQueryable<ISRC>, IOrderedQueryable<ISRC>>>.Ignored,
                    A<bool>.Ignored
                    )
                )
                .Returns(new ISRC()
                {
                    SongId = 1
                });

            A.CallTo(() => FakeWriterShareRepository.
                GetAll(A<Expression<Func<WriterShare, bool>>>.Ignored,
                    A<Func<IQueryable<WriterShare>, IOrderedQueryable<WriterShare>>>.Ignored,
                    A<bool>.Ignored,
                    A<Expression<Func<WriterShare, object>>[]>.Ignored
                    ))
                .Returns(Fixture.Build<WriterShare>()
                    .With(x => x.Share, 0.5)
                    .With(x => x.DistributionAgreementId, 1)
                    .CreateMany(2));

            A.CallTo(() => FakeDistributionAgreementDetailRepository.
                GetAll(A<Expression<Func<DistributionAgreementDetail, bool>>>.Ignored,
                    A<Func<IQueryable<DistributionAgreementDetail>,
                        IOrderedQueryable<DistributionAgreementDetail>>>.Ignored,
                    A<bool>.Ignored))
                .ReturnsNextFromSequence(new List<DistributionAgreementDetail>()
                    {
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 1)
                            .With(x => x.LabelId, 2)
                            .With(x => x.Share, 0.5)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 1)
                            .With(x => x.LabelId, 2)
                            .With(x => x.Share, 0.5)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 2)
                            .With(x => x.Label, (Label) null)
                            .With(x => x.LabelId, (int?) null)
                            .With(x => x.Share, 0.7)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 2)
                            .With(x => x.Label, (Label) null)
                            .With(x => x.LabelId, (int?) null)
                            .With(x => x.Share, 0.3)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                    },
                    new List<DistributionAgreementDetail>()
                    {
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 1)
                            .With(x => x.PublisherId, 3)
                            .With(x => x.TerritoryId, 2)
                            .With(x => x.Share, 0.9)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 1)
                            .With(x => x.PublisherId, 3)
                            .With(x => x.TerritoryId, (int?) null)
                            .With(x => x.Share, 0.1)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 2)
                            .With(x => x.PublisherId, 2)
                            .With(x => x.TerritoryId, 1)
                            .With(x => x.Share, 1)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 3)
                            .With(x => x.PublisherId, 3)
                            .With(x => x.TerritoryId, 1)
                            .With(x => x.Share, 0.8)
                            .With(x=>x.Rate,0.5)
                            .Create(),
                        Fixture.Build<DistributionAgreementDetail>()
                            .With(x => x.AgreementGroup, 3)
                            .With(x => x.PublisherId, (int?) null)
                            .With(x => x.TerritoryId, (int?) null)
                            .With(x => x.Share, 0.2)
                            .With(x=>x.Rate,0.5)
                            .Create()
                    }
                );

            A.CallTo(() =>
                _fakeDistributionAgreementRankScore.CalculateMostRankedFilterGroup(
                    A<IEnumerable<DistributionAgreementDetail>>.Ignored,
                    A<StatementDetail>.Ignored,
                    A<IEnumerable<string>>.Ignored)
            ).Returns(new DistributionAgreementDetail()
            {
                AgreementGroup = 1
            });
        }

        private void MockQueryBuilderDefaultBehaviour()
        {
            ICreteria<DistributionAgreementDetail> mockedCriteria = new Criteria<DistributionAgreementDetail>
            {
                ExpressionCriteria = (s => s.TerritoryId == 1)
            };

            A.CallTo(() => (_fakeDistributionAgreementFilterBuilder.Build()))
                .Returns(mockedCriteria);
        }

        #endregion Mocks
    }
}
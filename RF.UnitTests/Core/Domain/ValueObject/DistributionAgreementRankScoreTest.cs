using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FakeItEasy;
using RF.Domain.Entities;
using RF.Domain.Exceptions;
using RF.Domain.Interfaces.Strategies;
using RF.Domain.Strategies;
using RF.Domain.ValueObjects;
using Xunit;

namespace RF.UnitTests.Core.Domain.ValueObject
{
    public class DistributionAgreementRankScoreTest
    {
        private readonly DistributionAgreementRankScore<StatementDetail, DistributionAgreementDetail> _distributionAgreementRankScore;
        private IEnumerable<DistributionAgreementDetail> _fakeDistributionAgreementDetailList;
        private readonly IList<string> _fakeFilteredProperties;
        private StatementDetail _fakeEntityToMatch;
        private readonly Fixture _fixture;

        public DistributionAgreementRankScoreTest()
        {
            IDistributionAgreementFilterScoreStrategy scoreStrategy = new DistributionAgreementFilterScoreStrategy();
            _distributionAgreementRankScore = new DistributionAgreementRankScore<StatementDetail, DistributionAgreementDetail>(scoreStrategy);
            _fakeFilteredProperties = new List<string>();
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void CalculateMostRankedFilterGroup_WhenOnlyOneFieldMatch_ReturnsDistributionAgreementDetailWithScoreOf1()
        {
            _fakeFilteredProperties.Add("RoyaltyTypeGroupId");

            //Arrange
            _fakeDistributionAgreementDetailList = _fixture
                .Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeGroupId, 1)
                .CreateMany(1);

            _fakeEntityToMatch = _fixture
                .Build<StatementDetail>()
                .With(x => x.RoyaltyTypeGroupId, 1)
                .Create();

            //Act
            var calculateMostRankedFilterGroup = _distributionAgreementRankScore.CalculateMostRankedFilterGroup(
                _fakeDistributionAgreementDetailList,
                _fakeEntityToMatch,
                _fakeFilteredProperties.ToList());

            //Assert
            Assert.Equal(1, calculateMostRankedFilterGroup.RoyaltyTypeGroupId);
        }

        [Theory]
        [InlineData(1, 2, 1, 1, 2)]
        [InlineData(1, null, 1, 1, null)]
        [InlineData(null, null, 1, null, null)]
        [InlineData(500, 2, 500, 500, 500)]
        public void CalculateMostRankedFilterGroup_ReturnsDistributionAgreementDetailWithBestScore(
             int? royaltyTypeGroupId,
             int? territoryId,
             int? matchId,
             int? expectedRoyaltyTypeGroupId,
             int? expectedTerritoryId
         )
        {
            _fakeFilteredProperties.Add("RoyaltyTypeGroupId");
            _fakeFilteredProperties.Add("TerritoryId");

            //Arrange
            var listA = _fixture
                .Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeGroupId, royaltyTypeGroupId)
                .With(x => x.TerritoryId, territoryId)
                .CreateMany(1);

            var listB = _fixture
                .Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeGroupId, 500)
                .With(x => x.TerritoryId, 500)
                .CreateMany(1);

            _fakeDistributionAgreementDetailList = listA.Concat(listB);

            _fakeEntityToMatch = _fixture
                .Build<StatementDetail>()
                .With(x => x.RoyaltyTypeId, matchId)
                .With(x => x.TerritoryId, matchId)
                .Create();

            //Act
            var calculateMostRankedFilterGroup = _distributionAgreementRankScore
                .CalculateMostRankedFilterGroup(_fakeDistributionAgreementDetailList, _fakeEntityToMatch, _fakeFilteredProperties.ToList());

            //Assert
            Assert.Equal(expectedRoyaltyTypeGroupId, calculateMostRankedFilterGroup.RoyaltyTypeGroupId);
            Assert.Equal(expectedTerritoryId, calculateMostRankedFilterGroup.TerritoryId);
        }

        [Fact]
        public void CalculateMostRankedFilterGroup_IfNoPropertiesAreMatched_ShouldReturnFirstDistributionAgreementDetailSent()
        {
            _fakeFilteredProperties.Add("RoyaltyTypeGroupId");
            _fakeFilteredProperties.Add("TerritoryId");

            //Arrange
            _fakeDistributionAgreementDetailList = _fixture
                .Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeGroupId, 1)
                .With(x => x.TerritoryId, 2)
                .CreateMany(1);

            _fakeEntityToMatch = _fixture
                .Build<StatementDetail>()
                .With(x => x.RoyaltyTypeId, 123)
                .With(x => x.TerritoryId, 456)
                .Create();

            //Act
            var calculateMostRankedFilterGroup = _distributionAgreementRankScore
                .CalculateMostRankedFilterGroup(_fakeDistributionAgreementDetailList, _fakeEntityToMatch, _fakeFilteredProperties.ToList());

            //Assert
            Assert.Equal(1, calculateMostRankedFilterGroup.RoyaltyTypeGroupId);
            Assert.Equal(2, calculateMostRankedFilterGroup.TerritoryId);
        }

        [Fact]
        public void CalculateMostRankedFilterGroup_IfAFilteredPropertyDoesntExist_ShouldThrowException()
        {
            _fakeFilteredProperties.Add("fakeProperty");

            //Arrange
            _fakeDistributionAgreementDetailList = _fixture
                .Build<DistributionAgreementDetail>()
                .CreateMany(10);

            _fakeEntityToMatch = _fixture
                .Build<StatementDetail>()
                .Create();

            //Act && Assert
            Assert.Throws<RFDomainException>(() => _distributionAgreementRankScore
                .CalculateMostRankedFilterGroup(_fakeDistributionAgreementDetailList, _fakeEntityToMatch, _fakeFilteredProperties.ToList()));
        }
    }
}
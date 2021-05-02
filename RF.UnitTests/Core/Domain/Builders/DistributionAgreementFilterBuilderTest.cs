using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FakeItEasy;
using RF.Domain.Builders;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Filters;
using Xunit;

namespace RF.UnitTests.Core.Domain.Builders
{
    public class DistributionAgreementFilterBuilderTest
    {
        private readonly List<string> _fakeFilteredKeys;

        private readonly Random _randomId;

        private readonly ICreteria<DistributionAgreementDetail> _fakeCriteria;
        private DistributionAgreementFilterBuilder<DistributionAgreementDetail> _fakeDistributionAgreementFilterBuilder { get; }

        private readonly Fixture _fixture;

        public DistributionAgreementFilterBuilderTest()
        {
            _fakeFilteredKeys = new List<string>();

            _randomId = new Random();

            _fakeCriteria = A.Fake<ICreteria<DistributionAgreementDetail>>();

            _fakeDistributionAgreementFilterBuilder = new DistributionAgreementFilterBuilder<DistributionAgreementDetail>(_fakeCriteria);

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(3, 3, 0, 0)]
        [InlineData(1, 0, 10, 0)]
        [InlineData(null, 0, 0, 0)]
        [InlineData(1, null, 10, 0)]
        public void GetDistributionAgreementDetail_WithDistributionAgreement_ReturnQueryResults(int idCreate, int idDistributionAgreement, int listSize, int expectedListSize)
        {
            //Arrange

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                    .With(x => x.DistributionAgreementId, idCreate)
                    .CreateMany(listSize);

            var fakeEmptyDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .CreateMany(listSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.DistributionAgreementId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeEmptyDistributionAgreementDetailList)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithDistributionAgreement(idDistributionAgreement)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 0, 0)]
        [InlineData(true, 1, 1, 1, 1)]
        [InlineData(true, 1, 0, 0, 0)]
        [InlineData(false, 123, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 5)]
        public void GetDistributionAgreementDetail_WithDistributionAgreementCombinedWithOtherConditions_ReturnQueryResults(bool useFilter, int idDistributionAgreement, int idEpisode, int listSize, int expectedListSize)
        {
            //Arrange

            if (useFilter)
            {
                _fakeFilteredKeys.Add("EpisodeId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.DistributionAgreementId, idDistributionAgreement)
                .With(x => x.EpisodeId, idEpisode)
                .CreateMany(listSize);

            var fakeEmptyDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .CreateMany(listSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.DistributionAgreementId, _randomId.Next(200, 300))
                .With(x => x.EpisodeId, idEpisode)
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeEmptyDistributionAgreementDetailList)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithDistributionAgreement(idDistributionAgreement)
                .WithEpisode(useFilter, idEpisode)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 1, 2)]
        [InlineData(true, 1, 0, 5, 5)]
        [InlineData(false, 123, 0, 5, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithEpisode_ReturnQueryResults(bool useFilter, int? idEpisode, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("EpisodeId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.EpisodeId, idEpisode)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.EpisodeId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.EpisodeId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithEpisode(useFilter, idEpisode)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 0, 1)]
        [InlineData(true, 1, 0, 0, 0)]
        [InlineData(false, 123, 5, 0, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithLabel_ReturnQueryResults(bool useFilter, int? idLabel, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("LabelId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.LabelId, idLabel)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.LabelId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.LabelId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithLabel(useFilter, idLabel)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 0, 5, 5)]
        [InlineData(false, 123, 0, 0, 0)]
        [InlineData(false, 123, 5, 0, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithPlatformTier_ReturnQueryResults(bool useFilter, int? idPlatform, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("PlatformTierId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.PlatformTierId, idPlatform)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullPlatformId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.PlatformTierId)
                .Without(x => x.PlatformTier)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.PlatformTierId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullPlatformId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithPlatformTier(useFilter, idPlatform)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 0, 1)]
        [InlineData(true, 1, 0, 5, 5)]
        [InlineData(false, 123, 0, 0, 0)]
        [InlineData(false, 123, 0, 5, 0)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithPublisher_ReturnQueryResults(bool useFilter, int? idPublisher, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("PublisherId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.PublisherId, idPublisher)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.PublisherId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.PublisherId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithPublisher(useFilter, idPublisher)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 0, 1)]
        [InlineData(true, 1, 0, 0, 0)]
        [InlineData(false, 123, 0, 0, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithRoyaltyType_ReturnQueryResults(bool useFilter, int idRoyaltyType, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("RoyaltyTypeId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeId, idRoyaltyType)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.RoyaltyTypeId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.RoyaltyTypeId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithRoyaltyType(useFilter, idRoyaltyType)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 1, 2)]
        [InlineData(true, 1, 0, 0, 0)]
        [InlineData(false, 123, 0, 5, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithSociety_ReturnQueryResults(bool useFilter, int idSociety, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("SocietyId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.SocietyId, idSociety)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.SocietyId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.SocietyId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithSociety(useFilter, idSociety)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 0, 1)]
        [InlineData(true, 1, 5, 5, 10)]
        [InlineData(false, 123, 0, 0, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithTerritory_ReturnQueryResults(bool useFilter, int idTerritory, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("TerritoryId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.TerritoryId, idTerritory)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.TerritoryId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.TerritoryId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithTerritory(useFilter, idTerritory)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 1, 2)]
        [InlineData(true, 1, 5, 5, 10)]
        [InlineData(false, 123, 5, 0, 0)]
        [InlineData(true, null, 0, 5, 5)]
        [InlineData(false, null, 0, 5, 0)]
        public void GetDistributionAgreementDetail_WithSource_ReturnQueryResults(bool useFilter, int idSource, int listSize, int nullListSize, int expectedListSize)
        {
            //Arrange
            if (useFilter)
            {
                _fakeFilteredKeys.Add("SourceId");
            }

            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.SourceId, idSource)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.SourceId)
                .Without(x => x.EpisodeId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.SourceId, _randomId.Next(200, 300))
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithSource(useFilter, idSource)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }

        [Theory]
        [InlineData(true, 1, 1, 1, 0, 1)]
        [InlineData(false, 1, 1, 0, 5, 0)]
        [InlineData(true, 1, null, 1, 5, 1)]
        [InlineData(false, 1, null, 1, 5, 1)]
        [InlineData(true, null, 1, 0, 5, 0)]
        [InlineData(false, null, 1, 1, 5, 1)]
        [InlineData(true, null, null, 5, 5, 5)]
        public void GetDistributionAgreementDetail_WithAllParameters_ReturnQueryResults(bool useFilter, int idCreate, int fakeDistributionAgreementId, int listSize, int nullListSize, int expectedListSize)

        {
            //Arrange

            if (useFilter)
            {
                _fakeFilteredKeys.Add("EpisodeId");
                _fakeFilteredKeys.Add("LabelId");
                _fakeFilteredKeys.Add("PlatformTierId");
                _fakeFilteredKeys.Add("PublisherId");
                _fakeFilteredKeys.Add("RoyaltyTypeId");
                _fakeFilteredKeys.Add("SocietyId");
                _fakeFilteredKeys.Add("TerritoryId");
                _fakeFilteredKeys.Add("SourceId");
            }

            var randomId = _randomId.Next(200, 300);
            var fakeDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.DistributionAgreementId, fakeDistributionAgreementId)
                .With(x => x.EpisodeId, idCreate)
                .With(x => x.LabelId, idCreate)
                .With(x => x.PlatformTierId, idCreate)
                .With(x => x.PublisherId, idCreate)
                .With(x => x.RoyaltyTypeId, idCreate)
                .With(x => x.SocietyId, idCreate)
                .With(x => x.TerritoryId, idCreate)
                .With(x => x.SourceId, idCreate)
                .CreateMany(listSize);

            var fakeNullDistributionAgreementDetailListWithNullEpisodeId = _fixture.Build<DistributionAgreementDetail>()
                .Without(x => x.SourceId)
                .CreateMany(nullListSize);

            var fakeRandomDistributionAgreementDetailList = _fixture.Build<DistributionAgreementDetail>()
                .With(x => x.DistributionAgreementId, randomId)
                .With(x => x.EpisodeId, randomId)
                .With(x => x.LabelId, randomId)
                .With(x => x.PlatformTierId, randomId)
                .With(x => x.PublisherId, randomId)
                .With(x => x.RoyaltyTypeId, randomId)
                .With(x => x.SocietyId, randomId)
                .With(x => x.TerritoryId, randomId)
                .With(x => x.SourceId, randomId)
                .CreateMany(listSize);

            var allLists = fakeDistributionAgreementDetailList
                .Concat(fakeNullDistributionAgreementDetailListWithNullEpisodeId)
                .Concat(fakeRandomDistributionAgreementDetailList);

            var predicateForQuery = _fakeDistributionAgreementFilterBuilder
                .Start()
                .WithDistributionAgreement(fakeDistributionAgreementId)
                .WithEpisode(useFilter, idCreate)
                .WithLabel(useFilter, idCreate)
                .WithPlatformTier(useFilter, idCreate)
                .WithPublisher(useFilter, idCreate)
                .WithRoyaltyType(useFilter, idCreate)
                .WithSociety(useFilter, idCreate)
                .WithTerritory(useFilter, idCreate)
                .WithSource(useFilter, idCreate)
                .Build();

            //Act
            var result =
                allLists.Where(predicateForQuery.ExpressionCriteria.Compile());

            //Assert
            Assert.Equal(expectedListSize, result.Count());
            Assert.Equal(_fakeDistributionAgreementFilterBuilder.GetFilteredKeys(), _fakeFilteredKeys);
        }
    }
}
using System;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.PostProcessors;
using RF.Application.Core.UseCases.Episode.Queries.GetEpisodeIdByAlias;
using RF.Application.Core.UseCases.Label.Queries.GetLabelIdByAlias;
using RF.Application.Core.UseCases.PlatformTier.Queries.GetPlatformTierAndPlatformTypeIdByAlias;
using RF.Application.Core.UseCases.ProductionTitle.Queries.GetProductionTitleAndTypeIdByAlias;
using RF.Application.Core.UseCases.Publisher.Queries.GetPublisherIdByAlias;
using RF.Application.Core.UseCases.RoyaltyType.Queries.GetRoyaltyTypeIdAndRoyaltyTypeGroupIdByAliasAndSourceId;
using RF.Application.Core.UseCases.Society.Queries.GetSocietyIdByAlias;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdByIsrcOrIswcId;
using RF.Application.Core.UseCases.Territory.Queries.GetTerritoryAndRegionIdByAlias;
using RF.Domain.Dto;
using RF.Domain.Enum;
using RF.Domain.Interfaces.ValueObjects;
using Xunit;

namespace RF.UnitTests.Core.Application.Common.PostProcessors
{
    public class StatementQueriesPostProcessorTest
    {
        private readonly IStatementErrors _fakeStatementErrors;
        private readonly StatementQueriesPostProcessor _postProcessor;

        public StatementQueriesPostProcessorTest()
        {
            _fakeStatementErrors = A.Fake<IStatementErrors>();
            _postProcessor = new StatementQueriesPostProcessor(_fakeStatementErrors);
        }

        [Fact]
        public void Process_RoyaltyTypeIdIsNullAndRoyaltyTypeAliasIsAlsoNull_AddStatementErrors()
        {
            //Arrange
            var request = new GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery();
            var response = new RoyaltyTypeIdAndRoyaltyTypeGroupIdDto();

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.RoyaltyType, "Provided empty alias for mandatory field"))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.RoyaltyType, request.RoyaltyTypeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_TerritoryIdNullAndTerritoryAliasIsAlsoNull_AddStatementErrors()
        {
            //Arrange
            var request = new GetTerritoryAndRegionIdByTerritoryAliasQuery();
            var response = new TerritoryIdAndRegionIdDto();

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Territory, "Provided empty alias for mandatory field"))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Territory, request.TerritoryAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_SocietyIdIs0AndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake song";
            var request = new GetSocietyIdByAliasQuery()
            {
                SocietyAlias = fakeAlias
            };

            //Act
            _postProcessor.Process(request, 0, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Society, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_PublisherIdIsNullAndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake publisher";
            var request = new GetPublisherIdByAliasQuery()
            {
                PublisherAlias = fakeAlias
            };

            //Act
            _postProcessor.Process(request, null, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Publisher, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_PlatformTierIdIs0AndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake platform tier";
            var request = new GetPlatformTierAndPlatformTypeIdByAliasQuery()
            {
                PlatformTierAlias = fakeAlias
            };
            var response = new PlatformTierIdAndPlatformTypeIdDto();

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Platform, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_EpisodeIdIs0AndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake episode";
            var request = new GetEpisodeIdByAliasQuery()
            {
                EpisodeAlias = fakeAlias
            };

            //Act
            _postProcessor.Process(request, 0, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Episode, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_LabelIdIs0AndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake label";
            var request = new GetLabelIdByAliasQuery()
            {
                LabelAlias = fakeAlias
            };

            //Act
            _postProcessor.Process(request, 0, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Label, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_ProductionTitleIdIs0AndAliasNotEmpty_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake production title";
            var request = new GetProductionTitleAndTypeIdByAliasQuery()
            {
                ProductionTitleAlias = fakeAlias
            };

            var response = new ProductionTitleIdAndProductionTitleTypeIdDto();

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.ProductionTitle, "Could not find " + fakeAlias))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_GetSongIdByIsrcOrIswcIdQueryRequestSourceSongCodeNotNull_AddStatementError()
        {
            //Arrange
            const string fakeAlias = "fake source song code";
            var request = new GetSongIdByIsrcOrIswcIdQuery()
            {
                SourceSongCode = fakeAlias
            };

            const int response = 0;

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                    (ETLErrorEnum.Song,
                        $"Couldnt get Song by SourceSongCode: {request.SourceSongCode}" + Environment.NewLine))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_GetSongIdByIsrcOrIswcIdQueryRequestIsrcIdNotNull_AddStatementError()
        {
            //Arrange
            var request = new GetSongIdByIsrcOrIswcIdQuery()
            {
                IsrcId = 2
            };

            const int response = 0;

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                (ETLErrorEnum.Song,
                    $"Couldnt get Song by ISRC: {request.IsrcId}" + Environment.NewLine))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Process_GetSongIdByIsrcOrIswcIdQueryRequestIsAllNullable_AddStatementError()
        {
            //Arrange
            var request = new GetSongIdByIsrcOrIswcIdQuery();

            const int response = 0;

            //Act
            _postProcessor.Process(request, response, CancellationToken.None);

            //Assert
            A.CallTo(() => _fakeStatementErrors.AddStatementError
                (ETLErrorEnum.Song,
                    "Couldn't get song Id"))
                .MustHaveHappenedOnceExactly();
        }
    }
}
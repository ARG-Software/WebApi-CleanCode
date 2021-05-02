using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
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
using RF.Library.Utils;

namespace RF.Application.Core.Common.PostProcessors
{
    public class StatementQueriesPostProcessor :
        IRequestPostProcessor<GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery, RoyaltyTypeIdAndRoyaltyTypeGroupIdDto>,
        IRequestPostProcessor<GetTerritoryAndRegionIdByTerritoryAliasQuery, TerritoryIdAndRegionIdDto>,
        IRequestPostProcessor<GetSocietyIdByAliasQuery, int>,
        IRequestPostProcessor<GetPublisherIdByAliasQuery, int?>,
        IRequestPostProcessor<GetPlatformTierAndPlatformTypeIdByAliasQuery, PlatformTierIdAndPlatformTypeIdDto>,
        IRequestPostProcessor<GetEpisodeIdByAliasQuery, int>,
        IRequestPostProcessor<GetLabelIdByAliasQuery, int>,
        IRequestPostProcessor<GetProductionTitleAndTypeIdByAliasQuery, ProductionTitleIdAndProductionTitleTypeIdDto>,
        IRequestPostProcessor<GetSongIdByIsrcOrIswcIdQuery, int>

    {
        private readonly IStatementErrors _errors;

        public StatementQueriesPostProcessor(IStatementErrors errors)
        {
            _errors = errors;
        }

        public Task Process(GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery request,
            RoyaltyTypeIdAndRoyaltyTypeGroupIdDto response, CancellationToken cancellationToken)
        {
            var royaltyTypeId = response.RoyaltyTypeId;
            var alias = request.RoyaltyTypeAlias;

            if (string.IsNullOrEmpty(alias))
            {
                _errors.AddStatementError(ETLErrorEnum.RoyaltyType, "Provided empty alias for mandatory field");
            }

            if (royaltyTypeId == null)
            {
                _errors.AddStatementError(ETLErrorEnum.RoyaltyType, alias);
            }

            return Unit.Task;
        }

        public Task Process(GetTerritoryAndRegionIdByTerritoryAliasQuery request, TerritoryIdAndRegionIdDto response,
            CancellationToken cancellationToken)
        {
            var territoryId = response.TerritoryId;
            var alias = request.TerritoryAlias;

            if (string.IsNullOrEmpty(alias))
            {
                _errors.AddStatementError(ETLErrorEnum.Territory, "Provided empty alias for mandatory field");
            }

            if (territoryId == null)
            {
                _errors.AddStatementError(ETLErrorEnum.Territory, alias);
            }

            return Unit.Task;
        }

        public Task Process(GetSocietyIdByAliasQuery request, int response, CancellationToken cancellationToken)
        {
            var societyId = response;
            var alias = request.SocietyAlias;

            if (societyId == 0 && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.Society, "Could not find " + alias);
            }
            return Unit.Task;
        }

        public Task Process(GetPublisherIdByAliasQuery request, int? response, CancellationToken cancellationToken)
        {
            var publisherId = response;
            var alias = request.PublisherAlias;

            if (publisherId == null && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.Publisher, "Could not find " + alias);
            }
            return Unit.Task;
        }

        public Task Process(GetPlatformTierAndPlatformTypeIdByAliasQuery request, PlatformTierIdAndPlatformTypeIdDto response,
            CancellationToken cancellationToken)
        {
            var platformTierId = response.PlatformTierId;
            var alias = request.PlatformTierAlias;

            if (platformTierId == null && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.Platform, "Could not find " + alias);
            }

            return Unit.Task;
        }

        public Task Process(GetEpisodeIdByAliasQuery request, int response, CancellationToken cancellationToken)
        {
            var episodeId = response;
            var alias = request.EpisodeAlias;

            if (episodeId == 0 && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.Episode, "Could not find " + alias);
            }
            return Unit.Task;
        }

        public Task Process(GetLabelIdByAliasQuery request, int response, CancellationToken cancellationToken)
        {
            var labelId = response;
            var alias = request.LabelAlias;

            if (labelId == 0 && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.Label, "Could not find " + alias);
            }
            return Unit.Task;
        }

        public Task Process(GetProductionTitleAndTypeIdByAliasQuery request, ProductionTitleIdAndProductionTitleTypeIdDto response,
            CancellationToken cancellationToken)
        {
            var productionTitleId = response.ProductionTitleId;
            var alias = request.ProductionTitleAlias;

            if (productionTitleId == null && !(string.IsNullOrEmpty(alias)))
            {
                _errors.AddStatementError(ETLErrorEnum.ProductionTitle, "Could not find " + alias);
            }

            return Unit.Task;
        }

        public Task Process(GetSongIdByIsrcOrIswcIdQuery request, int response, CancellationToken cancellationToken)
        {
            var songId = response;

            if (songId != 0)
            {
                return Unit.Task;
            }

            var errorString = new StringBuilder()
                .AppendIf(!string.IsNullOrEmpty(request.SourceSongCode), $"Couldnt get Song by SourceSongCode: {request.SourceSongCode}" + Environment.NewLine)
                .AppendIf(!string.IsNullOrEmpty(request.Song), $"Couldnt get Song by Alias: {request.Song}" + Environment.NewLine)
                .AppendIf((request.IsrcId.HasValue), $"Couldnt get Song by ISRC: {request.IsrcId}" + Environment.NewLine)
                .AppendIf((request.IswcId.HasValue), $"Couldnt get Song by ISWC: {request.IswcId}" + Environment.NewLine)
                .ToString();

            _errors.AddStatementError(ETLErrorEnum.Song,
                string.IsNullOrEmpty(errorString) ? "Couldn't get song Id" : errorString);

            return Unit.Task;
        }
    }
}
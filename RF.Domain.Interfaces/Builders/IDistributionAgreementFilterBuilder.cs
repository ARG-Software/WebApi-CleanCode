using System.Collections.Generic;

namespace RF.Domain.Interfaces.Builders
{
    using Filters;

    public interface IDistributionAgreementFilterBuilder<T> where T : class
    {
        ICreteria<T> Build();

        IEnumerable<string> GetFilteredKeys();

        IDistributionAgreementFilterBuilder<T> Start();

        IDistributionAgreementFilterBuilder<T> WithDistributionAgreement(int distributionAgreementId);

        IDistributionAgreementFilterBuilder<T> WithRoyaltyType(bool isPartOfFilter, int? royaltyTypeId);

        IDistributionAgreementFilterBuilder<T> WithTerritory(bool isPartOfFilter, int? territoryId);

        IDistributionAgreementFilterBuilder<T> WithRegion(bool isPartOfFilter, int? regionId);

        IDistributionAgreementFilterBuilder<T> WithPublisher(bool isPartOfFilter, int? publisherId);

        IDistributionAgreementFilterBuilder<T> WithEpisode(bool isPartOfFilter, int? episodeId);

        IDistributionAgreementFilterBuilder<T> WithPlatformTier(bool isPartOfFilter, int? platformTierId);

        IDistributionAgreementFilterBuilder<T> WithPlatformType(bool isPartOfFilter, int? platformTypeId);

        IDistributionAgreementFilterBuilder<T> WithLabel(bool isPartOfFilter, int? labelId);

        IDistributionAgreementFilterBuilder<T> WithSociety(bool isPartOfFilter, int? societyId);

        IDistributionAgreementFilterBuilder<T> WithSource(bool isPartOfFilter, int? sourceId);

        IDistributionAgreementFilterBuilder<T> WithProductionType(bool isPartOfFilter, int? productionTypeId);

        IDistributionAgreementFilterBuilder<T> WithRoyaltyTypeGroup(bool isPartOfFilter, int? royaltyTypeGroupId);
    }
}
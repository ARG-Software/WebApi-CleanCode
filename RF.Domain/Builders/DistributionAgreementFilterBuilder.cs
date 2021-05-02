using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Filters;
using RF.Domain.Interfaces.Builders;
using RF.Library.LinqExtensions;

namespace RF.Domain.Builders
{
    public class DistributionAgreementFilterBuilder<T> : IDistributionAgreementFilterBuilder<T> where T : DistributionAgreementDetail, new()
    {
        private Expression<Func<T, bool>> _predicate;
        private IList<string> _filteredKeys;
        private readonly ICreteria<T> _creteria;

        public DistributionAgreementFilterBuilder(ICreteria<T> creteria)
        {
            _creteria = creteria;
        }

        public IDistributionAgreementFilterBuilder<T> Start()
        {
            _predicate = PredicateBuilder.New<T>();
            _filteredKeys = new List<string>();
            return this;
        }

        public ICreteria<T> Build()
        {
            _creteria.ExpressionCriteria = _predicate;
            return _creteria;
        }

        public IEnumerable<string> GetFilteredKeys()
        {
            return _filteredKeys;
        }

        public IDistributionAgreementFilterBuilder<T> WithDistributionAgreement(int distributionAgreementId)
        {
            _predicate = _predicate.And(x => x.DistributionAgreementId == distributionAgreementId);
            return this;
        }

        public IDistributionAgreementFilterBuilder<T> WithRoyaltyType(bool isPartOfFilter, int? royaltyTypeId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.RoyaltyTypeId);
            return QueryBuilder(isPartOfFilter, x => x.RoyaltyTypeId == royaltyTypeId || x.RoyaltyTypeId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithTerritory(bool isPartOfFilter, int? territoryId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.TerritoryId);
            return QueryBuilder(isPartOfFilter, x => x.TerritoryId == territoryId || x.TerritoryId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithRegion(bool isPartOfFilter, int? regionId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.RegionId);
            return QueryBuilder(isPartOfFilter, x => x.RegionId == regionId || x.RegionId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithPublisher(bool isPartOfFilter, int? publisherId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.PublisherId);
            return QueryBuilder(isPartOfFilter, x => x.PublisherId == publisherId || x.PublisherId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithEpisode(bool isPartOfFilter, int? episodeId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.EpisodeId);
            return QueryBuilder(isPartOfFilter, x => x.EpisodeId == episodeId || x.EpisodeId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithPlatformTier(bool isPartOfFilter, int? platformId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.PlatformTierId);
            return QueryBuilder(isPartOfFilter, x => x.PlatformTierId == platformId || x.PlatformTier == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithPlatformType(bool isPartOfFilter, int? platformTypeId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.PlatformTypeId);
            return QueryBuilder(isPartOfFilter, x => x.PlatformTypeId == platformTypeId || x.PlatformTypeId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithLabel(bool isPartOfFilter, int? labelId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.LabelId);
            return QueryBuilder(isPartOfFilter, x => x.LabelId == labelId || x.LabelId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithSociety(bool isPartOfFilter, int? societyId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.SocietyId);
            return QueryBuilder(isPartOfFilter, x => x.SocietyId == societyId || x.SocietyId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithSource(bool isPartOfFilter, int? sourceId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.SourceId);
            return QueryBuilder(isPartOfFilter, x => x.SourceId == sourceId || x.EpisodeId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithProductionType(bool isPartOfFilter, int? productionTypeId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.ProductionTypeId);
            return QueryBuilder(isPartOfFilter, x => x.ProductionTypeId == productionTypeId || x.ProductionTypeId == null);
        }

        public IDistributionAgreementFilterBuilder<T> WithRoyaltyTypeGroup(bool isPartOfFilter, int? royaltyTypeGroupId)
        {
            AddPropertyToFilteredKeys(isPartOfFilter, x => x.RoyaltyTypeGroupId);
            return QueryBuilder(isPartOfFilter, x => x.EpisodeId == royaltyTypeGroupId || x.RoyaltyTypeGroupId == null);
        }

        private void AddPropertyToFilteredKeys(bool isPartOfTheFilter, Expression<Func<T, object>> expr)
        {
            if (!isPartOfTheFilter)
            {
                return;
            }

            var propertyName = GenericExtensions.GetPropertyName(expr);
            _filteredKeys.Add(propertyName);
        }

        private IDistributionAgreementFilterBuilder<T> QueryBuilder(bool isPartOfFilter, Expression<Func<T, bool>> filter)
        {
            if (!isPartOfFilter)
            {
                return this;
            }

            var newPredicate = PredicateBuilder.New<T>();
            newPredicate = newPredicate.And(filter);
            _predicate = _predicate.And(newPredicate);
            return this;
        }
    }
}
using System.Collections.Generic;

namespace RF.Domain.Interfaces.ValueObjects
{
    public interface IDistributionAgreementRankScore<in TU, T>
    {
        public T CalculateMostRankedFilterGroup(IEnumerable<T> distributionAgreementDetail, TU entityToMatch,
            IEnumerable<string> filteredProperties);
    }
}
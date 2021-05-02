using System.Collections.Generic;
using RF.Domain.Entities;

namespace RF.Services.Interfaces.ETL
{
    public interface IRoyaltiesService
    {
        /// <summary>
        /// Inserts the royalties distributions based on the statement list passed.
        /// </summary>
        /// <param name="royaltiesList">The royalties list.</param>
        /// <returns></returns>
        void InsertRoyaltiesDistributions(IEnumerable<RoyaltyDistribution> royaltiesList);

        /// <summary>
        /// Calculates the royalties for the given statements.
        /// </summary>
        /// <param name="statementList">The statement list.</param>
        /// <param name="sourceId">The source identifier used to process royalties.</param>
        /// <returns></returns>
        IEnumerable<RoyaltyDistribution> RoyaltiesDistributionCalculation(IEnumerable<StatementDetail> statementList);
    }
}
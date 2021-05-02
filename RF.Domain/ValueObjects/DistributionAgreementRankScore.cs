using System;
using System.Collections.Generic;
using System.Linq;
using RF.Domain.Entities;
using RF.Domain.Exceptions;
using RF.Domain.Interfaces.Strategies;
using RF.Domain.Interfaces.ValueObjects;
using RF.Library.LinqExtensions;

namespace RF.Domain.ValueObjects
{
    public class DistributionAgreementRankScore<T, TOut> : IDistributionAgreementRankScore<T, TOut> where T : StatementDetail where TOut : DistributionAgreementDetail
    {
        private readonly IDistributionAgreementFilterScoreStrategy _scoreStrategy;
        private SortedDictionary<int, DistributionAgreementDetail> _agreementRank;

        public DistributionAgreementRankScore(IDistributionAgreementFilterScoreStrategy scoreStrategy)
        {
            _scoreStrategy = scoreStrategy;
        }

        private void StartNewRank()
        {
            _agreementRank = new SortedDictionary<int, DistributionAgreementDetail>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        }

        public TOut CalculateMostRankedFilterGroup(IEnumerable<TOut> distributionAgreementDetail, T entityToMatch,
            IEnumerable<string> filteredProperties)
        {
            StartNewRank();

            var distributionAgreementDetails = distributionAgreementDetail.ToList();
            var properties = filteredProperties.ToList();
            foreach (var agreement in distributionAgreementDetails)
            {
                var rowScore = 0;

                foreach (var property in properties)
                {
                    try
                    {
                        var valueOfStatementDetail = entityToMatch.GetPropertyValue(property);
                        var valueOfDistributionAgreementDetail = agreement.GetPropertyValue(property);
                        _scoreStrategy.GetRowScore(ref rowScore, valueOfDistributionAgreementDetail as int?,
                            valueOfStatementDetail as int?);
                    }
                    catch (ArgumentException)
                    {
                        throw new RFDomainException("Couldn't get filter properties from entities");
                    }
                }

                if (!_agreementRank.ContainsKey(rowScore))
                {
                    _agreementRank.Add(rowScore, agreement);
                }
            }

            var mostRankedRow = _agreementRank.FirstOrDefault().Value;
            return (TOut)mostRankedRow;
        }
    }
}
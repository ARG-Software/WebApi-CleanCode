using RF.Domain.Interfaces.Strategies;

namespace RF.Domain.Strategies
{
    public class DistributionAgreementFilterScoreStrategy : IDistributionAgreementFilterScoreStrategy
    {
        public int GetRowScore(ref int currentScore, int? valueA, int? valueB)
        {
            if (!valueA.HasValue || !valueB.HasValue)
            {
                return currentScore;
            }

            if (valueA.Value == valueB.Value)
            {
                currentScore += 1;
            }

            return currentScore;
        }
    }
}
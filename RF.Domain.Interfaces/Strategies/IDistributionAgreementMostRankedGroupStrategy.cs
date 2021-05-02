namespace RF.Domain.Interfaces.Strategies
{
    public interface IDistributionAgreementFilterScoreStrategy
    {
        int GetRowScore(ref int currentScore, int? valueA, int? valueB);
    }
}
using System;

namespace RF.Domain.Interfaces.ValueObjects
{
    public interface IParsedStatement
    {
        string PerformanceDate { get; }
        double? BonusAmount { get; }
        double? RoyaltyNetUsd { get; }
        double? UnitRate { get; }
        int? Quantity { get; }
        string Album { get; }
        string Episode { get; }
        string ISRC { get; }
        string ISWC { get; }
        string Label { get; }
        string Platform { get; }
        string ProductionTitle { get; }
        string Publisher { get; }
        string RoyaltyType { get; }
        string Society { get; }
        string Song { get; }
        string SourceSongCode { get; }
        string Territory { get; }

        DateTime GetPerformanceDate();

        double GetBonusAmount();

        double GetRoyaltyNetUsd();

        double GetRoyaltyNetUsdWithExchangeRate(double exchangeRate);

        double GetUnitRate();

        int GetQuantity();
    }
}
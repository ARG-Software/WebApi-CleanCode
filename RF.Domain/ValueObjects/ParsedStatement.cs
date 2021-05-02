using System;
using RF.Domain.Exceptions;
using RF.Domain.Interfaces.ValueObjects;
using RF.Library.Utils;

namespace RF.Domain.ValueObjects
{
    

    public class ParsedStatement : IParsedStatement
    {
        public ParsedStatement()
        {
        }

        public string PerformanceDate { get; set; }
        public double? BonusAmount { get; set; }
        public double? RoyaltyNetUsd { get; set; }
        public double? UnitRate { get; set; }
        public int? Quantity { get; set; }
        public string Album { get; set; }
        public string Episode { get; set; }
        public string ISRC { get; set; }
        public string ISWC { get; set; }
        public string Label { get; set; }
        public string Platform { get; set; }
        public string ProductionTitle { get; set; }
        public string Publisher { get; set; }
        public string RoyaltyType { get; set; }
        public string Society { get; set; }
        public string Song { get; set; }
        public string SourceSongCode { get; set; }
        public string Territory { get; set; }

        public DateTime GetPerformanceDate()
        {
            if (string.IsNullOrEmpty(PerformanceDate))
            {
                return DateTime.Now;
            }

            var parsedDate = DateFunctions.ConvertStringToDateTime(PerformanceDate);

            if (parsedDate.HasValue)
            {
                return parsedDate.Value;
            }

            throw new RFDomainException("Parsed Statement: Could not parse PerformanceDate");
        }

        public double GetBonusAmount()
        {
            return BonusAmount ?? 0;
        }

        public double GetRoyaltyNetUsd()
        {
            return RoyaltyNetUsd.GetValueOrDefault(0);
        }

        public int GetQuantity()
        {
            var quantity = Quantity.GetValueOrDefault(1);

            return quantity == 0 ? 1 : quantity;
        }

        public double GetRoyaltyNetUsdWithExchangeRate(double exchangeRate)
        {
            if (Math.Abs(exchangeRate) <= 0)
            {
                return GetRoyaltyNetUsd();
            }

            return GetRoyaltyNetUsd() * exchangeRate;
        }

        public double GetUnitRate()
        {
            return UnitRate ?? (GetRoyaltyNetUsd() / GetQuantity());
        }
    }
}
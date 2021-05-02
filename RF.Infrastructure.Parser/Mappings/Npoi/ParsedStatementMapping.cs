using Npoi.Mapper;
using RF.Domain.ValueObjects;

namespace RF.Infrastructure.Parser.Mappings.Npoi
{
    using Npoi = ExtensionMethods.Npoi.MappingExtensionMethods;

    public static class ParsedStatementMapping
    {
        public static void MapParsedStatement<T>(this Mapper importer) where T : ParsedStatement
        {
            importer.CheckThatMandatoryFieldsAreNotNullOrEmpty<T>(new[] { "Territory", "Song", "RoyaltyType" },
                p => p.Territory, p => p.Song, p => p.RoyaltyType);

            importer.Map<T>("PerformanceDate",
                p => p.PerformanceDate, (column, target) =>
                {
                    string performanceDate = null;

                    var mappingValueCondition = Npoi.ParseDateTimeValues(
                        Npoi.TrimCellValue(column), ref performanceDate);

                    if (!mappingValueCondition)
                    {
                        return false;
                    }

                    ((ParsedStatement)target).PerformanceDate = performanceDate;

                    return true;
                });

            importer.Map<T>("ISRC",
                p => p.ISRC, (column, target) =>
                {
                    string ISRC = null;

                    var mappingValueCondition = Npoi.ConvertToAlphaNumericOnlyValues(
                        Npoi.TrimCellValue(column), ref ISRC);

                    if (!mappingValueCondition)
                    {
                        return false;
                    }

                    ((ParsedStatement)target).ISRC = ISRC;

                    return true;
                });

            importer.Map<T>("ISWC",
                p => p.ISWC, (column, target) =>
                {
                    string ISWC = null;

                    var mappingValueCondition = Npoi.ConvertToAlphaNumericOnlyValues(
                        Npoi.TrimCellValue(column), ref ISWC);

                    if (!mappingValueCondition)
                    {
                        return false;
                    }

                    ((ParsedStatement)target).ISWC = ISWC;

                    return true;
                });
        }
    }
}
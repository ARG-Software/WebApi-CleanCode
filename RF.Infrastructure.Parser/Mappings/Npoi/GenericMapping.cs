using System;
using System.Linq.Expressions;
using Npoi.Mapper;
using NPOI.SS.UserModel;

namespace RF.Infrastructure.Parser.Mappings.Npoi
{
    using System.Reflection;
    using Exceptions;
    using Library.LinqExtensions;
    using System.Collections.Generic;
    using System.Linq;
    using RF.Infrastructure.Parser.ExtensionMethods.Npoi;

    public static class GenericMapping
    {
        public static void CheckIfHeaderIsCorrectlyMapped<T>(this IRow header, IEnumerable<PropertyInfo> headerProperties) where T : class
        {
            if (header == null || !header.Cells.Any())
            {
                throw new RFParserException("No header defined");
            }

            var headersInFile = header.Cells.Select(x => x.StringCellValue);
            var headersNeeded = headerProperties.Select(x => x.Name);

            var headersNotPresentInFile = headersNeeded.Except(headersInFile).ToList();

            if (headersNotPresentInFile.Any())
            {
                throw new RFParserException("Header of the file incomplete. Headers missing are : " +
                                            string.Join<string>(", ", headersNotPresentInFile));
            }
        }

        public static void TrimAllStringFields<T>(this Mapper importer, IEnumerable<PropertyInfo> headerProperties) where T : class
        {
            var properties = headerProperties.Where(p => p.PropertyType == typeof(string));

            foreach (var prop in properties)
            {
                importer.Map(prop.Name, ExpressionExtensions.CreatePropSelectorExpression<T>(prop.Name), (column, target) =>
                    {
                        var trimmedValue = MappingExtensionMethods.TrimCellValue(column);

                        target.GetType().GetProperty(prop.Name)?.SetValue(target, trimmedValue, null);

                        return true;
                    }
               );
            }
        }

        public static void CheckThatMandatoryFieldsAreNotNullOrEmpty<T>(this Mapper importer,
            IEnumerable<string> properties, params Expression<Func<T, object>>[] propertySelector) where T : class
        {
            var propertiesList = properties.ToList();
            for (var i = 0; i < propertiesList.Count(); i++)
            {
                var prop = propertiesList[i];
                var selector = propertySelector[i];
                importer.Map(prop, selector, (column, target) =>
                    {
                        var value = column.CurrentValue;

                        if (value == null || string.IsNullOrEmpty(value.ToString()))
                        {
                            return false;
                        }

                        target.GetType().GetProperty(prop)?.SetValue(target, value);
                        return true;
                    }
                );
            }
        }

        public static void ParseDoubleProperties<T>(this Mapper importer,
            IEnumerable<PropertyInfo> properties)
        {
            foreach (var prop in properties)
            {
                importer.Map(prop.Name, ExpressionExtensions.CreatePropSelectorExpression<T>(prop.Name),
                    (column, target) =>
                    {
                        double? valueParsed = null;

                        var mappingValueCondition = MappingExtensionMethods.ParseDoubleValues(
                            column.CurrentValue, ref valueParsed);

                        if (!mappingValueCondition)
                        {
                            return false;
                        }

                        if (prop.PropertyType == typeof(double))
                        {
                            if (valueParsed == null)
                            {
                                valueParsed = 0;
                            }

                            target.GetType().GetProperty(prop.Name)?.SetValue(target, valueParsed);
                            return true;
                        }
                        target.GetType().GetProperty(prop.Name)?.SetValue(target, valueParsed, null);
                        return true;
                    }
                );
            }
        }
    }
}
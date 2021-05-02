using System.Runtime.CompilerServices;
using RF.Domain.Interfaces.ValueObjects;

[assembly: InternalsVisibleTo("RF.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace RF.Infrastructure.Parser
{
    using Exceptions;
    using System;
    using System.IO;
    using System.Text;
    using System.Reflection;
    using Library.LinqExtensions;
    using System.Collections.Generic;

    public abstract class BaseParser<T> where T : class
    {
        protected int SheetToReadIndex { get; private set; }
        protected int StartingLineIndex { get; private set; }
        protected IEnumerable<PropertyInfo> HeaderProperties { get; }
        private readonly StringBuilder _parsingErrors;

        protected BaseParser()
        {
            _parsingErrors = new StringBuilder();
            HeaderProperties = GenericExtensions.GetObjectPropertiesList<T>();
        }

        protected void SetParserMetadata(ITemplateDefinition info)
        {
            if (info == null)
            {
                throw new RFParserException("Source to parse file not present.");
            }

            SheetToReadIndex = info.WorkSheetNumber ?? 0;
            StartingLineIndex = info.StartingLine;
        }

        protected void AddParseError(int column, int row)
        {
            _parsingErrors
                .AppendFormat("Error Parsing on row:{0} and column: {1} {2}", row, column, Environment.NewLine);
        }

        protected string GetErrors()
        {
            return _parsingErrors.ToString();
        }

        internal abstract IEnumerable<T> ParseFile(Stream fileToParse, ITemplateDefinition info);
    }
}
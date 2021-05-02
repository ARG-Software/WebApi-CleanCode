using System.Threading.Tasks;
using RF.Domain.Interfaces.ValueObjects;
using System;
using System.IO;
using RF.Infrastructure.Parser.Exceptions;
using System.Linq;
using Npoi.Mapper;
using System.Collections.Generic;
using RF.Domain.Interfaces.Parser;
using RF.Domain.ValueObjects;
using RF.Infrastructure.Parser.Mappings.Npoi;

namespace RF.Infrastructure.Parser
{
    public class NpoiParser<T> : BaseParser<T>, IParser<T> where T : class, new()
    {
        private Mapper _importer;

        public Task<IEnumerable<T>> ConvertStreamFileToObjectList(Stream fileToParse, ITemplateDefinition info)
        {
            if (fileToParse == null)
            {
                throw new RFParserException("File is corrupted or not present.");
            }

            if (info == null)
            {
                throw new RFParserException("Source to parse file not present.");
            }

            try
            {
                SetParserMetadata(info);
                var parsedFileList = ParseFile(fileToParse, info);
                return Task.FromResult(parsedFileList);
            }
            catch (Exception e)
            {
                throw new RFParserException(e.Message);
            }
        }

        internal override IEnumerable<T> ParseFile(Stream fileToParse, ITemplateDefinition info)
        {
            ReadFile(fileToParse);

            SetMapping();

            VerifyFileHeader();

            var parsedList = _importer.Take<T>(SheetToReadIndex, 0).ToList();

            if (parsedList.Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
            {
                GetParsingErrors(parsedList);
            }

            var convertedList = parsedList.Select(statement => statement.Value).ToList();

            return convertedList;
        }

        private void ReadFile(Stream fileToParse)
        {
            try
            {
                _importer = new Mapper(fileToParse)
                {
                    HasHeader = true,
                    FirstRowIndex = StartingLineIndex
                };
            }
            catch (Exception e)
            {
                throw new RFParserException("Couldnt read file. It is corrupted or has wrong format.");
            }
        }

        private void SetMapping()
        {
            _importer.TrimAllStringFields<T>(HeaderProperties);

            var doublePropertiesToMap = HeaderProperties.Where(p => p.PropertyType == typeof(double?) || p.PropertyType == typeof(double));

            _importer.ParseDoubleProperties<T>(doublePropertiesToMap);

            if (typeof(IParsedStatement).IsAssignableFrom(typeof(T)))
            {
                _importer.MapParsedStatement<ParsedStatement>();
            }
        }

        private void VerifyFileHeader()
        {
            var header = _importer.Workbook.GetSheetAt(SheetToReadIndex).GetRow(StartingLineIndex);

            header.CheckIfHeaderIsCorrectlyMapped<T>(HeaderProperties);
        }

        private void GetParsingErrors(IEnumerable<RowInfo<T>> parsedList)
        {
            foreach (var element in parsedList)
            {
                if (!string.IsNullOrEmpty(element.ErrorMessage))
                {
                    AddParseError(element.ErrorColumnIndex, element.RowNumber);
                }
            }

            throw new RFParserException(GetErrors());
        }
    }
}
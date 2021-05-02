using System;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.UnitTests.Infrastructure.Parser
{
    using Xunit;
    using Fixtures;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using RF.Infrastructure.Parser;
    using System.Collections.Generic;
    using RF.Infrastructure.Parser.Exceptions;

    public sealed class ParsingTestClass
    {
        public double DoubleRow { get; set; }
        public string StringRow { get; set; }
        public int IntRow { get; set; }
    }

    #region Mocked Abstract class. If you add a method to the abstract class, please add it here to test

    public class ExposedBaseParser<T> : BaseParser<T> where T : class
    {
        #region Variables

        public IEnumerable<PropertyInfo> GetHeaderProperties()
        {
            return this.HeaderProperties;
        }

        public int GetSheetToReadIndex()
        {
            return this.SheetToReadIndex;
        }

        public int GetStartingLineIndex()
        {
            return this.StartingLineIndex;
        }

        #endregion Variables

        public void ExposedSetParserMetadata(ITemplateDefinition info)
        {
            this.SetParserMetadata(info);
        }

        public void ExposedAddParseError(int column, int row)
        {
            this.AddParseError(column, row);
        }

        public string ExposedGetErrors()
        {
            return this.GetErrors();
        }

        internal override IEnumerable<T> ParseFile(Stream fileToParse, ITemplateDefinition info)
        {
            throw new System.NotImplementedException();
        }
    }

    #endregion Mocked Abstract class. If you add a method to the abstract class, please add it here to test

    public class BaseParserTest : IClassFixture<ParserFixture>
    {
        private readonly ExposedBaseParser<ParsingTestClass> _baseParser;
        private readonly ParserFixture _fixture;

        public BaseParserTest(ParserFixture fixture)
        {
            _baseParser = new ExposedBaseParser<ParsingTestClass>();
            _fixture = fixture;
        }

        [Fact]
        public void BaseParserConstructor()
        {
            //Act & Assert
            var headerProperties = _baseParser.GetHeaderProperties().Select(X => X.Name);
            var errors = _baseParser.ExposedGetErrors();

            Assert.NotNull(headerProperties);
            Assert.NotNull(errors);
            Assert.Equal(string.Empty, errors);
            Assert.Contains("DoubleRow", headerProperties);
        }

        [Fact]
        public void SetParserMetadata_IfInfoIsNull_ThrowException()
        {
            //Assert & Assert
            Assert.Throws<RFParserException>(() => _baseParser.ExposedSetParserMetadata(null));
        }

        [Fact]
        public void SetParserMetadata_IfInfoIsValid_SetValuesCorrectly()
        {
            //Arrange
            const int mockStartingLine = 2;
            const int mockWorkSheetIndex = 5;
            _fixture.MockSourceParameters(mockStartingLine, mockWorkSheetIndex);

            var mockedSourceStructure = _fixture.GetSourceMockObject();

            //Act
            _baseParser.ExposedSetParserMetadata(mockedSourceStructure);

            //Assert
            Assert.Equal(mockStartingLine, _baseParser.GetStartingLineIndex());
            Assert.Equal(mockWorkSheetIndex, _baseParser.GetSheetToReadIndex());
        }

        [Fact]
        public void SetParserMetadata_IfWorksheetNumberIsNull_Return0ForSheetReadIndex()
        {
            //Arrange
            const int mockStartingLine = 2;
            _fixture.MockSourceParameters(mockStartingLine, null);
            var mockedSourceStructure = _fixture.GetSourceMockObject();

            //Act
            _baseParser.ExposedSetParserMetadata(mockedSourceStructure);

            //Assert
            Assert.Equal(0, _baseParser.GetSheetToReadIndex());
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 4)]
        public void ParseErrorTheory(int col, int row)
        {
            //Act
            _baseParser.ExposedAddParseError(col, row);
            var errors = _baseParser.ExposedGetErrors();
            var expectedError = $"Error Parsing on row:{row} and column: {col} {Environment.NewLine}";

            //Assert
            Assert.Equal(expectedError, errors);
        }
    }
}
using System.Linq;
using RF.Domain.ValueObjects;
using Xunit;
using RF.UnitTests.Infrastructure.Parser.Fixtures;
using System.IO;
using RF.Infrastructure.Parser.Exceptions;

namespace RF.UnitTests.Infrastructure.Parser
{
    public class NpoiParserTest : IClassFixture<ParserFixture>
    {
        private readonly ParserFixture _fixture;

        private readonly Stream _file;

        public NpoiParserTest(ParserFixture fixture)
        {
            _fixture = fixture;
            _file = _fixture.LoadFile();
        }

        [Fact]
        public async System.Threading.Tasks.Task ConvertStreamFileToObjectList_IfFileStreamIsNull_ThrowsException()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 2);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsingTestClass>()
                    .ConvertStreamFileToObjectList(null, _fixture.GetSourceMockObject()));

            Assert.Equal("File is corrupted or not present.", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfFileSourceIsNull_ThrowsException()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 2);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsingTestClass>()
                    .ConvertStreamFileToObjectList(_fixture.LoadFile(), null));

            Assert.Equal("Source to parse file not present.", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfFileHasUnsupportedExtension_ThrowsException()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 0);
            using (var fileStream = File.Create("mock.csv", 300))
            {
                //Act && Assert
                var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                     _fixture.GetParser<ParsingTestClass>()
                         .ConvertStreamFileToObjectList(fileStream, _fixture.GetSourceMockObject()));

                Assert.Equal("Couldnt read file. It is corrupted or has wrong format.", exception.Message);
            }
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_FileFollowsTheStandard_ReturnListWithParsedValues()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 0);

            //Act
            var result = await _fixture.GetParser<ParsingTestClass>()
                .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject());
            var resultToList = result.ToList();

            //Assert
            Assert.NotNull(resultToList);
            Assert.Equal(2, resultToList.Count());
            var firstElement = resultToList.First();
            Assert.Equal(0.005038445, firstElement.DoubleRow);
            Assert.Equal("row1", firstElement.StringRow);
            Assert.Equal(0, firstElement.IntRow);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_FieldsNeedToBeTrimmed_ReturnListWithParsedAndTrimmedValues()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 1);

            //Act
            var result = await _fixture.GetParser<ParsingTestClass>()
                .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject());

            var resultList = result.ToList();

            //Assert
            var firstElement = resultList.First();
            var secondElement = resultList.Last();

            Assert.Equal("row1", firstElement.StringRow);
            Assert.Equal("row2", secondElement.StringRow);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IncorrectHeaders_ThrowsExceptionWithMissingHeaders()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 2);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsingTestClass>()
                    .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject()));

            Assert.Equal("Header of the file incomplete. Headers missing are : DoubleRow, StringRow", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_NoHeaderPresent_ThrowsException()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 3);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                 _fixture.GetParser<ParsingTestClass>()
                     .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject()));

            Assert.Equal("No header defined", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfValuesCouldntBeConverted_ThrowsExceptionWithIncorrectRowAndColumn()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 4);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsingTestClass>()
                    .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject()));

            Assert.Contains("Error Parsing on row:1 and column: 0 ", exception.Message);
            Assert.Contains("Error Parsing on row:2 and column: 2 ", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfFileHasEmptyLine_ReturnListIgnoringBlankLine()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 5);

            //Act
            var result = await _fixture.GetParser<ParsingTestClass>()
                .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject());

            //Assert
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_ForParsedStatement_ReturnListWithParsedValues()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 6);

            //Act
            var result = await _fixture.GetParser<ParsedStatement>()
                .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject());

            var resultList = result.ToList();

            //Assert
            Assert.NotNull(resultList);
            Assert.Equal(2, resultList.Count());
            var firstElement = resultList.First();
            Assert.Equal("United States", firstElement.Territory);
            Assert.Equal("APPLE, INC", firstElement.Platform);
            Assert.Equal("10/02/2018 00:00:00", firstElement.PerformanceDate);
            var lastElement = resultList.Last();
            Assert.Equal("01/01/2019 00:00:00", lastElement.PerformanceDate);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_ForParsedStatementIfDateIsInIncorrectFormat_ThrowsException()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 7);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsedStatement>()
                    .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject()));

            Assert.Contains("Error Parsing on row:1 and column: 10 ", exception.Message);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfFieldIsNotAlphaNumeric_ReturnListWithValuesConvertedToAlphaNumeric()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 8);

            //Act
            var result = await _fixture.GetParser<ParsedStatement>()
                .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject());

            var resultList = result.ToList();

            //Assert
            Assert.NotNull(resultList);
            Assert.Equal(2, resultList.Count());
            var firstElement = resultList.First();
            Assert.Equal("USFKP0600923", firstElement.ISRC);
            Assert.Equal("T0701934420", firstElement.ISWC);
            var lastElement = resultList.Last();
            Assert.Null(lastElement.ISRC);
            Assert.Equal("", lastElement.ISWC);
        }

        [Fact]
        public async void ConvertStreamFileToObjectList_IfMandatoryFieldsEmpty_ThrowsExceptionWithIncorrectRowAndColumn()
        {
            //Arrange
            _fixture.MockSourceParameters(0, 9);

            var exception = await Assert.ThrowsAsync<RFParserException>(() =>
                _fixture.GetParser<ParsedStatement>()
                    .ConvertStreamFileToObjectList(_file, _fixture.GetSourceMockObject()));

            Assert.Contains("Error Parsing on row:1 and column: 19", exception.Message);
            Assert.Contains("Error Parsing on row:2 and column: 0 ", exception.Message);
            Assert.Contains("Error Parsing on row:3 and column: 9 ", exception.Message);
        }
    }
}
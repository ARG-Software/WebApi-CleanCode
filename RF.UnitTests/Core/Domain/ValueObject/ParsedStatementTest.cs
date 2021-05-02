using System;
using AutoFixture;
using RF.Domain.Exceptions;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;
using RF.Library.Utils;
using Xunit;

namespace RF.UnitTests.Core.Domain.ValueObject
{
    public class ParsedStatementTest
    {
        private readonly ParsedStatement _mockedParsedStatement;
        private readonly Fixture _fixture;

        public ParsedStatementTest()
        {
            _mockedParsedStatement = new ParsedStatement();
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ReturnNewParsedStatementObject()
        {
            //Act
            var result = new ParsedStatement();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IParsedStatement>(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetPerformanceDate_IfPerformanceDateIsEmptyOrNull_ReturnsCurrentDateTime(string performanceDate)
        {
            //Arrange

            _mockedParsedStatement.PerformanceDate = performanceDate;

            var dateTimeAtStartOfTest = DateTime.Now;

            //Act
            var result = _mockedParsedStatement.GetPerformanceDate();

            //Assert
            Assert.InRange(result, dateTimeAtStartOfTest, DateTime.Now);
        }

        [Fact]
        public void GetPerformanceDate_IfDateIsCorrectlyParsed_ReturnPerformanceDate()
        {
            //Arrange
            const string mockedDate = "25/5/1986";
            _mockedParsedStatement.PerformanceDate = mockedDate;
            var expectedDate = DateFunctions.ConvertStringToDateTime(mockedDate);

            //Act
            var result = _mockedParsedStatement.GetPerformanceDate();

            //Assert
            Assert.IsAssignableFrom<DateTime>(result);
            Assert.Equal(result, expectedDate);
        }

        [Fact]
        public void GetPerformanceDate_IfCouldntParsePerformanceDate_ThrowsException()
        {
            //Arrange
            const string mockedDate = "asadasda";
            _mockedParsedStatement.PerformanceDate = mockedDate;
            DateTime.TryParse(mockedDate, out var expectedDate);

            //Act
            var exception = Assert.Throws<RFDomainException>(() => _mockedParsedStatement.GetPerformanceDate());

            //Assert
            Assert.Equal("Parsed Statement: Could not parse PerformanceDate", exception.Message);
        }

        [Fact]
        public void GetBonusAmount_IfBonusAmountIsNull_Returns0()
        {
            //Arrange
            _mockedParsedStatement.BonusAmount = null;

            //Act
            var result = _mockedParsedStatement.GetBonusAmount();

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetBonusAmount_IfBonusAmountIsNotNull_ReturnsBonusAmountValue()
        {
            //Arrange
            const double mockedBonusAmountValue = 12.3;
            _mockedParsedStatement.BonusAmount = mockedBonusAmountValue;

            //Act
            var result = _mockedParsedStatement.GetBonusAmount();

            //Assert
            Assert.Equal(mockedBonusAmountValue, result);
        }

        [Fact]
        public void GetRoyaltyNetUsd_IfRoyaltyNetUsdNull_Return0()
        {
            //Arrange
            _mockedParsedStatement.RoyaltyNetUsd = null;

            //Act
            var result = _mockedParsedStatement.GetRoyaltyNetUsd();

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetRoyaltyNetUsd_IfRoyaltyNetUsdIsNotNull_ReturnsRoyaltyNetUsdValue()
        {
            //Arrange
            const double mockedRoyaltyNetUsd = 12.4;
            _mockedParsedStatement.RoyaltyNetUsd = mockedRoyaltyNetUsd;

            //Act
            var result = _mockedParsedStatement.GetRoyaltyNetUsd();

            //Assert
            Assert.Equal(mockedRoyaltyNetUsd, result);
        }

        [Fact]
        public void GetQuantity_IfQuantityIsNull_Returns1()
        {
            //Arrange
            _mockedParsedStatement.Quantity = null;

            //Act
            var result = _mockedParsedStatement.GetQuantity();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetQuantity_IfQuantityIsNotNull_ReturnsQuantityValue()
        {
            //Arrange
            const int mockedQuantity = 12;
            _mockedParsedStatement.Quantity = mockedQuantity;

            //Act
            var result = _mockedParsedStatement.GetQuantity();

            //Assert
            Assert.Equal(mockedQuantity, result);
        }

        [Fact]
        public void GetQuantity_IfQuantityIsNegativeNumber_ReturnsQuantityValue()
        {
            //Arrange
            const int mockedQuantity = -12;
            _mockedParsedStatement.Quantity = mockedQuantity;

            //Act
            var result = _mockedParsedStatement.GetQuantity();

            //Assert
            Assert.Equal(mockedQuantity, result);
        }

        [Fact]
        public void GetQuantity_IfQuantityIsZero_ReturnsOneAsValue()
        {
            //Arrange
            const int expectedResult = 1;
            const int mockedQuantity = 0;

            _mockedParsedStatement.Quantity = mockedQuantity;

            //Act
            var result = _mockedParsedStatement.GetQuantity();

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0.01, 0.12, 0.0012)]
        [InlineData(0.0, 0.12, 0.12)]
        public void GetRoyaltyNetUsdWithExchangeRateTheory(double exchangeRate, double royaltyNet, double expectedResult)
        {
            //Arrange
            _mockedParsedStatement.RoyaltyNetUsd = royaltyNet;

            //Act
            var result = _mockedParsedStatement.GetRoyaltyNetUsdWithExchangeRate(exchangeRate);

            //Assert
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(1, 0.12, 0.12)]
        [InlineData(2, 0.24, 0.12)]
        public void GetUnitRateTheory(int quantity, double royaltyNetUsd, double expectedResult)
        {
            //Arrange
            _mockedParsedStatement.RoyaltyNetUsd = royaltyNetUsd;
            _mockedParsedStatement.Quantity = quantity;

            //Act
            var result = _mockedParsedStatement.GetUnitRate();

            //Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetUnitRate_IfUnitRateIsNotNull_ReturnUnitRateValue()
        {
            //Arrange
            const double mockedUnitRate = 0.12;
            _mockedParsedStatement.UnitRate = mockedUnitRate;

            //Act
            var result = _mockedParsedStatement.GetUnitRate();

            //Assert
            Assert.Equal(result, mockedUnitRate);
        }
    }
}
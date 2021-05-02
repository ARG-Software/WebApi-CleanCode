using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using RF.Domain.Strategies;
using Xunit;

namespace RF.UnitTests.Core.Domain.Strategies
{
    public class DistributionAgreementFilterScoreStrategyTest
    {
        private readonly Random _random;
        private readonly Fixture _fixture;
        private readonly DistributionAgreementFilterScoreStrategy _scoreStrategy;

        public DistributionAgreementFilterScoreStrategyTest()
        {
            _random = new Random();
            _fixture = new Fixture();
            _scoreStrategy = new DistributionAgreementFilterScoreStrategy();
        }

        [Theory]
        [InlineData(1, 1, 5, 5, 5)]
        [InlineData(1, 1, 0, 5, 0)]
        [InlineData(1, 2, 5, 5, 0)]
        public void GetRowScore_WhenPropertiesMatch_ShouldReturnCorrectScore(int valueA, int valueB, int matchedListSize, int unmatchedListSize, int expectedScore)
        {
            //Arrange
            var rowScore = 0;

            var matchedList = _fixture.Build<TestClass>()
                .With(x => x.valueA, valueA)
                .With(x => x.valueB, valueB)
                .CreateMany(matchedListSize);

            var unmatchedList = _fixture.Build<TestClass>()
                .With(x => x.valueA, _random.Next(valueA + 1, 100))
                .With(x => x.valueB, _random.Next(valueA + 1, 100))
                .CreateMany(unmatchedListSize);

            var listToCheckScore = matchedList.Concat(unmatchedList);

            //Act
            foreach (var element in listToCheckScore)
            {
                _scoreStrategy.GetRowScore(ref rowScore, element.valueA, element.valueB);
            }

            //Assert
            Assert.Equal(expectedScore, rowScore);
        }

        [Fact]
        public void GetRowScore_WhenOneOfThePropertiesAreNull_ShouldReturnScoreAs0()
        {
            //Arrange
            var rowScore = 0;

            var valueANullList = _fixture.Build<TestClass>()
                .Without(x => x.valueA)
                .With(x => x.valueB, 1)
                .CreateMany(5);

            var valueBNullList = _fixture.Build<TestClass>()
                .With(x => x.valueA, 1)
                .Without(x => x.valueB)
                .CreateMany(5);

            var listToCheckScore = valueANullList.Concat(valueBNullList);

            //Act
            foreach (var element in listToCheckScore)
            {
                _scoreStrategy.GetRowScore(ref rowScore, element.valueA, element.valueB);
            }

            //Assert
            Assert.Equal(0, rowScore);
        }

        public class TestClass
        {
            public int? valueB { get; set; }
            public int? valueA { get; set; }
        }
    }
}
using System;
using System.Text;
using Xunit;
using RF.Library.Utils;

namespace RF.UnitTests.Libraries.Utils
{
    public class StringFunctionTest

    {
        [Theory]
        [InlineData("LK3 $  2    NM73   62IEW")]
        [InlineData("...#LK3$2NM7362IEW")]
        [InlineData("####$$$$$LK32NM7362IEW$$$$$#####")]
        public void GetValueAsAlphaNumeric_IfValueIsNormal_ReturnsAlphaNumericAsValue(string valueDefinition)
        {
            //Arrange
            const string expectedResult = "LK32NM7362IEW";

            //Act
            var result = valueDefinition.ConvertToAlphaNumeric();

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(true, new[] { "test1", "test2" }, "test1test2")]
        [InlineData(false, new[] { "test1", "test2" }, "")]
        public void AppendIf_BasedOnCondition_AppendToString(bool condition, string[] strings, string expectedOutput)
        {
            //Arrange
            var fakeStringBuilder = new StringBuilder();

            //Act
            foreach (var text in strings)
            {
                fakeStringBuilder.AppendIf(condition, text);
            }

            //Assert
            Assert.Equal(expectedOutput, fakeStringBuilder.ToString());
        }
    }
}
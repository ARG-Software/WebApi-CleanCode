using System;
using RF.Domain.Entities;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;
using Xunit;

namespace RF.UnitTests.Core.Domain.ValueObject
{
    public class TemplateDefinitionTest
    {
        private readonly TemplateDefinition _mockedTemplateStructure;

        public TemplateDefinitionTest()
        {
            _mockedTemplateStructure = new TemplateDefinition();
        }

        [Fact]
        public void Constructor_ReturnNewParsedStatementObject()
        {
            //Act
            var result = new TemplateDefinition();

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Fields);
            Assert.IsAssignableFrom<ITemplateDefinition>(result);
        }

        [Theory]
        [InlineData(null, "Definition string is null or empty")]
        [InlineData("Bad definition", "Error parsing template definition, incorrect or invalid one")]
        public void CreateTemplateDefinition_InvalidInput_ThrowsException(string templateDefinition,
            string exceptionMessage)
        {
            //Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _mockedTemplateStructure.CreateTemplateDefinition(templateDefinition));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Theory]
        [InlineData(
            "{\"label\":\"BMG Source\",\"startingLine\": 0,\"fields\": { \"Society\" : {\"type\": \"column\", \"value\": \"S\"}}}")]
        public void CreateTemplateDefinition_ValidDefinition_ReturnsTemplateStructure(
            string templateDefinition)
        {
            //Act
            var result = _mockedTemplateStructure.CreateTemplateDefinition(templateDefinition);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ITemplateDefinition>(result);
        }

        [Fact]
        public void IsDefinitionValid_IfInvalidDefinition_ReturnsFalse()
        {
            //Arrange
            const string templateDefinition = "invalid definition";

            //Act
            var result = _mockedTemplateStructure.IsDefinitionValid(templateDefinition);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateTemplateStructure_IfDefinitionIsValid_ReturnsTrue()
        {
            //Arrange
            const string templateDefinition = "{\"label\":\"BMG Source\",\"startingLine\": 0,\"fields\": { \"Society\" : {\"type\": \"column\", \"value\": \"S\"}}}";

            //Act
            var result = _mockedTemplateStructure.IsDefinitionValid(templateDefinition);

            //Assert
            Assert.True(result);
        }
    }
}
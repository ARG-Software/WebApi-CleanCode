using System.Collections.Generic;
using System.Linq;
using RF.Domain.Enum;
using RF.Domain.ValueObjects;
using Xunit;

namespace RF.UnitTests.Core.Domain.ValueObject
{
    public class StatementErrorsTest
    {
        private readonly StatementErrors _statementErrors;

        public StatementErrorsTest()
        {
            _statementErrors = new StatementErrors();
        }

        [Fact]
        public void GetStatementErrors_ReturnsStatementErrors()
        {
            //Act
            var result = _statementErrors.GetStatementErrors();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IDictionary<string, HashSet<string>>>(result);
        }

        [Fact]
        public void AddStatementError_IfNoKeyIsPresentOnList_AddNewError()
        {
            //Arrange
            const ETLErrorEnum keyError = ETLErrorEnum.StatementHeader;
            const string error = "Any error";

            //Act
            _statementErrors.AddStatementError(keyError, error);

            //Arrange
            var statementErrors = _statementErrors.GetStatementErrors();
            Assert.NotEmpty(statementErrors);
            Assert.Equal(statementErrors[$"{keyError}"].First(), error);
        }

        [Fact]
        public void AddStatementError_IfErrorKeyIsPresentOnList_AddMessageToAlreadyInstantiatedKey()
        {
            //Arrange
            const ETLErrorEnum keyError = ETLErrorEnum.StatementHeader;
            const string error = "Any error";
            const string error2 = "Any error 2";

            //Act
            _statementErrors.AddStatementError(keyError, error);
            _statementErrors.AddStatementError(keyError, error2);

            //Arrange
            var statementErrors = _statementErrors.GetStatementErrors();
            Assert.NotEmpty(statementErrors);
            Assert.Equal(statementErrors[$"{keyError}"].First(), error);
            Assert.Equal(statementErrors[$"{keyError}"].Last(), error2);
            Assert.Equal(2, statementErrors[$"{keyError}"].Count);
        }

        [Fact]
        public void AddStatementError_IfErrorAlreadyExistsInKey_ErrorIsNotAdded()
        {
            //Arrange
            const ETLErrorEnum keyError = ETLErrorEnum.StatementHeader;
            const string error = "Any error";
            const string error2 = "Any error";

            //Act
            _statementErrors.AddStatementError(keyError, error);
            _statementErrors.AddStatementError(keyError, error2);

            //Arrange
            var etlProcessingErrors = _statementErrors.GetStatementErrors();
            Assert.NotEmpty(etlProcessingErrors);
            Assert.Single(etlProcessingErrors[$"{keyError}"]);
        }
    }
}
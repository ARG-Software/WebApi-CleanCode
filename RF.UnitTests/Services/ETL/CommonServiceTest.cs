using System;
using FakeItEasy;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Services.ETL.Common;
using RF.Services.ETL.Common.Exceptions;
using RF.UnitTests.Services.Fixtures;
using Xunit;

namespace RF.UnitTests.Services.ETL
{
    #region Mocked Abstract Class to be able to test internal methods

    public class ExposedCommonService : CommonService
    {
    }

    #endregion Mocked Abstract Class to be able to test internal methods

    public class CommonServiceTest : CommonConfigurationAndMethodsForServicesTest
    {
        private readonly ExposedCommonService _service;

        public CommonServiceTest()
        {
            _service = A.Fake<ExposedCommonService>
            (
                options => options.CallsBaseMethods()
            );
            _service.UnitOfWork = MockUnitOfWork;
            _service.SongAliasRepository = FakeSongAliasRepository;
        }

        [Fact]
        public void CommitDatabasePendingChanges_IfSuccessful_ReturnTrue()
        {
            //Arrange
            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(true);

            //Act
            var result = _service.CommitDatabasePendingChanges();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CommitDatabasePendingChanges_IfExceptionIsThrownWhenSavingToDatabase_ReturnFalse()
        {
            //Arrange
            A.CallTo(() => MockUnitOfWork.Commit())
                .Throws<Exception>();

            //Act
            var result = _service.CommitDatabasePendingChanges();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetRepositoryByType_IfTypeIsRecognized_ReturnRepoForThatType()
        {
            //Act
            var result = _service.GetRepositoryByType<SongAlias>();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadWriteRepository<SongAlias>>(result);
        }

        [Fact]
        public void GetRepositoryByType_IfTypeIsNotRecognized_ThrowException()
        {
            //Act
            var exception = Assert.Throws<ETLException>(() => _service.GetRepositoryByType<Song>());

            //Assert
            Assert.Equal("Repository not found for the requested type " + typeof(Song).Name, exception.Message);
        }
    }
}
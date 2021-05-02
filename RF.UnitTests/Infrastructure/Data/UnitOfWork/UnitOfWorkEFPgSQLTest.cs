namespace RF.UnitTests.Infrastructure.UnitOfWork
{
    using Moq;
    using Xunit;
    using System;
    using System.Threading;
    using System.Transactions;
    using Microsoft.EntityFrameworkCore;
    using RF.Infrastructure.Data.UnitOfWork.EfPgSql;

    public class UnitOfWorkEFPgSQLTest
    {
        private readonly Mock<DbContext> _fakeContext;

        public UnitOfWorkEFPgSQLTest()
        {
            _fakeContext = new Mock<DbContext>();
        }

        [Fact]
        public void Commit_CallSaveChanges_VerifyIfSaveChangesHasBeenCalledOnce()
        {
            //Arrange
            var saveChangesHasBeenCalled = false;
            var timesSaveChangesHasBeenCalled = 0;
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChanges())
                .Callback(() => { saveChangesHasBeenCalled = true; timesSaveChangesHasBeenCalled++; })
                .Returns(1)
                .Verifiable();
            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act
            var result = mockedUnitOfWork.Commit();

            //Assert
            Assert.True(result);
            Assert.True(saveChangesHasBeenCalled);
            Assert.Equal(1, timesSaveChangesHasBeenCalled);
        }

        [Fact]
        public void Commit_IfCouldntUpdateDatabase_ThrowsInvalidOperationException()
        {
            //Arrange
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChanges())
                .Throws(new DbUpdateException("Bad Operation", new Exception()));

            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            // Act & Assert
            Assert.Throws<TransactionException>(() => mockedUnitOfWork.Commit());
        }

        [Fact]
        public async void CommitAsync_IfCouldntUpdateDatabase_ThrowsInvalidOperationException()
        {
            //Arrange
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Throws(new DbUpdateException("Bad Operation", new Exception()));

            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act & Assert
            await Assert.ThrowsAsync<TransactionException>(() => mockedUnitOfWork.CommitAsync());
        }

        [Fact]
        public void Commit_IfAExceptionIsThrown_ThrowsApplicationException()
        {
            //Arrange
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChanges())
                .Throws(new Exception("Any Exception"));

            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act & Assert
            Assert.Throws<ApplicationException>(() => mockedUnitOfWork.Commit());
        }

        [Fact]
        public async void CommitAsync_IfAExceptionIsThrown_ThrowsApplicationException()
        {
            //Arrange
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Throws(new Exception("Any Exception"));

            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => mockedUnitOfWork.CommitAsync());
        }

        [Fact]
        public async void Commit_CallSaveChangesAsync_VerifyIfSaveChangesAsyncHasBeenCalledOnce()
        {
            //Arrange
            var saveChangesHasBeenCalled = false;
            var timesSaveChangesHasBeenCalled = 0;
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => { saveChangesHasBeenCalled = true; timesSaveChangesHasBeenCalled++; })
                .ReturnsAsync(1)
                .Verifiable();
            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act
            var result = await mockedUnitOfWork.CommitAsync();

            //Assert
            Assert.True(result);
            Assert.True(saveChangesHasBeenCalled);
            Assert.Equal(1, timesSaveChangesHasBeenCalled);
        }

        [Fact]
        public void Dispose_CallDispose_VerifyIfDisposeHasBeenCalledOnce()
        {
            //Arrange
            var disposeHasBeenCalled = false;
            var timesDisposeHasBeenCalled = 0;
            Mock.Get(_fakeContext.Object)
                .Setup(x => x.Dispose())
                .Callback(() => { disposeHasBeenCalled = true; timesDisposeHasBeenCalled++; })
                .Verifiable();
            var mockedUnitOfWork = new UnitOfWorkEfPgSqL(_fakeContext.Object);

            //Act
            mockedUnitOfWork.Dispose();

            //Assert
            Assert.True(disposeHasBeenCalled);
            Assert.Equal(1, timesDisposeHasBeenCalled);
        }
    }
}
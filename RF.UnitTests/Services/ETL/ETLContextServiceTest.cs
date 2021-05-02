using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using FakeItEasy;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Bus;
using RF.Services.ETL;
using RF.Services.ETL.Common.Exceptions;
using RF.UnitTests.Services.Fixtures;
using Xunit;

namespace RF.UnitTests.Services.ETL
{
    public class ETLContextServiceTest : CommonConfigurationAndMethodsForServicesTest
    {
        private readonly ETLContextService _service;
        private readonly IMemoryBus _fakeBus;

        public ETLContextServiceTest()
        {
            _fakeBus = A.Fake<IMemoryBus>();

            _service = A.Fake<ETLContextService>(options => options.CallsBaseMethods());
            _service.Bus = _fakeBus;
            _service.PaymentReceivedRepository = FakePaymentReceivedRepository;
            _service.StatementHeaderRepository = FakeStatementHeaderRepository;
        }

        [Fact]
        public void GetBackgroundTaskContext_ReturnsSetBackgroundContext()
        {
            //Arrange
            _service.SetBackgroundTaskContext(FakePerformContext);

            //Act
            var result = _service.GetBackgroundTaskContext();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, FakePerformContext);
        }

        [Fact]
        public void GetStatementDetailsToBeInserted_ReturnsStatementDetails()
        {
            //Act
            var result = _service.GetStatementDetailsToBeInserted();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<StatementDetail>>(result);
        }

        [Fact]
        public void AddStatementDetailToInsert_AddStatementDetailToList()
        {
            //Arrange
            var mockedStatementDetail = Fixture.Build<StatementDetail>()
                .With(x => x.Id, 1)
                .Create();

            //Act
            _service.AddStatementDetailToInsert(mockedStatementDetail);

            //Assert
            var statementDetails = _service.GetStatementDetailsToBeInserted().ToList();
            Assert.NotNull(statementDetails);
            Assert.NotEmpty(statementDetails);
            Assert.Equal(1, statementDetails.First().Id);
        }

        [Fact]
        public void GetPaymentForETLProcessment_ReturnsPaymentForProcesment()
        {
            //Act
            var result = _service.GetPaymentForETLProcessment();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<PaymentReceived>(result);
        }

        [Fact]
        public void SetPaymentForETLProcessment_IfPaymentDoesntExist_ThrowsException()
        {
            //Arrange
            const int mockPaymentId = 1;
            A.CallTo(() => FakePaymentReceivedRepository.Single(
                    A<Expression<Func<PaymentReceived, bool>>>.Ignored,
                    A<Func<IQueryable<PaymentReceived>, IOrderedQueryable<PaymentReceived>>>.Ignored,
                    false))
                .Returns(null);

            //Act && Assert
            var ex = Assert.Throws<ETLException>(() => _service.SetPaymentForETLProcessment(mockPaymentId));
            Assert.Equal("A payment with the Id " + mockPaymentId + ", doesn't exist on the database", ex.Message);
        }

        [Fact]
        public void SetPaymentForETLProcessment_IfPaymentExists_ReturnsPaymentReceived()
        {
            //Arrange
            const int mockPaymentId = 1;
            const double mockExchangeRate = 1.222;
            const int mockCurrencyId = 12;
            A.CallTo(() => FakePaymentReceivedRepository.Single(
                    A<Expression<Func<PaymentReceived, bool>>>.Ignored,
                    A<Func<IQueryable<PaymentReceived>, IOrderedQueryable<PaymentReceived>>>.Ignored,
                    false))
                .Returns(Fixture.Build<PaymentReceived>()
                    .With(x => x.Id, mockPaymentId)
                    .With(x => x.ExchangeRate, mockExchangeRate)
                    .With(x => x.CurrencyId, mockCurrencyId).Create());

            //Act
            _service.SetPaymentForETLProcessment(mockPaymentId);

            //Assert
            var result = _service.GetPaymentForETLProcessment();
            Assert.NotNull(result);
            Assert.Equal(result.Id, mockPaymentId);
            Assert.Equal(result.ExchangeRate, mockExchangeRate);
            Assert.Equal(result.CurrencyId, mockCurrencyId);
        }

        [Theory]
        [InlineData(2.22, 2, 4.45, true)]
        [InlineData(2.20, 2, 4.46, false)]
        [InlineData(2.20, 1, 2.20, true)]
        public void CheckIfPaymentIsReconciledTheory(double totalForeign, int listSize, double grossAmountForeign, bool expectedResult)
        {
            //Arrange
            A.CallTo(() => FakeStatementHeaderRepository.GetAll(
                    A<Expression<Func<StatementHeader, bool>>>.Ignored,
                    null,
                    true,
                    A<Expression<Func<StatementHeader, object>>>.Ignored))
                .Returns(
                    Fixture.Build<StatementHeader>().With(x => x.TotalForeign, totalForeign).CreateMany(listSize));
            A.CallTo(() => _service.GetPaymentForETLProcessment())
                .Returns(Fixture.Build<PaymentReceived>().With(x => x.GrossAmountForeign, grossAmountForeign).Create());

            //Act
            var result = _service.CheckIfPaymentCanBeReconciled(A.Dummy<StatementHeader>());

            //Assert
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(2.22, 3, 8.88, true)]
        [InlineData(2.20, 2, 4.46, false)]
        [InlineData(2.20, 1, 4.40, true)]
        public void CheckIfPaymentIsReconciledTheory_WithCurrentNewCreatedStatementHeader(double totalForeign, int listSize, double grossAmountForeign, bool expectedResult)
        {
            //Arrange
            const double mockGrossAmountForeign = 2.30;

            A.CallTo(() => FakeStatementHeaderRepository.GetAll(

                    A<Expression<Func<StatementHeader, bool>>>.Ignored,
                    null,
                    true,
                    A<Expression<Func<StatementHeader, object>>>.Ignored))
                .Returns(
                    Fixture.Build<StatementHeader>().With(x => x.TotalForeign, totalForeign).CreateMany(listSize));
            A.CallTo(() => _service.GetPaymentForETLProcessment())
                .Returns(Fixture.Build<PaymentReceived>().With(x => x.GrossAmountForeign, grossAmountForeign).Create());

            //Act
            var result = _service.CheckIfPaymentCanBeReconciled(
                Fixture.Build<StatementHeader>().With(x => x.TotalForeign, totalForeign).Create()
            );

            //Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void MarkPaymentAsReconciled_CheckIfMethodWasCalledWIthReconciledParameterUpdated()
        {
            //Arrange
            var mockedPayment = Fixture.Build<PaymentReceived>().With(x => x.Reconciled, false).Create();
            A.CallTo(() => _service.GetPaymentForETLProcessment())
                 .Returns(mockedPayment);

            //Act
            _service.MarkPaymentAsReconciled();

            //Assert
            var mockedPaymentAfterCall = mockedPayment;
            mockedPaymentAfterCall.Reconciled = true;
            A.CallTo(() => FakePaymentReceivedRepository.UpdateRFEntity(mockedPaymentAfterCall))
                .MustHaveHappenedOnceExactly();
        }
    }
}
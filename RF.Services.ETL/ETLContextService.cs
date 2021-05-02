using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire.Server;
using Hangfire.Tags;
using RF.Domain.Entities;
using RF.Library.Utils;
using RF.Services.ETL.Common;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;

namespace RF.Services.ETL
{
    public class ETLContextService : CommonService, IETLContextService
    {
        private static PerformContext BackgroundContext { get; set; }
        private readonly List<StatementDetail> _statementDetailsToBeInserted;
        private PaymentReceived _paymentForETLProcessment;

        public ETLContextService()
        {
            _paymentForETLProcessment = new PaymentReceived();
            _statementDetailsToBeInserted = new List<StatementDetail>();
        }

        public void SetBackgroundTaskContext(PerformContext context)
        {
            BackgroundContext = context;
        }

        public PerformContext GetBackgroundTaskContext()
        {
            return BackgroundContext;
        }

        public void SetBackgroundTaskContextTags(string[] tags)
        {
            var currentDateTag = DateFunctions.ConvertDateToStringFormat(DateTime.Now, "d");
            var tagsWithCurrentDate = tags.Append(currentDateTag).ToList();
            BackgroundContext.AddTags(tagsWithCurrentDate);
        }

        public IEnumerable<StatementDetail> GetStatementDetailsToBeInserted()
        {
            return _statementDetailsToBeInserted;
        }

        public void AddStatementDetailToInsert(StatementDetail statement)
        {
            _statementDetailsToBeInserted.Add(statement);
        }

        public virtual PaymentReceived GetPaymentForETLProcessment()
        {
            return _paymentForETLProcessment;
        }

        public void SetPaymentForETLProcessment(int paymentId)
        {
            var paymentReceivedForProcessment =
                PaymentReceivedRepository.Single(x => x.Id == paymentId && x.Reconciled == false, null, false);
            this._paymentForETLProcessment = paymentReceivedForProcessment ?? throw new ETLException("A payment with the Id " + paymentId + ", doesn't exist on the database");
        }

        public bool CheckIfPaymentCanBeReconciled(StatementHeader statementHeader)
        {
            var payments = StatementHeaderRepository.GetAll(x => x.PaymentReceivedId == GetPaymentForETLProcessment().Id && x.PaymentReceived.Reconciled == false, null,
                true, x => x.PaymentReceived).ToList();

            //Adding newly created non-saved StatementHeader since it's not queryable yet in the database
            payments.Add(statementHeader);

            var totalPaymentsSum = payments.Sum(x => x.TotalForeign);
            var canPaymentBeReconciled =
                Math.Abs(totalPaymentsSum - GetPaymentForETLProcessment().GrossAmountForeign) <= 0.05;

            return canPaymentBeReconciled;
        }

        public void MarkPaymentAsReconciled()
        {
            var paymentUpdated = GetPaymentForETLProcessment();
            paymentUpdated.Reconciled = true;
            PaymentReceivedRepository.UpdateRFEntity(paymentUpdated);
        }
    }
}
using System.Collections.Generic;
using Hangfire.Server;
using RF.Domain.Entities;

namespace RF.Services.Interfaces.ETL
{
    public interface IETLContextService
    {
        /// <summary>
        /// Gets the statement details to be inserted.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StatementDetail> GetStatementDetailsToBeInserted();

        /// <summary>
        /// Adds the statement detail to insert.
        /// </summary>
        /// <param name="statement">The statement.</param>
        void AddStatementDetailToInsert(StatementDetail statement);

        /// <summary>
        /// Gets the payment for etl processment.
        /// </summary>
        /// <returns></returns>
        PaymentReceived GetPaymentForETLProcessment();

        /// <summary>
        /// Sets the payment for etl processment.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        void SetPaymentForETLProcessment(int paymentId);

        /// <summary>
        /// Checks if payment is reconciled.
        /// </summary>
        /// #TODO: Pass to mediatr. combine this function with MarkPaymentAsReconcilde
        bool CheckIfPaymentCanBeReconciled(StatementHeader statementHeader);

        /// <summary>
        /// Marks the payment as reconciled.
        /// </summary>
        /// <returns></returns>
        /// #TODO: Pass to mediatr
        void MarkPaymentAsReconciled();

        /// <summary>
        /// Gets the background task context.
        /// </summary>
        /// <returns></returns>
        PerformContext GetBackgroundTaskContext();

        /// <summary>
        /// Sets the background task context tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        void SetBackgroundTaskContextTags(string[] tags);

        /// <summary>
        /// Sets the background task context.
        /// </summary>
        /// <param name="context">The context.</param>
        void SetBackgroundTaskContext(PerformContext context);
    }
}
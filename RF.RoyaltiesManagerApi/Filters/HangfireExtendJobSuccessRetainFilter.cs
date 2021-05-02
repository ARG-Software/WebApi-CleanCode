using System;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace RF.RoyaltiesManagerApi.Filters
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Hangfire.Common.JobFilterAttribute" />
    /// <seealso cref="Hangfire.States.IApplyStateFilter" />
    public class HangfireExtendJobSuccessRetainFilter : JobFilterAttribute, IApplyStateFilter
    {
        /// <summary>
        /// Called after the specified state was applied
        /// to the job within the given transaction.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="transaction"></param>
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(60);
        }

        /// <summary>
        /// Called when the state with specified state was
        /// unapplied from the job within the given transaction.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="transaction"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }
}
using System;

namespace RF.Domain.Interfaces.Logger
{
    public interface IRoyaltyManagerLogger
    {
        /// <summary>
        /// Logs job messages on debug level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        void JobDebug(string jobName, string message);

        /// <summary>
        /// Logs job messages on debug level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void JobDebug(string jobName, string message, Exception exception);

        /// <summary>
        /// Logs job messages on warning level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        void JobWarning(string jobName, string message);

        /// <summary>
        /// Logs job messages on warning level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void JobWarning(string jobName, string message, Exception exception);

        /// <summary>
        /// Logs job messages on error level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        void JobError(string jobName, string message);

        /// <summary>
        /// Logs job messages on warning level.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void JobError(string jobName, string message, Exception exception);
    }
}
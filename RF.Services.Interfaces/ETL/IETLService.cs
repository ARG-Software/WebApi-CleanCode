using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Hangfire.Server;
using RF.Domain.Dto;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Services.Interfaces.ETL
{
    public interface IETLService
    {
        /// <summary>
        /// Parses the statement file.
        /// </summary>
        /// <param name="templateId">The template identifier for the current statement.</param>
        /// <param name="statementFile">The statement file.</param>
        /// <returns></returns>
        Task<IEnumerable<IParsedStatement>> ParseStatementFile(int templateId, Stream statementFile);

        /// <summary>
        /// Processes the file in background.
        /// </summary>
        /// <param name="jobName">The name describing the current job</param>
        /// <param name="royaltiesManagerDto">A object containing all metadata for the job processment</param>
        /// <param name="backgroundJob">The context of the background job</param>
        /// <returns></returns>
        [DisplayName("{0}")]
        Task<string> ProcessFileInBackground(string jobName, RoyaltyManagerProcessmentDto royaltiesManagerDto,
            PerformContext backgroundJob);
    }
}
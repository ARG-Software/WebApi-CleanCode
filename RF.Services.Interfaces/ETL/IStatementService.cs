using RF.Domain.Common;
using RF.Domain.Entities;

namespace RF.Services.Interfaces.ETL
{
    public interface IStatementService
    {
        /// <summary>
        /// Inserts the statement header.
        /// </summary>
        /// #TODO: Pass to mediatr
        bool InsertStatementHeader(StatementHeader statementHeader);

        /// <summary>
        /// Inserts the statement details and updates the statement header reference
        /// </summary>
        /// <param name="statementHeader">The statement header.</param>
        /// <returns></returns>
        /// #TODO: Pass to mediatr
        bool InsertStatementDetails(StatementHeader statementHeader);

        /// <summary>
        /// Creates and inserts statement header.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// #TODO: Pass to mediatr
        StatementHeader CreateStatementHeader(int templateId);

        /// <summary>
        /// Finds the code by value passed or by song.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="songId">The song identifier.</param>
        /// <returns></returns>
        /// #TODO: Pass to mediatr
        int? FindCode<T>(string code, int songId) where T : SongCodeIdentifierBase, new();

        /// <summary>
        /// Inserts the code entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code">The code.</param>
        /// <param name="songId">The song identifier.</param>
        /// <returns></returns>
        /// #TODO: Pass to mediatr
        int? InsertCodeEntity<T>(string code, int songId) where T : SongCodeIdentifierBase, new();

        /// <summary>
        /// Updates the code entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code">The code.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// #TODO: Pass to mediatr
        int? UpdateCodeEntity<T>(string code, T entity) where T : SongCodeIdentifierBase, new();
    }
}
namespace RF.Domain.Interfaces.Repositories.Generic
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IWriteRepository<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Inserts the royal flush entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        void InsertRFEntity(TEntity entity);

        /// <summary>
        /// Inserts a batch of royal flush entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void InsertRFEntity(params TEntity[] entities);

        /// <summary>
        /// Inserts a batch of royal flush entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void InsertRFEntity(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts the royal flush entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task InsertRFEntityAsync(TEntity entity);

        /// <summary>
        /// Inserts a batch of royal flush entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task InsertRFEntityAsync(params TEntity[] entities);

        /// <summary>
        /// Inserts a batch of royal flush entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task InsertRFEntityAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the royal flush entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        void UpdateRFEntity(TEntity entity);

        /// <summary>
        /// Updates the list of rf entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void UpdateRFEntity(params TEntity[] entities);

        /// <summary>
        /// Updates the list of rf entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void UpdateRFEntity(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the royal flush entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        void DeleteRFEntity(TEntity entity);

        /// <summary>
        /// Deletes the rf entity list of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void DeleteRFEntity(params TEntity[] entities);

        /// <summary>
        /// Deletes the royal flush entities list.
        /// </summary>
        /// <param name="listToDelete">The list to delete.</param>
        /// <returns></returns>
        void DeleteRFEntity(IEnumerable<TEntity> listToDelete);

        /// <summary>
        /// Deletes the royal flush entity by  unique identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        void DeleteRFEntityById(int id);
    }
}
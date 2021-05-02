namespace RF.Domain.Interfaces.Repositories.Generic
{
    public interface IReadWriteRepository<TEntity> :
                         IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class
    {
    }
}
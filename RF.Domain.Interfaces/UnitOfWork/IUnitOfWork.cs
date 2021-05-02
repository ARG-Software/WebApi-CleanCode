namespace RF.Domain.Interfaces.UnitOfWork
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        Task<bool> CommitAsync();
    }
}
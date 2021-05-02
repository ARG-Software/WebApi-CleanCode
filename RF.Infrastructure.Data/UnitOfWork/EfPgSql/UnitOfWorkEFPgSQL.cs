namespace RF.Infrastructure.Data.UnitOfWork.EfPgSql
{
    using System;
    using System.Transactions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using RF.Domain.Interfaces.UnitOfWork;

    public sealed class UnitOfWorkEfPgSqL : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWorkEfPgSqL(DbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try
            {
                var transactionSuccess = _context.SaveChanges();
                return (transactionSuccess > 0);
            }
            catch (Exception e)
            {
                throw UnitOfWorkExceptionHandler(e);
            }
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                var transactionSuccess = await _context.SaveChangesAsync();
                return (transactionSuccess > 0);
            }
            catch (Exception e)
            {
                throw UnitOfWorkExceptionHandler(e);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private Exception UnitOfWorkExceptionHandler(Exception e)
        {
            if (e is DbUpdateException)
            {
                return new TransactionException("Couldn't commit database transaction when committing to database");
            }

            return new ApplicationException("An error occured on database");
        }
    }
}
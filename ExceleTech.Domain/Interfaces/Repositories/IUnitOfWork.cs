using System.Transactions;

namespace ExceleTech.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollBackTransaction();
        
    }
}

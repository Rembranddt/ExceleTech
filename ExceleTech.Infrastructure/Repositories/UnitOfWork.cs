using ExceleTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ExceleTech.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context;

        public UnitOfWork(EFContext context)
        {
            _context = context;
        
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transaction = new CommittableTransaction(
                new TransactionOptions 
                { 
                    IsolationLevel = isolationLevel
                });
            
            _context.Database.OpenConnection();
            _context.Database.EnlistTransaction(transaction);                    
           
        }
        public void CommitTransaction() 
        {
            var transaction = (CommittableTransaction)_context.Database.GetEnlistedTransaction();
            transaction.Commit();
            transaction.Dispose();            
        }

        public void RollBackTransaction()
        {

            var transaction = _context.Database.GetEnlistedTransaction();
            transaction.Rollback();
            transaction.Dispose();
        }

        public  Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

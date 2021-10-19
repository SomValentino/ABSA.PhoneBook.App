using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ABSA.PhoneBook.Data.EntityMapping;
using ABSA.PhoneBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABSA.PhoneBook.Data.Context {
    public class PhoneBookDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {
            
        }

        public DbSet<Domain.Entities.PhoneBook> PhoneBooks { get; set; }

        public DbSet<Domain.Entities.PhoneBookEntry> PhoneBookEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhoneBookMap());
            modelBuilder.ApplyConfiguration(new PhoneBookEntryMap());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            var transaction = await BeginTransactionAsync();

            if (transaction == null) return false;

            await CommitTransactionAsync(transaction);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
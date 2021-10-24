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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhoneBookMap());
            modelBuilder.ApplyConfiguration(new PhoneBookEntryMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
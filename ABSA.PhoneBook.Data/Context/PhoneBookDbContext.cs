using ABSA.PhoneBook.Data.EntityMapping;
using Microsoft.EntityFrameworkCore;

namespace ABSA.PhoneBook.Data.Context {
    public class PhoneBookDbContext : DbContext 
    {
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
        
    }
}
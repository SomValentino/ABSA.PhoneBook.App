using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABSA.PhoneBook.Data.EntityMapping {
    public class PhoneBookMap : IEntityTypeConfiguration<Domain.Entities.PhoneBook> 
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.PhoneBook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.HasMany(x => x.Entries).WithOne(x => x.PhoneBook).HasForeignKey(x => x.PhoneBookId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
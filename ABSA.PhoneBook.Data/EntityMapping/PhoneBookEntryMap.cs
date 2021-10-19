using System;
using ABSA.PhoneBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABSA.PhoneBook.Data.EntityMapping
{
    public class PhoneBookEntryMap : IEntityTypeConfiguration<PhoneBookEntry>
    {
        public void Configure(EntityTypeBuilder<PhoneBookEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(10).IsRequired();
        }
    }
}

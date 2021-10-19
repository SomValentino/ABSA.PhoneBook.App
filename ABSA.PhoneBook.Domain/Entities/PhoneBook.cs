using System;
using System.Collections.Generic;

namespace ABSA.PhoneBook.Domain.Entities
{
    public class PhoneBook : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<PhoneBookEntry> Entries { get; set; }
    }
}
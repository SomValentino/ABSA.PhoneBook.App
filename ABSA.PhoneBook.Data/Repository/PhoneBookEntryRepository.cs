﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Domain.Entities;
using ABSA.PhoneBook.Domain.Interfaces;

namespace ABSA.PhoneBook.Data.Repository
{
    public class PhoneBookEntryRepository : BaseRepository<PhoneBookEntry>, IPhoneBookEntryRepository
    {
        

        public PhoneBookEntryRepository(PhoneBookDbContext phoneBookDbContext) : base(phoneBookDbContext)
        {
            
        }

        
    }
}

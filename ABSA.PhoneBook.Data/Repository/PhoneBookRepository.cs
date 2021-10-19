using System;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Domain.Interfaces;

namespace ABSA.PhoneBook.Data.Repository
{
    public class PhoneBookRepository : BaseRepository<Domain.Entities.PhoneBook>,IPhoneBookRepository
    {
        public PhoneBookRepository(PhoneBookDbContext phoneBookContext) : base(phoneBookContext)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Response;
using ABSA.PhoneBook.Domain.Entities;

namespace ABSA.PhoneBook.API.Application.Services
{
    public class PhoneBookEntryService : IPhoneBookEntryService
    {
        public PhoneBookEntryService()
        {
        }

        public Task<PhoneBookEntry> Create(PhoneBookEntry phoneBookEntry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PhoneBookEntry phoneBookEntry)
        {
            throw new NotImplementedException();
        }

        public Task<PhoneBookEntryDto> Get(int page, int pageSize, string searchCriteria, int phoneBookId)
        {
            throw new NotImplementedException();
        }

        public Task<PhoneBookEntry> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PhoneBookEntry phoneBookEntry)
        {
            throw new NotImplementedException();
        }
    }
}

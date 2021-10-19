using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABSA.PhoneBook.API.Application.Services
{
    public interface IPhoneBookEntryService
    {
        Task<Domain.Entities.PhoneBookEntry> Create(Domain.Entities.PhoneBookEntry phoneBookEntry);
        Task<IEnumerable<Domain.Entities.PhoneBookEntry>> Get(int page, int pageSize, string searchCriteria);
        Task<Domain.Entities.PhoneBookEntry> GetById(int id);
        Task<bool> Update(Domain.Entities.PhoneBookEntry phoneBookEntry);
        Task<bool> Delete(Domain.Entities.PhoneBookEntry phoneBookEntry);
    }
}

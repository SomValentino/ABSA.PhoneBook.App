using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Response;

namespace ABSA.PhoneBook.API.Application.Services
{
    public interface IPhoneBookEntryService
    {
        Task<Domain.Entities.PhoneBookEntry> Create(Domain.Entities.PhoneBookEntry phoneBookEntry);
        Task<PhoneBookEntryDto> Get(int page, int pageSize, string searchCriteria, int phoneBookId);
        Task<Domain.Entities.PhoneBookEntry> GetById(int id);
        Task<bool> Update(Domain.Entities.PhoneBookEntry phoneBookEntry);
        Task<bool> Delete(Domain.Entities.PhoneBookEntry phoneBookEntry);
    }
}

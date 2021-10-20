using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Response;

namespace ABSA.PhoneBook.API.Application.Services
{
    public interface IPhoneBookService
    {
        Task<Domain.Entities.PhoneBook> Create(Domain.Entities.PhoneBook phoneBook);
        Task<PhoneBookDto> Get(int page, int pageSize, string searchCriteria);

        Task<bool> HasPhoneBook(string name);
        Task<Domain.Entities.PhoneBook> GetById(int id);
        Task<bool> Update(Domain.Entities.PhoneBook phoneBook);
        Task<bool> Delete(Domain.Entities.PhoneBook phoneBook);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Response;
using ABSA.PhoneBook.Domain.Entities;
using ABSA.PhoneBook.Domain.Interfaces;
using ABSA.PhoneBook.API.Application.Utilities;

namespace ABSA.PhoneBook.API.Application.Services
{
    public class PhoneBookEntryService : IPhoneBookEntryService
    {
        private readonly IPhoneBookEntryRepository _phoneBookEntryRepository;
        public PhoneBookEntryService(IPhoneBookEntryRepository phoneBookEntryRepository)
        {
            _phoneBookEntryRepository = phoneBookEntryRepository;
        }

        public async Task<PhoneBookEntry> Create(PhoneBookEntry phoneBookEntry)
        {
            var entry = await _phoneBookEntryRepository.Create(phoneBookEntry);
            await _phoneBookEntryRepository.UnitOfWork.SaveEntitiesAsync();
            return entry;
        }

        public async Task<bool> Delete(PhoneBookEntry phoneBookEntry)
        {
            await _phoneBookEntryRepository.DeleteEntity(phoneBookEntry);
            return await _phoneBookEntryRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<PhoneBookEntryDto> Get(int page, int pageSize, string searchCriteria, int phoneBookId)
        {
            var total = await _phoneBookEntryRepository.GetTotalCount();

            var data = await _phoneBookEntryRepository.GetEntities(page,pageSize,
                            x => x.Name.ToLower() == searchCriteria.ToLower() || searchCriteria == null,y => y.PhoneBookId == phoneBookId);
            
            var dtoData = new PhoneBookEntryDto
            {
                PhoneBookEntries = data,
                Total = total,
                NextPage = Paginatorhelper.NextPage(total,page,pageSize),
                PreviousPage = Paginatorhelper.PreviousPage(total,page)
            };

            return dtoData;
        }

        public async Task<PhoneBookEntry> GetById(int id)
        {
            return await _phoneBookEntryRepository.GetEntityById(id);
        }

        public async Task<bool> Update(PhoneBookEntry phoneBookEntry)
        {
            await _phoneBookEntryRepository.UpdateEntity(phoneBookEntry);
            return await _phoneBookEntryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}

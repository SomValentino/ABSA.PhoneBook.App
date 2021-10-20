using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Response;
using ABSA.PhoneBook.API.Application.Utilities;
using ABSA.PhoneBook.Domain.Interfaces;

namespace ABSA.PhoneBook.API.Application.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public PhoneBookService(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }
        public async Task<Domain.Entities.PhoneBook> Create(Domain.Entities.PhoneBook phoneBook)
        {
            var book = await _phoneBookRepository.Create(phoneBook);
            await _phoneBookRepository.UnitOfWork.SaveEntitiesAsync();
            return book;
        }

        public async Task<bool> Delete(Domain.Entities.PhoneBook phoneBook)
        {
            await _phoneBookRepository.DeleteEntity(phoneBook);
            return await _phoneBookRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<PhoneBookDto> Get(int page, int pageSize, string searchCriteria)
        {
            
            var total = await _phoneBookRepository.GetTotalCount(x => searchCriteria == null ||
                            x.Name.ToLower().Contains(searchCriteria.ToLower()));
            
            
            var data = await _phoneBookRepository.GetEntities(page, pageSize, 
                            x =>  searchCriteria == null || x.Name.ToLower().Contains(searchCriteria.ToLower()));

            var dtoData = new PhoneBookDto
            {
                PhoneBooks = data,
                Total = total,
                NextPage = Paginatorhelper.NextPage(total, page, pageSize),
                PreviousPage = Paginatorhelper.PreviousPage(total,page)
            };

            return dtoData;
        }

        public async Task<Domain.Entities.PhoneBook> GetById(int id)
        {
            return await _phoneBookRepository.GetEntityById(id);
        }

        public async Task<bool> Update(Domain.Entities.PhoneBook phoneBook)
        {
            await _phoneBookRepository.UpdateEntity(phoneBook);
            return await _phoneBookRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}

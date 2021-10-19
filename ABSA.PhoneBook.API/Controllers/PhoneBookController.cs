using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABSA.PhoneBook.API.Application.Services;
using ABSA.PhoneBook.API.Application.Dto.Request;

namespace ABSA.PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly IPhoneBookEntryService _phoneBookEntryService;
        public PhoneBookController(IPhoneBookService phoneBookService, IPhoneBookEntryService phoneBookEntryService)
        {
            _phoneBookService = phoneBookService;
            _phoneBookEntryService = phoneBookEntryService;
        }

        #region PhoneBook
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10, string searchCriteria = null)
        {
            var data = await _phoneBookService.Get(page, pageSize, searchCriteria);

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetPhoneBook")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _phoneBookService.GetById(id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneBookCreateDto phoneBookCreateDto)
        {
            var phoneBook = new Domain.Entities.PhoneBook
            {
                Name = phoneBookCreateDto.Name,
                CreatedAt = DateTime.UtcNow
            };

            var createdPhoneBook = await _phoneBookService.Create(phoneBook);

            return CreatedAtAction("GetPhoneBook", createdPhoneBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PhoneBookCreateDto phoneBookCreateDto)
        {
            var data = await _phoneBookService.GetById(id);

            if (data == null) return NotFound();
            
            data.Name = phoneBookCreateDto.Name;
            data.UpdatedAt = DateTime.UtcNow;

            var result = await _phoneBookService.Update(data);

            if (!result) throw new Exception("PhoneBook was not updated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _phoneBookService.GetById(id);

            if (data == null) return NotFound();

            var result = await _phoneBookService.Delete(data);

            if (!result) throw new Exception("PhoneBook was not deleted");

            return NoContent();
        }
        #endregion
    }
}
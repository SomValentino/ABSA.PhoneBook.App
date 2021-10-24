using System.Reflection.PortableExecutable;
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

            if (data == null) return NotFound(new { errorMessage = "PhoneBook not found" });

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneBookCreateDto phoneBookCreateDto)
        {
            var phoneBookExist = await _phoneBookService.HasPhoneBook(phoneBookCreateDto.Name);
            if(phoneBookExist) return BadRequest(new {errorMessage = "PhoneBook with same already exist"});
            
            var phoneBook = new Domain.Entities.PhoneBook
            {
                Name = phoneBookCreateDto.Name,
                CreatedAt = DateTime.UtcNow
            };

            var createdPhoneBook = await _phoneBookService.Create(phoneBook);

            return CreatedAtAction(nameof(GetById), new {id = createdPhoneBook.Id},createdPhoneBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PhoneBookCreateDto phoneBookCreateDto)
        {
            var data = await _phoneBookService.GetById(id);

            if (data == null) return NotFound(new { errorMessage = "PhoneBook not found" });
            
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

            if (data == null) return NotFound(new { errorMessage = "PhoneBook not found" });

            var result = await _phoneBookService.Delete(data);

            if (!result) throw new Exception("PhoneBook was not deleted");

            return NoContent();
        }
        #endregion

        #region PhoneBookEntry
        [HttpGet("{phoneBookId}/entries")]
        public async Task<IActionResult> GetEntries(int phoneBookId, int page = 1, int pageSize = 10,string searchCriteria = null)
        {
            var phoneBook = await _phoneBookService.GetById(phoneBookId);

            if (phoneBook == null) return NotFound(new { errorMessage = "PhoneBook not found" });

            var entries = await _phoneBookEntryService.Get(page,pageSize,searchCriteria,phoneBookId);

            return Ok(entries);
        }

        [HttpGet("entry/{entryId}",Name = "GetEntry")]
        public async Task<IActionResult> GetEntry(int entryId)
        {
            var entry = await _phoneBookEntryService.GetById(entryId);

            if (entry == null) return NotFound(new { ErrorMessage = "Entry not found" });

            return Ok(entry);
        }

        [HttpPost("{phoneBookId}/entry")]
        public async Task<IActionResult> CreateEntry(int phoneBookId, [FromBody] PhoneBookEntryCreateDto phoneBookEntryCreateDto)
        {
            var phoneBook = await _phoneBookService.GetById(phoneBookId);

            if(phoneBook == null) return NotFound(new{ errorMessage = "PhoneBook not found"});

            var phoneBookEntry = new Domain.Entities.PhoneBookEntry
            {
                Name = phoneBookEntryCreateDto.Name,
                PhoneNumber = phoneBookEntryCreateDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                PhoneBookId = phoneBook.Id
            };

            var createdEntry = await _phoneBookEntryService.Create(phoneBookEntry);

            return CreatedAtAction(nameof(GetEntry), new { entryId = createdEntry.Id},createdEntry);
        }

        [HttpPut("entry/{entryId}")]
        public async Task<IActionResult> UpdateEntry(int entryId,[FromBody] PhoneBookEntryCreateDto phoneBookEntryCreateDto)
        {
            var entry = await _phoneBookEntryService.GetById(entryId);

            if(entry == null) return NotFound(new { errorMessage = "PhoneBook entry not found" });

            entry.Name = phoneBookEntryCreateDto.Name;
            entry.PhoneNumber = phoneBookEntryCreateDto.PhoneNumber;
            entry.UpdatedAt = DateTime.UtcNow;

            var result = await _phoneBookEntryService.Update(entry);

            if(!result) throw new Exception("Entry was not updated");

            return NoContent();
        }

        [HttpDelete("entry/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            var entry = await _phoneBookEntryService.GetById(entryId);

            if (entry == null) return NotFound(new { errorMessage = "PhoneBook Entry not found" });

            var result = await _phoneBookEntryService.Delete(entry);

            if (!result) throw new Exception("Entry was not deleted");

            return NoContent();
        }
        #endregion
    }
}
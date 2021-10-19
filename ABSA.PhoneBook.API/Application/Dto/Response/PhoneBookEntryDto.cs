using System;
using System.Collections.Generic;

namespace ABSA.PhoneBook.API.Application.Dto.Response
{
    public class PhoneBookEntryDto : PaginatedDto
    {
        public IEnumerable<Domain.Entities.PhoneBookEntry> PhoneBooks { get; set; }
    }
}

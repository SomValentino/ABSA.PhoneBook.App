using System;
using System.Collections.Generic;

namespace ABSA.PhoneBook.API.Application.Dto.Response
{
    public class PhoneBookDto : PaginatedDto
    {
        public IEnumerable<Domain.Entities.PhoneBook> PhoneBooks { get; set; }

    }
}

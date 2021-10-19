using System;
namespace ABSA.PhoneBook.API.Application.Dto.Response
{
    public class PaginatedDto
    {
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int Total { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ABSA.PhoneBook.API.Application.Dto.Request
{
    public class PhoneBookCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}

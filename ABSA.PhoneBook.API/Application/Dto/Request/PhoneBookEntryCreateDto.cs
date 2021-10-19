using System.ComponentModel.DataAnnotations;

namespace ABSA.PhoneBook.API.Application.Dto.Request
{
    public class PhoneBookEntryCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
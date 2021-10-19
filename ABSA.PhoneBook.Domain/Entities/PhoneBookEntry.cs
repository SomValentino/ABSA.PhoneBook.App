
namespace ABSA.PhoneBook.Domain.Entities {
    public class PhoneBookEntry : BaseEntity
    {
        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }

        public int PhoneBookId { get; set; }
        public PhoneBook PhoneBook { get; set; }
        
        
    }
}
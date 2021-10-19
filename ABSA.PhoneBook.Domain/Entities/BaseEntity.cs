using ABSA.PhoneBook.Domain.Interfaces;
namespace ABSA.PhoneBook.Domain.Entities {
    public class BaseEntity : IEntity 
    {
        public int Id { get; set; }

    }
}
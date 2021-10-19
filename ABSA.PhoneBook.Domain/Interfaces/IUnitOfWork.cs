using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABSA.PhoneBook.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

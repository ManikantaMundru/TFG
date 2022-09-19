using TFG.Assessment.Domain.Interfaces.Repository;

namespace TFG.Assessment.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IUserRepository Users { get; }
        Task<int> Complete();
    }
}

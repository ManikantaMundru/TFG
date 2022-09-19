using TFG.Assessment.Domain.Entities;

namespace TFG.Assessment.Domain.Interfaces.Repository
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomers();
    }
}

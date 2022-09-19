using Microsoft.EntityFrameworkCore;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.Domain.Interfaces.Repository;

namespace TFG.Assessment.EFCore.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomersContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.OrderByDescending(x => x.Id).ToListAsync();
        }
    }
}

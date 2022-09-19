using TFG.Assessment.Domain.Interfaces;
using TFG.Assessment.Domain.Interfaces.Repository;

namespace TFG.Assessment.EFCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomersContext _context;
        public UnitOfWork(CustomersContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Users = new UserRepository(_context);
        }

        public ICustomerRepository Customers { get; private set; }
        public IUserRepository Users { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

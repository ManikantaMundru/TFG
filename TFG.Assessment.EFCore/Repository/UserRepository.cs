using Microsoft.EntityFrameworkCore;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.Domain.Interfaces.Repository;

namespace TFG.Assessment.EFCore.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CustomersContext context) : base(context)
        {

        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
        }
    }
}

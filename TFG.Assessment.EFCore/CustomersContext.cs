using Microsoft.EntityFrameworkCore;
using TFG.Assessment.Domain.Entities;

namespace TFG.Assessment.EFCore
{
    public class CustomersContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public CustomersContext(DbContextOptions<CustomersContext> options) : base(options)
        {

        }
    }
}

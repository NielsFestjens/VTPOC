using Microsoft.EntityFrameworkCore;
using POC.Core.Customers;

namespace POC.Core
{
    public class POCContext : DbContext
    {
        public POCContext(DbContextOptions<POCContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>();
        }
    }
}

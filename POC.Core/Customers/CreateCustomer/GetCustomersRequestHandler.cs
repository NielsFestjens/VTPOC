using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POC.Infrastructure;
using POC.Infrastructure.Commands;

namespace POC.Core.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly DbContext _dbContext;

        public CreateCustomerCommandHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCustomerCommand command)
        {
            var customer = new Customer
            {
                Name = command.Name
            };
            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
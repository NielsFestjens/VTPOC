using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using POC.Core.Customers;
using POC.Core.Customers.CreateCustomer;
using POC.Test.Utilities;
using Xunit;

namespace POC.Test.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler_Handle
    {
        [Fact]
        public async Task It_creates_the_customer()
        {
            var dbContext = DatabaseTestHelper.GetDbContext();
            var controller = new CreateCustomerCommandHandler(dbContext);
            var command = new CreateCustomerCommand
            {
                Name = "Piet"
            };

            await controller.Handle(command);

            var customer = dbContext.Set<Customer>().Single();
            customer.Name.Should().Be(command.Name);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using POC.Core.Customers;
using POC.Core.Customers.GetCustomers;
using POC.Test.Utilities;
using Xunit;

namespace POC.Test.Customers.GetCustomers
{
    public class GetCustomersRequestHandler_Handle
    {
        [Fact]
        public async Task It_returns_a_list_of_customers()
        {
            var dbContext = DatabaseTestHelper.GetDbContext();

            var customer1 = new CustomerBuilder().WithId(1).WithName("Jos").Build();
            dbContext.Add(customer1);

            var customer2 = new CustomerBuilder().WithId(2).WithName("Frans").Build();
            dbContext.Add(customer2);

            await dbContext.SaveChangesAsync();

            var controller = new GetCustomersRequestHandler(dbContext);

            var customers = (await controller.Handle(new GetCustomerRequest())).Customers;

            CheckCustomer(customers, customer1);
            CheckCustomer(customers, customer2);
            customers.Should().HaveCount(2);
        }

        private void CheckCustomer(List<CustomerDto> result, Customer expectedCustomer)
        {
            var actualCustomer = result.Single(x => x.Id == expectedCustomer.Id);
            actualCustomer.Name.Should().Be(expectedCustomer.Name);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POC.Infrastructure;
using POC.Infrastructure.Requests;

namespace POC.Core.Customers.GetCustomers
{
    public class GetCustomersRequestHandler : IRequestHandler<GetCustomerRequest, GetCustomersResult>
    {
        private readonly DbContext _dbContext;

        public GetCustomersRequestHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCustomersResult> Handle(GetCustomerRequest request)
        {
            var customers = await _dbContext
                .Set<Customer>()
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return new GetCustomersResult
            {
                Customers = customers
            };
        }
    }
}
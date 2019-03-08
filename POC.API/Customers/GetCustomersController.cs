using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.Core.Customers.GetCustomers;
using POC.Infrastructure;
using POC.Infrastructure.Requests;

namespace POC.API.Customers
{
    [Route("api/Customers/[controller]")]
    public class GetCustomersController
    {
        private readonly IRequestDispatcher _requestDispatcher;

        public GetCustomersController(IRequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<GetCustomersResult>> Get(GetCustomerRequest request)
        {
            return await _requestDispatcher.Dispatch<GetCustomerRequest, GetCustomersResult>(request);
        }
    }
}

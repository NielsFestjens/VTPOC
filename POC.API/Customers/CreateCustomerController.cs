using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.Core.Customers.CreateCustomer;
using POC.Infrastructure;

namespace POC.API.Customers
{
    [Route("api/Customers/[controller]")]
    public class CreateCustomerController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CreateCustomerController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return new OkResult();
        }
    }
}

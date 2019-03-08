using System.Collections.Generic;

namespace POC.Core.Customers.GetCustomers
{
    public class GetCustomersResult
    {
        public List<CustomerDto> Customers { get; set; }
    }


    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
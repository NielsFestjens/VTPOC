using POC.Core.Customers;

namespace POC.Test.Customers
{
    public class CustomerBuilder
    {
        private int _id;
        private string _name;

        public CustomerBuilder WithId(int value)
        {
            _id = value;
            return this;
        }

        public CustomerBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public Customer Build()
        {
            return new Customer
            {
                Id = _id,
                Name = _name
            };
        }
    }
}
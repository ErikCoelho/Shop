using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            var name = new Name("Erik", "Coelho");
            var doc = new Document("12345678911");
            var email = new Email("eriktest@gmail.com");

            if (document.Length == 11)
                return new Customer(name, doc, email, "senha321");

            return null;
        }

        public Customer GetEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Create(Customer customer)
        {
            
        }


    }
}

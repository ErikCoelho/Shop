using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Task CreateAsync(Customer customer)
        {
            return Task.CompletedTask;
        }

        public Task<Customer> GetEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetAsync(string document)
        {
            var name = new Name("Erik", "Coelho");
            var doc = new Document("12345678911");
            var email = new Email("eriktest@gmail.com");

            if (document.Length == 11)
                return new Customer(name, doc, email, "senha321");

            return null;
        }
    }
}

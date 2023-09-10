using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task<Customer> GetAsync(string document);
        Task<Customer> GetEmailAsync(string email);
    }
}

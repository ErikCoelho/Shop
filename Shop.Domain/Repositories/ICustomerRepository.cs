using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        Customer Get(string document);
    }
}

using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        Customer Get(string email);
    }
}

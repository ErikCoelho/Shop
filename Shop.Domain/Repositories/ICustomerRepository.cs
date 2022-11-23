using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(string document);
    }
}

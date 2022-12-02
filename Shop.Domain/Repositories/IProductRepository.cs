using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(IEnumerable<Guid> ids);

        void Create(Product product);

        Product GetById(Guid id);

        IEnumerable<Product> GetActiveProducts();

        IEnumerable<Product> GetInactiveProducts();

    }
}

using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);

        Task CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<Product> GetByIdAsync(Guid id);

        Task<IEnumerable<Product>> GetActiveProductsAsync();

        Task<IEnumerable<Product>> GetInactiveProductsAsync();

    }
}

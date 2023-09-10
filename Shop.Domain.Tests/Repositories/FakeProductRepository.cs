using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public Task CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>
            {
                new Product(String.Empty, "Produto 1", "Celular", 10, "true"),
                new Product(String.Empty, "Produto 2", "Celular", 10, "true"),
                new Product(String.Empty, "Produto 3", "Celular", 10, "true")
            };

            return  products;
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetInactiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetActiveProductsAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
